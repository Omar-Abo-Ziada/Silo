using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Silo.API.Common.Controller;
using Silo.API.Common.Request;
using Silo.API.Payment.Braintree;
using Silo.API.Presistance.Contexts.Repositories.Common;
using Silo.API.Servies.User;
using System.Reflection;

namespace Silo.API.Configurations.Extensions;

public static class Services
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BraintreeSettings>(configuration.GetSection(nameof(BraintreeSettings)));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddSingleton<IBraintreeGate, BraintreeGate>();

        services.AddScoped<ITokenHelper, TokenHelper>();

        services.AddScoped<IUserStateService, UserStateService>();

        services.AddHttpContextAccessor();
        services.AddMemoryCache();
        services.AddValidatorsFromAssemblyContaining<Program>(); // Scans current assembly

        services.AddScoped(typeof(BaseControllerParams<>));
        services.AddScoped(typeof(RequestHandlerBaseParameters));

        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(Services).Assembly));

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        //services.AddSingleton<IMapper, Mapper>();

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped<IMapperHelper, MapperHelper>();
        return services;
    }

    public static IServiceCollection AddLogs(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        // Clear default logging providers
        builder.Logging.ClearProviders();

        var outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}";


        // Configure Serilog entirely in code
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)

            // Console sink
            .WriteTo.Console(outputTemplate: outputTemplate)

         // File sink
         .WriteTo.File(
                path: "Logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1),
                outputTemplate: outputTemplate
            )


            // SQL Server sink
            .WriteTo.MSSqlServer(
                connectionString: configuration.GetConnectionString("Default"),
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true,
                    AutoCreateSqlDatabase = true
                },
                restrictedToMinimumLevel: LogEventLevel.Information
            )

            // Seq sink
            .WriteTo.Seq("http://localhost:5341/")

            .CreateLogger();

        // Plug Serilog into host
        builder.Host.UseSerilog();

        builder.Services.AddScoped(typeof(ILoggerHelper<>), typeof(LoggerHelper<>));

        return services;
    }
}