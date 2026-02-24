using ProjectName.Application.Mediator;
using ProjectName.Application.DTOs;

namespace ProjectName.Application.UseCases.Products.ListProducts;

public sealed record ListProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
