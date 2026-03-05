using FluentValidation;
using PcBuilderBackend.Application.PcCases.Dtos;

namespace PcBuilderBackend.Application.PcCases.Validators
{
    public class UpdatePcCaseValidator : AbstractValidator<UpdatePcCaseRequest>
    {
        public UpdatePcCaseValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FormFactor).NotEmpty().Must(f => new[] { "ATX", "mATX", "ITX" }.Contains(f))
                .WithMessage("FormFactor 'ATX', 'mATX' veya 'ITX' olmalıdır.");
            RuleFor(x => x.MaxGpuLengthMm).GreaterThan(0);
            RuleFor(x => x.MaxCoolerHeightMm).GreaterThan(0);
        }
    }
}
