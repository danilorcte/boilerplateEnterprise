using ProjectName.Domain.Common;

namespace ProjectName.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    public Product(string name, decimal price, string createdBy)
    {
        Name = name;
        Price = price;
        CreatedBy = createdBy;
        Active = true;
    }

    public void Update(string name, decimal price)
    {
        Name = name;
        Price = price;
        SetAuditUpdate();
    }

    public void Inactivate()
    {
        Active = false;
        SetAuditUpdate();
    }
}
