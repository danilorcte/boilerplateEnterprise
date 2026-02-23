using System.Security.Cryptography;
using System.Text;
using MediatR;
using ProjectName.Application.Abstractions;
using ProjectName.Application.DTOs;
using ProjectName.Application.Interfaces;
using ProjectName.Domain.Services;

namespace ProjectName.Application.UseCases.Auth.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponseDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenStore _refreshTokenStore;

    public LoginCommandHandler(ITenantContext tenantContext, ITokenService tokenService, IRefreshTokenStore refreshTokenStore)
    {
        _tenantContext = tenantContext;
        _tokenService = tokenService;
        _refreshTokenStore = refreshTokenStore;
    }

    public async Task<TokenResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Placeholder for identity provider integration.
        var accessToken = _tokenService.GenerateAccessToken(_tenantContext.UserId, _tenantContext.TenantId, _tenantContext.Roles, _tenantContext.Permissions);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
        await _refreshTokenStore.StoreAsync(_tenantContext.UserId, tokenHash, DateTime.UtcNow.AddDays(7), cancellationToken);

        return new TokenResponseDto(accessToken, refreshToken, 900);
    }
}
