using FluentValidation;
using TaskManager.Application.Dtos;

namespace TaskManager.Application.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.");
    }
}
public class UpdateTaskValidator : AbstractValidator<UpdateTaskDto>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.");
    }
}