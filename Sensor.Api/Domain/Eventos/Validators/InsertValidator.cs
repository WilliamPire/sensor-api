using FluentValidation;

namespace Sensor.Api.Domain.Eventos.Validators
{
    public class InsertValidator : AbstractValidator<Commands.Inserir.Request>
    {
        public InsertValidator()
        {
            RuleFor(a => a.TimeStamp)
                .NotEmpty()
                .WithMessage("O timeStamp é obrigatório");

            RuleFor(a => a.Tag)
                .NotEmpty()
                .WithMessage("A tag é obrigatória");

            RuleFor(a => a.Valor)
                .NotEmpty()
                .WithMessage("O valor é obrigatório");
        }
    }
}
