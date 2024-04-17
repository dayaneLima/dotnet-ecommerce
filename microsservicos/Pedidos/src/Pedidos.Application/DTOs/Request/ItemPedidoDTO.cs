using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record ItemPedidoDTO
{
    [Required(ErrorMessage = "Quantidade é obrigatória")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Identificador do produto é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Identificador do produto deve ser maior que zero")]
    public int IdProduto { get; set; }
}