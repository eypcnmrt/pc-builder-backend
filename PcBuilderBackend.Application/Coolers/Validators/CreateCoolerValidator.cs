using FluentValidation;
using PcBuilderBackend.Application.Coolers.Dtos;

namespace PcBuilderBackend.Application.Coolers.Validators
{
    public class CreateCoolerValidator : AbstractValidator<CreateCoolerRequest>
    {
        public CreateCoolerValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Type).NotEmpty().Must(t => new[] { "Air", "Liquid" }.Contains(t))
                .WithMessage("Type 'Air' veya 'Liquid' olmalıdır.");
            RuleFor(x => x.TdpW).GreaterThan(0);
            RuleFor(x => x.CompatibleSockets).NotEmpty();
            RuleFor(x => x.HeightMm).GreaterThan(0).When(x => x.HeightMm.HasValue);
            RuleFor(x => x.HeightMm).NotNull().When(x => x.Type == "Air")
                .WithMessage("Hava soğutucusu için HeightMm girilmesi zorunludur.");
            RuleFor(x => x.RadiatorSizeMm).GreaterThan(0).When(x => x.RadiatorSizeMm.HasValue);
            RuleFor(x => x.RadiatorSizeMm).NotNull().When(x => x.Type == "Liquid")
                .WithMessage("Sıvı soğutucusu için RadiatorSizeMm girilmesi zorunludur.");
        }
    }
}
