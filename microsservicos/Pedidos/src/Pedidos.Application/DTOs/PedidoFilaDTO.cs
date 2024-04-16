using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record PedidoFilaDTO: PedidoDTO
{
    [Required(ErrorMessage = "Id do usuário é obrigatório")]
    public required int IdUsuario {get; init;}
}