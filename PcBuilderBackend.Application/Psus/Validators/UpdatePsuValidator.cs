using FluentValidation;
using PcBuilderBackend.Application.Psus.Dtos;

namespace PcBuilderBackend.Application.Psus.Validators
{
    public class UpdatePsuValidator : AbstractValidator<UpdatePsuRequest>
    {
        private static readonly string[] ValidRatings = ["80+", "Bronze", "Silver", "Gold", "Platinum", "Titanium"];
        private static readonly string[] ValidModular = ["Full", "Semi", "Non"];

        public UpdatePsuValidator()
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
