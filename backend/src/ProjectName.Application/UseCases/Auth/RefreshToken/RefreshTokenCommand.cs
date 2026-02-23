using MediatR;
using ProjectName.Application.DTOs;

namespace ProjectName.Application.UseCases.Auth.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResponseDto>;
