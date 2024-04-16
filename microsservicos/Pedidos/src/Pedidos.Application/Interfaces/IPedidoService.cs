using Pedidos.Application.DTOs;

namespace Pedidos.Application.Interfaces;

public interface IPedidoService
{
    void InserirNaFila(int idUsuario, PedidoDTO pedidoDTO);
    Task Inserir(PedidoFilaDTO pedidoFilaDTO);
    // Task<PedidoRetornoDTO> Atualizar(int id, PedidoDTO PedidoDTO);
    // Task<PedidoRetornoDTO> Obter(int id);
    // Task Excluir(int id);
    // Task<IEnumerable<PedidoRetornoDTO>> Listar();
}