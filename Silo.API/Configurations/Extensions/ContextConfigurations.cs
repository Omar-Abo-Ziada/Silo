
using System.Diagnostics;

namespace Silo.API.Configurations.Extensions;

public static class ContextConfigurations
{
    public static IServiceCollection AddGeneralDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GeneralDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("Default"))
             .LogTo(action => Debug.WriteLine(action), LogLevel.Information)
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        return services;
    }
}