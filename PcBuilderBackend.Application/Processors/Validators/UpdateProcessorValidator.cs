using FluentValidation;
using PcBuilderBackend.Application.Processors.Dtos;

namespace PcBuilderBackend.Application.Processors.Validators
{
    public class UpdateProcessorValidator : AbstractValidator<UpdateProcessorRequest>
    {
        public UpdateProcessorValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Socket).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Cores).GreaterThan(0);
            RuleFor(x => x.Threads).GreaterThanOrEqualTo(x => x.Cores);
            RuleFor(x => x.BaseClock).GreaterThan(0);
            RuleFor(x => x.BoostClock).GreaterThanOrEqualTo(x => x.BaseClock);
            RuleFor(x => x.Tdp).GreaterThan(0);
        }
    }
}
