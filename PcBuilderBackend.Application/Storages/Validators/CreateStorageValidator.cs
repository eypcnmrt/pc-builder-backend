using FluentValidation;
using PcBuilderBackend.Application.Storages.Dtos;

namespace PcBuilderBackend.Application.Storages.Validators
{
    public class CreateStorageValidator : AbstractValidator<CreateStorageRequest>
    {
        public CreateStorageValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Type).NotEmpty().Must(t => new[] { "SSD", "HDD", "NVMe" }.Contains(t))
                .WithMessage("Type 'SSD', 'HDD' veya 'NVMe' olmalıdır.");
            RuleFor(x => x.CapacityGb).GreaterThan(0);
            RuleFor(x => x.Interface).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ReadSpeedMbs).GreaterThan(0);
            RuleFor(x => x.WriteSpeedMbs).GreaterThan(0);
        }
    }
}
