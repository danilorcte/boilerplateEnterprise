using MediatR;
using ProjectName.Application.DTOs;

namespace ProjectName.Application.UseCases.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<ProductDto>;
