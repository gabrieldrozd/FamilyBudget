using FamilyBudget.Api.Controllers.Base;
using Microsoft.OpenApi.Models;

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
                .WithOrigins("http://localhost:3000")
        ));

        services.AddSwaggerConfiguration();

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapControllers();
        app.MapGet("/", context => context.Response.WriteAsync(
            $"Family Budget is running!\nGo to: {app.Urls.Select(x => x).First()}/docs"));

        app.UseCors("CorsPolicy");

        app.UseSwaggerConfiguration();

        return app;
    }

    private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            foreach (var group in ApiGroups.GetNameValueDictionary())
                options.SwaggerDoc(group.Value, new OpenApiInfo
                {
                    Title = $"{group.Key}",
                    Version = group.Value
                });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securitySchema,
                    new[] { "Bearer" }
                }
            };

            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }

    private static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "docs";
            options.DocumentTitle = "Family Budget API";
            foreach (var group in ApiGroups.GetNameValueDictionary())
                options.SwaggerEndpoint($"/swagger/{group.Value}/swagger.json", $"{group.Key} API");
        });

        return app;
    }
}