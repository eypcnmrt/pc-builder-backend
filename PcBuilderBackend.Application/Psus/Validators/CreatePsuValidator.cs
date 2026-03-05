using FluentValidation;
using PcBuilderBackend.Application.Psus.Dtos;

namespace PcBuilderBackend.Application.Psus.Validators
{
    public class CreatePsuValidator : AbstractValidator<CreatePsuRequest>
    {
        private static readonly string[] ValidRatings = ["80+", "Bronze", "Silver", "Gold", "Platinum", "Titanium"];
        private static readonly string[] ValidModular = ["Full", "Semi", "Non"];

        public CreatePsuValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Wattage).GreaterThan(0);
            RuleFor(x => x.EfficiencyRating).NotEmpty().Must(r => ValidRatings.Contains(r))
                .WithMessage("Geçerli verimlilik: 80+, Bronze, Silver, Gold, Platinum, Titanium.");
            RuleFor(x => x.Modular).NotEmpty().Must(m => ValidModular.Contains(m))
                .WithMessage("Modular 'Full', 'Semi' veya 'Non' olmalıdır.");
        }
    }
}
