using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record PedidoFilaDTO: PedidoDTO
{
    [Required(ErrorMessage = "Id do usuário é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Id do usuário deve ser maior que zero")]
    public required int IdUsuario {get; init;}
}