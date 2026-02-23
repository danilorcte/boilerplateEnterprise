using Microsoft.Extensions.Caching.Distributed;
using ProjectName.Application.Interfaces;

namespace ProjectName.Infrastructure.Cache;

public sealed class RedisRefreshTokenStore : IRefreshTokenStore
{
    private readonly IDistributedCache _distributedCache;

    public RedisRefreshTokenStore(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task StoreAsync(string userId, string tokenHash, DateTime expiresAt, CancellationToken cancellationToken)
    {
        var key = BuildKey(userId, tokenHash);
        await _distributedCache.SetStringAsync(key, "1", new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = expiresAt
        }, cancellationToken);
    }

    public async Task<bool> IsValidAsync(string userId, string tokenHash, CancellationToken cancellationToken)
    {
        var key = BuildKey(userId, tokenHash);
        var value = await _distributedCache.GetStringAsync(key, cancellationToken);
        return value == "1";
    }

    public async Task RevokeAsync(string userId, string tokenHash, CancellationToken cancellationToken)
    {
        var key = BuildKey(userId, tokenHash);
        await _distributedCache.RemoveAsync(key, cancellationToken);
    }

    private static string BuildKey(string userId, string tokenHash) => $"refresh:{userId}:{tokenHash}";
}
