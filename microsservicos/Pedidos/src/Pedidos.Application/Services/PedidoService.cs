using Pedidos.Application.Interfaces;
using Pedidos.Domain.Repository;
using Pedidos.Application.DTOs;
using AutoMapper;
using Pedidos.Domain.Models;
using Pedidos.Domain.Exceptions;

namespace Pedidos.Application.Services;

public class PedidoService : IPedidoService
{
    // private readonly IPedidoRepository _PedidoRepository;
    // private readonly IMapper _mapper;

    // public PedidoService(IPedidoRepository PedidoRepository, IMapper mapper)
    // {
    //     _PedidoRepository = PedidoRepository;
    //     _mapper = mapper;
    // }

    // public async Task<PedidoRetornoDTO> Inserir(PedidoDTO PedidoDTO)
    // {
    //     var Pedido = _mapper.Map<Pedido>(PedidoDTO);
    //     var validacao = Pedido.Validar();
    //     if (!validacao.IsValid) throw new EntityErrorException($"Dados do Pedido {PedidoDTO.Nome} inválido(s)", validacao);

    //     _PedidoRepository.Inserir(Pedido);
    //     await _PedidoRepository.UnitOfWork.Commit();

    //     return _mapper.Map<PedidoRetornoDTO>(Pedido);
    // }

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
  