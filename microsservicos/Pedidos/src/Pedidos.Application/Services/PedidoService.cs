using AutoMapper;

using Pedidos.Application.Interfaces;
using Pedidos.Domain.Repository;
using Pedidos.Application.DTOs;
using Pedidos.Domain.Models;
using Pedidos.Domain.Exceptions;
using Pedidos.Application.MessageBus;
using Pedidos.Application.Integrations;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IMapper _mapper;
    private readonly IMessageProducer _messageProducer;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoIntegrationService _produtoIntegrationService;

    public PedidoService(IMessageProducer messageProducer, IMapper mapper, IPedidoRepository PedidoRepository, IProdutoIntegrationService produtoIntegrationService)
    {
        _mapper = mapper;
        _messageProducer = messageProducer;
        _pedidoRepository = PedidoRepository;
        _produtoIntegrationService = produtoIntegrationService;
    }

    public void PublicarNaFila(int idUsuario, PedidoDTO pedidoDTO)
    {
        var pedidoFilaDTO = _mapper.Map<PedidoFilaDTO>(pedidoDTO);        
        _messageProducer.SendMessage(pedidoFilaDTO with {IdUsuario = idUsuario});
    }

    public async Task Inserir(PedidoFilaDTO pedidoFilaDTO)
    {
        var pedido = _mapper.Map<Pedido>(pedidoFilaDTO);
        var produtosDTO = await ObterProdutosDTO(pedido);

        ValidarAtribuirPrecoVendaItensPedido(pedido, produtosDTO);

        pedido.Status = StatusPedido.EM_PROCESSAMENTO;
        pedido.ValorTotal =  CalcularValorTotalPedido(pedido);
        
        var validacao = pedido.Validar();
        if (!validacao.IsValid) throw new EntityErrorException($"Dados do pedido inválido(s)", validacao);

        _pedidoRepository.Inserir(pedido);
        await _pedidoRepository.UnitOfWork.Commit();
    }

    public async Task<PedidoRetornoDTO> Obter(int idUsuario, int id)
    {
        var pedido = await _pedidoRepository.ObterPedidoUsuario(idUsuario, id);

        if (pedido is null)
            throw new NotFoundException("Pedido não encontrado!");

        return _mapper.Map<PedidoRetornoDTO>(pedido);
    }

    public async Task<IEnumerable<PedidoRetornoDTO>> Listar(int idUsuario)
    {
        var pedidos = await _pedidoRepository.ObterTodosPedidosUsuario(idUsuario);
        return _mapper.Map<IEnumerable<PedidoRetornoDTO>>(pedidos);
    }

    public async Task<PedidoDetalhadoRetornoDTO> ObterComItensPedido(int idUsuario, int id)
    {
        var pedido = await _pedidoRepository.ObterPedidoUsuarioComItensPedido(idUsuario, id);

        if (pedido is null)
            throw new NotFoundException("Pedido não encontrado!");

        return await ObterPedidoDetalhado(pedido);
    }

    public async Task<IEnumerable<PedidoDetalhadoRetornoDTO>> ListarComItensPedido(int idUsuario)
    {
        var pedidosDetalhados = new List<PedidoDetalhadoRetornoDTO>();
        var pedidos = await _pedidoRepository.ObterTodosPedidosUsuarioComItensPedido(idUsuario);

        foreach (var pedido in pedidos)
            pedidosDetalhados.Add(await ObterPedidoDetalhado(pedido));            

        return pedidosDetalhados;
    }

    private async Task<IEnumerable<ProdutoDTO>> ObterProdutosDTO(Pedido pedido)
    {
        var idsProdutos = pedido.ItensPedido?.Select(e => e.IdProduto).ToList();
        return await _produtoIntegrationService.Listar(string.Join(',', idsProdutos!));
    }

    private void ValidarAtribuirPrecoVendaItensPedido(Pedido pedido, IEnumerable<ProdutoDTO> produtosDTO)
    {
        foreach (var itemPedido in pedido.ItensPedido!)
        {
            var produtoRelacionado = produtosDTO.Where(p => p.Id == itemPedido.IdProduto).FirstOrDefault();

            if (produtoRelacionado is null) 
                throw new NotFoundException($"Produto de id {itemPedido.Id} pedido não encontrado");

            if (itemPedido.Quantidade > produtoRelacionado.QuantidadeDisponivel)
                throw new BadRequestException($"A quantidade solicitada ${itemPedido.Quantidade} é maior do que a disponível (${produtoRelacionado.QuantidadeDisponivel}) para o produto ${produtoRelacionado.Nome}");

            itemPedido.PrecoVenda = produtoRelacionado.Valor;
        }
    }

    private double CalcularValorTotalPedido(Pedido pedido) => pedido.ItensPedido!.Sum(i => i.PrecoVenda * i.Quantidade);

    private async Task<PedidoDetalhadoRetornoDTO> ObterPedidoDetalhado(Pedido pedido) 
    {
        var itensPedido = new List<ItemPedidoRetornoDTO>();
        var produtosDTO = await ObterProdutosDTO(pedido);

        foreach (var itemPedido in pedido.ItensPedido!)
        {
            var produtoRelacionado = produtosDTO.Where(p => p.Id == itemPedido.IdProduto).FirstOrDefault();

            if (produtoRelacionado is null) 
                throw new NotFoundException($"Produto de id {itemPedido.Id} pedido não encontrado");

            itensPedido.Add(
                new ItemPedidoRetornoDTO(
                    itemPedido.Quantidade, produtoRelacionado.Nome, produtoRelacionado.Descricao, 
                    itemPedido.PrecoVenda,produtoRelacionado.Categoria,produtoRelacionado.UrlImagem
                )
            );
        }

        return new PedidoDetalhadoRetornoDTO(pedido.Id, pedido.ValorTotal, pedido.Status.ToString(), pedido.DataHorarioCadastro, itensPedido);
    }
}
  