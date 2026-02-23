namespace ProjectName.Application.DTOs;

public sealed record ProductDto(Guid Id, string Name, decimal Price, bool Active);
