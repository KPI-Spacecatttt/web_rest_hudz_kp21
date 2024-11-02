using FluentValidation;
using web_rest_hudz_kp21.Models.DTOs;


namespace web_rest_hudz_kp21.Validators
{
    public class BikePartValidator : AbstractValidator<BikePartDTO>
    {
        public BikePartValidator()
        {
            RuleFor(part => part.PartType)
                .NotEmpty().WithMessage("Part type is required");
            RuleFor(part => part.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(part => part.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer name is required");
            RuleFor(part => part.Price)
                .GreaterThan(0).WithMessage("Price must be a positive value");
            RuleFor(part => part.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");
        }
    }
}