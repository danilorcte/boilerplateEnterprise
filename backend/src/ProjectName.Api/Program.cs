using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Api.Extensions;
using ProjectName.Api.Middlewares;
using ProjectName.Infrastructure.Logging;
using ProjectName.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddStructuredLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProjectName.Application.UseCases.Products.CreateProduct.CreateProductCommand>());
builder.Services.AddValidatorsFromAssemblyContaining<ProjectName.Application.Validators.CreateProductCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSecurityDefaults(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapGet("/csrf-token", (IAntiforgery antiforgery, HttpContext context) =>
{
    var tokenSet = antiforgery.GetAndStoreTokens(context);
    return Results.Ok(new { token = tokenSet.RequestToken });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
