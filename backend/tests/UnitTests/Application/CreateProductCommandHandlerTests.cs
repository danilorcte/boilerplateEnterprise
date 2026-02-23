using FluentAssertions;
using Moq;
using ProjectName.Application.Abstractions;
using ProjectName.Application.UseCases.Products.CreateProduct;
using ProjectName.Domain.Repositories;

namespace UnitTests.Application;

public sealed class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateProduct_ForCurrentTenant()
    {
        // Arrange
        var repository = new Mock<IProductRepository>();
        var tenantContext = new Mock<ITenantContext>();
        tenantContext.SetupGet(x => x.TenantId).Returns("tenant-01");
        tenantContext.SetupGet(x => x.UserId).Returns("user-01");

        var handler = new CreateProductCommandHandler(repository.Object, tenantContext.Object);
        var command = new CreateProductCommand("Keyboard", 199.90m);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        repository.Verify(x => x.AddAsync(It.IsAny<ProjectName.Domain.Entities.Product>(), CancellationToken.None), Times.Once);
        result.Name.Should().Be("Keyboard");
        result.Price.Should().Be(199.90m);
    }
}
