using FamilyBudget.Infrastructure.Middleware;

namespace FamilyBudget.Api;

public static class Extensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options => options.AddPolicy(
            "CorsPolicy",
            policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:5173")
        ));

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapControllers();
        app.MapGet("/", context => context.Response.WriteAsync(
            $"Family Budget is running!\nGo to: {app.Urls.Select(x => x).First()}/docs"));

        app.UseCors("CorsPolicy");

        return app;
    }
}