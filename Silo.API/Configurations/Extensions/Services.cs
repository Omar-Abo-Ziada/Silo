
namespace Silo.API.Configurations.Extensions;

public static class Services
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, TokenHelper>();

        return services;
    }
}