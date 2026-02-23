using ProjectName.Domain.Entities;

namespace ProjectName.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, string tenantId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> ListAsync(string tenantId, CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
}
