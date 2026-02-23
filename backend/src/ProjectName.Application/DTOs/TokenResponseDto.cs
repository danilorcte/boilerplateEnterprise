namespace ProjectName.Application.DTOs;

public sealed record TokenResponseDto(string AccessToken, string RefreshToken, int ExpiresInSeconds);
