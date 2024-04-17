using Produtos.Domain.Errors;

namespace Produtos.Domain.Models.Core;

public abstract class Entity
{
    public int Id { get; set; }
    public override string ToString() => $"{GetType().Name} [Id={Id}]";
    public abstract EntityError Validar();
}