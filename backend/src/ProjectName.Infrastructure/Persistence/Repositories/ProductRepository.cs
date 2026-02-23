using Microsoft.EntityFrameworkCore;
using ProjectName.Domain.Entities;
using ProjectName.Domain.Repositories;

namespace ProjectName.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(Guid id, string tenantId, CancellationToken cancellationToken)
        => await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId, cancellationToken);

    public async Task<IReadOnlyList<Product>> ListAsync(string tenantId, CancellationToken cancellationToken)
        => await _dbContext.Products.Where(x => x.TenantId == tenantId).ToListAsync(cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
