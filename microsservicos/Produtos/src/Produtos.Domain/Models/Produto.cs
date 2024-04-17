using Produtos.Domain.Models.Core;
using Produtos.Domain.Validations;
using Produtos.Domain.Errors;

namespace Produtos.Domain.Models;

public class Produto : EntityTimestamp
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public double Valor { get; set; }
    public required string Categoria { get; set; }
    public int QuantidadeDisponivel { get; set; }
    public required string UrlImagem { get; set; }
    
    public override EntityError Validar() => new EntityError(new ProdutoValidation().Validate(this));
}
