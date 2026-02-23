using MediatR;
using ProjectName.Application.Abstractions;
using ProjectName.Application.DTOs;
using ProjectName.Domain.Repositories;

namespace ProjectName.Application.UseCases.Products.ListProducts;

public sealed class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, IReadOnlyList<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly ITenantContext _tenantContext;

    public ListProductsQueryHandler(IProductRepository productRepository, ITenantContext tenantContext)
    {
        _productRepository = productRepository;
        _tenantContext = tenantContext;
    }

    public async Task<IReadOnlyList<ProductDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.ListAsync(_tenantContext.TenantId, cancellationToken);
        return products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Active)).ToList();
    }
}
