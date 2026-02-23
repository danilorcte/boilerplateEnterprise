using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProjectName.Application.Abstractions;

namespace ProjectName.Infrastructure.MultiTenancy;

public sealed class HttpTenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpTenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string TenantId => _httpContextAccessor.HttpContext?.User.FindFirst("tenant_id")?.Value
                              ?? _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].ToString()
                              ?? "default";

    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";

    public IReadOnlyCollection<string> Roles => _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray()
                                                 ?? Array.Empty<string>();

    public IReadOnlyCollection<string> Permissions => _httpContextAccessor.HttpContext?.User.FindAll("permission").Select(x => x.Value).ToArray()
                                                       ?? Array.Empty<string>();
}
