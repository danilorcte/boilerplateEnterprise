namespace ProjectName.Application.Interfaces;

public interface IRefreshTokenStore
{
    Task StoreAsync(string userId, string tokenHash, DateTime expiresAt, CancellationToken cancellationToken);
    Task<bool> IsValidAsync(string userId, string tokenHash, CancellationToken cancellationToken);
    Task RevokeAsync(string userId, string tokenHash, CancellationToken cancellationToken);
}
