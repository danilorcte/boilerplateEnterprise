using FluentValidation;
using ProjectName.Application.UseCases.Products.CreateProduct;

namespace ProjectName.Application.Validators;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
