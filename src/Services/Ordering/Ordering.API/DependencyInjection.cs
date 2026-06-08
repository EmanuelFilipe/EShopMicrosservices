namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register API services, e.g. controllers, Swagger, etc.
        // services.AddControllers();
        // services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        //app.MapCarter();

        return app;
    }
}
