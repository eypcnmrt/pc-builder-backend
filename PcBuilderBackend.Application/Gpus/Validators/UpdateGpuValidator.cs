using FluentValidation;
using PcBuilderBackend.Application.Gpus.Dtos;

namespace PcBuilderBackend.Application.Gpus.Validators
{
    public class UpdateGpuValidator : AbstractValidator<UpdateGpuRequest>
    {
        public UpdateGpuValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.MemoryGb).GreaterThan(0);
            RuleFor(x => x.MemoryType).NotEmpty().MaximumLength(20);
            RuleFor(x => x.CoreClock).GreaterThan(0);
            RuleFor(x => x.BoostClock).GreaterThanOrEqualTo(x => x.CoreClock);
            RuleFor(x => x.Tdp).GreaterThan(0);
            RuleFor(x => x.LengthMm).GreaterThan(0);
        }
    }
}
