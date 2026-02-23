using System.Security.Cryptography;
using System.Text;
using MediatR;
using ProjectName.Application.Abstractions;
using ProjectName.Application.DTOs;
using ProjectName.Application.Interfaces;
using ProjectName.Domain.Services;

namespace ProjectName.Application.UseCases.Auth.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponseDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenStore _refreshTokenStore;

    public RefreshTokenCommandHandler(ITenantContext tenantContext, ITokenService tokenService, IRefreshTokenStore refreshTokenStore)
    {
        _tenantContext = tenantContext;
        _tokenService = tokenService;
        _refreshTokenStore = refreshTokenStore;
    }

    public async Task<TokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var oldHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.RefreshToken)));
        var isValid = await _refreshTokenStore.IsValidAsync(_tenantContext.UserId, oldHash, cancellationToken);
        if (!isValid)
        {
            throw new UnauthorizedAccessException("Refresh token inválido ou revogado.");
        }

        await _refreshTokenStore.RevokeAsync(_tenantContext.UserId, oldHash, cancellationToken);

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var newHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(newRefreshToken)));
        await _refreshTokenStore.StoreAsync(_tenantContext.UserId, newHash, DateTime.UtcNow.AddDays(7), cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(_tenantContext.UserId, _tenantContext.TenantId, _tenantContext.Roles, _tenantContext.Permissions);
        return new TokenResponseDto(accessToken, newRefreshToken, 900);
    }
}
