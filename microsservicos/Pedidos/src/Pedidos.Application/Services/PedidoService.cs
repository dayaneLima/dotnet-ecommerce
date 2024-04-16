using Pedidos.Application.Interfaces;
using Pedidos.Domain.Repository;
using Pedidos.Application.DTOs;
using AutoMapper;
using Pedidos.Domain.Models;
using Pedidos.Domain.Exceptions;
using Pedidos.Application.MessageBus;
using Pedidos.Application.Integrations;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMessageProducer _messageProducer;
    private readonly IMapper _mapper;
    private readonly IProdutoIntegrationService _produtoIntegrationService;
   public static HttpClientHandler HttpClientHandlerIgnoreCertificate()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        return clientHandler;
    }
    public PedidoService(IMessageProducer messageProducer, IMapper mapper, IPedidoRepository PedidoRepository)
    {
        _messageProducer = messageProducer;
        _mapper = mapper;
        _pedidoRepository = PedidoRepository;
        _produtoIntegrationService =  Refit.RestService.For<IProdutoIntegrationService>(new HttpClient(HttpClientHandlerIgnoreCertificate())
        {
            BaseAddress = new Uri("https://produtos-api:5002")
        });;;
    }

    public void InserirNaFila(int idUsuario, PedidoDTO pedidoDTO)
    {
        var pedidoFilaDTO = _mapper.Map<PedidoFilaDTO>(pedidoDTO);        
        _messageProducer.SendMessage(pedidoFilaDTO with {IdUsuario = idUsuario});
    }

    public async Task Inserir(PedidoFilaDTO pedidoFilaDTO)
    {
        var pedido = _mapper.Map<Pedido>(pedidoFilaDTO);
        var idsProdutos = pedido.ItensPedido?.Select(e => e.IdProduto).ToList();

        var produtosRetornoDTO = await _produtoIntegrationService.Listar(string.Join(',', idsProdutos!));

        ValidarAtribuirPrecoVendaItensPedido(pedido, produtosRetornoDTO);
        pedido.Status = StatusPedido.EM_PROCESSAMENTO;
        pedido.ValorTotal =  pedido.ItensPedido!.Sum(i => i.PrecoVenda);
        
        var validacao = pedido.Validar();
        if (!validacao.IsValid) throw new EntityErrorException($"Dados do pedido inválido(s)", validacao);

        _pedidoRepository.Inserir(pedido);
        await _pedidoRepository.UnitOfWork.Commit();
    }

    private void ValidarAtribuirPrecoVendaItensPedido(Pedido pedido, IEnumerable<ProdutoRetornoDTO> produtosRetornoDTO)
    {
        foreach (var itemPedido in pedido.ItensPedido!)
        {
            var produtoRelacionado = produtosRetornoDTO.Where(p => p.Id == itemPedido.IdProduto).FirstOrDefault();

            if (produtoRelacionado is null) 
                throw new NotFoundException($"Produto de id {itemPedido.Id} pedido não encontrado");

            if (itemPedido.Quantidade > produtoRelacionado.QuantidadeDisponivel)
                throw new BadRequestException($"A quantidade solicitada ${itemPedido.Quantidade} é maior do que a disponível (${produtoRelacionado.QuantidadeDisponivel}) para o produto ${produtoRelacionado.Nome}");

            itemPedido.PrecoVenda = produtoRelacionado.Valor;
        }
    }

    // public async Task<PedidoRetornoDTO> Atualizar(int id, PedidoDTO PedidoDTO)
    // {
    //     var PedidoBanco = await Obter(id);

    //     if (PedidoBanco is null)
    //         throw new NotFoundException("Pedido não encontrado!");

    //     var Pedido = _mapper.Map<Pedido>(PedidoDTO);
    //     Pedido.Id = id;

    //     var validacao = Pedido.Validar();
    //     if (!validacao.IsValid) throw new EntityErrorException($"Dados do Pedido {PedidoDTO.Nome} inválido(s)", validacao);

    //     _PedidoRepository.Atualizar(Pedido);
    //     await _PedidoRepository.UnitOfWork.Commit();

    //     return _mapper.Map<PedidoRetornoDTO>(Pedido);
    // }

    // public async Task<PedidoRetornoDTO> Obter(int id)
    // {
    //     var Pedido = await _PedidoRepository.ObterPorId(id);

    //     if (Pedido is null)
    //         throw new NotFoundException("Pedido não encontrado!");

    //     return _mapper.Map<PedidoRetornoDTO>(Pedido);
    // }

    // public async Task Excluir(int id)
    // {
    //     _PedidoRepository.Excluir(id);
    //     var PedidoExcluido = await _PedidoRepository.UnitOfWork.Commit();

    //     if (!PedidoExcluido)
    //         throw new NotFoundException("Pedido não encontrado!");
    // }

    // public async Task<IEnumerable<PedidoRetornoDTO>> Listar()
    // {
    //     var Pedidos = await _PedidoRepository.ObterTodos();
    //     return _mapper.Map<IEnumerable<PedidoRetornoDTO>>(Pedidos);
    // }
}
  