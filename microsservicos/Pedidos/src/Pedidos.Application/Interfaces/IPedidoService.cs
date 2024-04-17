using Pedidos.Application.DTOs;

namespace Pedidos.Application.Interfaces;

public interface IPedidoService
{
    void PublicarNaFila(int idUsuario, PedidoDTO pedidoDTO);
    Task Inserir(PedidoFilaDTO pedidoFilaDTO);
    Task<PedidoRetornoDTO> Obter(int idUsuario, int id);
    Task<PedidoDetalhadoRetornoDTO> ObterComItensPedido(int idUsuario, int id);
    Task<IEnumerable<PedidoRetornoDTO>> Listar(int idUsuario);
    Task<IEnumerable<PedidoDetalhadoRetornoDTO>> ListarComItensPedido(int idUsuario);
}