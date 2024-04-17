namespace Produtos.Domain.Models.Core;

public abstract class EntityTimestamp : Entity
{
    public DateTime? DataHorarioCadastro { get; set; }
    public DateTime? DataHorarioAtualizacao { get; set; }
    public DateTime? DataHorarioExclusao { get; set; }
}