using FluentValidation;
using web_rest_hudz_kp21.Models.DTOs;

namespace web_rest_hudz_kp21.Validators
{
    public class BicycleValidator : AbstractValidator<BicycleDTO>
    {
        public BicycleValidator()
        {
            RuleFor(bicycle => bicycle.Model)
                .NotEmpty().WithMessage("Model name is required");
            RuleFor(bicycle => bicycle.Type)
                .NotEmpty().WithMessage("Type is required");
            RuleFor(bicycle => bicycle.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer name is required");
            RuleFor(bicycle => bicycle.ReleaseYear)
                .InclusiveBetween(2005, DateTime.Now.Year)
                .WithMessage($"Release year must be between 2005 and {DateTime.Now.Year}");
            RuleFor(bicycle => bicycle.Weight)
                .GreaterThan(0).WithMessage("Weight must be a positive value");
            RuleFor(bicycle => bicycle.Price)
                .GreaterThan(0).WithMessage("Price must be a positive value");
            RuleFor(bicycle => bicycle.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");
        }
    }

}