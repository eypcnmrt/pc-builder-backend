using FluentValidation;
using PcBuilderBackend.Application.Rams.Dtos;

namespace PcBuilderBackend.Application.Rams.Validators
{
    public class CreateRamValidator : AbstractValidator<CreateRamRequest>
    {
        public CreateRamValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CapacityGb).GreaterThan(0);
            RuleFor(x => x.Type).NotEmpty().MaximumLength(10);
            RuleFor(x => x.SpeedMhz).GreaterThan(0);
            RuleFor(x => x.Modules).GreaterThan(0);
            RuleFor(x => x.LatencyCl).GreaterThan(0);
        }
    }
}
