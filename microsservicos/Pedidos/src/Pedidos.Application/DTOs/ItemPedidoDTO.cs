using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record ItemPedidoDTO
{
    [Required(ErrorMessage = "Quantidade é obrigatória")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Identificador do produto é obrigatório")]
    public int IdProduto { get; set; }
}