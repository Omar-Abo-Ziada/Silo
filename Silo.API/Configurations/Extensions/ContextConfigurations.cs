
namespace Silo.API.Configurations.Extensions;

public static class ContextConfigurations
{
    public static IServiceCollection AddGeneralDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GeneralDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("Default"))
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        return services;
    }
}