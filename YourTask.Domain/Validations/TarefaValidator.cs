using FluentValidation;
using YourTask.Domain.Models;

namespace YourTask.Domain.Validations;

public class TarefaValidator : AbstractValidator<Tarefa>
{
    public TarefaValidator()
    {
        RuleFor(t => t.Titulo)
            .NotEmpty().WithMessage("Titulo é obrigatório")
            .MaximumLength(100).WithMessage("Título deve ter no máximo 100 caracteres");

        RuleFor(t => t.DataConclusao)
            .GreaterThanOrEqualTo(t => t.DataCriacao)
            .WithMessage("Data de conclusão não pode ser anterior à data de criação");
    }
}
