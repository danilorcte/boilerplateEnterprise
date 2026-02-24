using ProjectName.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Application.UseCases.Products.CreateProduct;
using ProjectName.Application.UseCases.Products.ListProducts;

namespace ProjectName.Api.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "Product.Read")]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new ListProductsQuery(), cancellationToken));

    [HttpPost]
    [Authorize(Policy = "Product.Write")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(command, cancellationToken));
}
