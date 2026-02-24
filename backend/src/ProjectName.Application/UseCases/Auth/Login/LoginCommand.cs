using ProjectName.Application.Mediator;
using ProjectName.Application.DTOs;

namespace ProjectName.Application.UseCases.Auth.Login;

public sealed record LoginCommand(string UserName, string Password) : IRequest<TokenResponseDto>;
