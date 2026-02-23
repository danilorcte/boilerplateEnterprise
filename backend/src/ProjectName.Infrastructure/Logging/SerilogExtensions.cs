using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProjectName.Infrastructure.Logging;

public static class SerilogExtensions
{
    public static void AddStructuredLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, _, config) => config
            .Enrich.FromLogContext()
            .WriteTo.Console());
    }
}
