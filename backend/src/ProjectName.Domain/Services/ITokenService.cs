namespace ProjectName.Domain.Services;

public interface ITokenService
{
    string GenerateAccessToken(string userId, string tenantId, IEnumerable<string> roles, IEnumerable<string> permissions);
    string GenerateRefreshToken();
}
