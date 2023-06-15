using FamilyBudget.Application;
using FamilyBudget.Domain;
using FamilyBudget.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Services

var services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:5173");
        });
});

services
    .AddInfrastructure()
    .AddApplication()
    .AddDomain();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Application

var application = builder.Build();

application
    .UseInfrastructure()
    .UseApplication()
    .UseDomain();

if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI();
}
application.UseHttpsRedirection();
application.UseAuthorization();
application.MapControllers();

application.UseCors("CorsPolicy");

#endregion

application.Run();