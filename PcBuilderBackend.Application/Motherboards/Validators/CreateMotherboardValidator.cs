using FluentValidation;
using PcBuilderBackend.Application.Motherboards.Dtos;

namespace PcBuilderBackend.Application.Motherboards.Validators
{
    public class CreateMotherboardValidator : AbstractValidator<CreateMotherboardRequest>
    {
        private static readonly string[] ValidFormFactors = ["ATX", "mATX", "ITX"];

        public CreateMotherboardValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Socket).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Chipset).NotEmpty().MaximumLength(100);
            RuleFor(x => x.FormFactor).NotEmpty()
                .Must(f => ValidFormFactors.Contains(f))
                .WithMessage("Form faktör ATX, mATX veya ITX olmalıdır.");
            RuleFor(x => x.MaxRamGb).GreaterThan(0);
            RuleFor(x => x.RamSlots).GreaterThan(0);
            RuleFor(x => x.SupportedRamType).NotEmpty()
                .Must(r => new[] { "DDR4", "DDR5" }.Contains(r))
                .WithMessage("Desteklenen RAM tipi DDR4 veya DDR5 olmalıdır.");
        }
    }
}
