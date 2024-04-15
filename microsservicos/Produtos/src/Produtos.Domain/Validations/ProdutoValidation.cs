using FluentValidation;
using Produtos.Domain.Models;

namespace Produtos.Domain.Validations;

public class ProdutoValidation : AbstractValidator<Produto>
{
    public ProdutoValidation()
    {             
        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(255).WithMessage("Nome não pode passar de 255 caracteres");

        RuleFor(e => e.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatório")
            .MaximumLength(255).WithMessage("Descrição não pode passar de 255 caracteres");

        RuleFor(e => e.Valor).NotEmpty().WithMessage("Valor é obrigatório");
        RuleFor(e => e.Categoria).NotEmpty().WithMessage("Categoria é obrigatório");
        RuleFor(e => e.QuantidadeDisponivel).NotEmpty().WithMessage("Quantidade disponível é obrigatório");
        RuleFor(e => e.UrlImagem).NotEmpty().WithMessage("Imagem é obrigatório");
        RuleFor(e => e.DataHorarioCadastro).NotEmpty().WithMessage("Data de cadastro é obrigatório");
        RuleFor(e => e.DataHorarioAtualizacao).NotEmpty().WithMessage("Data de atualização é obrigatório");
    }
}
