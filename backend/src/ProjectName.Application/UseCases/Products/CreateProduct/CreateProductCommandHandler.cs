using MediatR;
using ProjectName.Application.Abstractions;
using ProjectName.Application.DTOs;
using ProjectName.Domain.Entities;
using ProjectName.Domain.Repositories;

namespace ProjectName.Application.UseCases.Products.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly ITenantContext _tenantContext;

    public CreateProductCommandHandler(IProductRepository productRepository, ITenantContext tenantContext)
    {
        _productRepository = productRepository;
        _tenantContext = tenantContext;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name.Trim(), request.Price, _tenantContext.UserId);
        product.SetTenant(_tenantContext.TenantId);

        await _productRepository.AddAsync(product, cancellationToken);

        return new ProductDto(product.Id, product.Name, product.Price, product.Active);
    }
}
