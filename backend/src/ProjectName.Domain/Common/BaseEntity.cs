namespace ProjectName.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public string TenantId { get; protected set; } = string.Empty;
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public string CreatedBy { get; protected set; } = "system";

    protected void SetAuditUpdate() => UpdatedAt = DateTime.UtcNow;

    public void SetTenant(string tenantId)
    {
        TenantId = tenantId;
    }
}
