using FluentValidation;
using PcBuilderBackend.Application.Gpus.Dtos;

namespace PcBuilderBackend.Application.Gpus.Validators
{
    public class CreateGpuValidator : AbstractValidator<CreateGpuRequest>
    {
        public CreateGpuValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.MemoryGb).GreaterThan(0);
            RuleFor(x => x.BoostClock).GreaterThan(0);
            RuleFor(x => x.Tdp).GreaterThan(0);
            RuleFor(x => x.LengthMm).GreaterThan(0);
        }
    }
}
