using System.Reflection;
using FamilyBudget.Api;
using FamilyBudget.Application;
using FamilyBudget.Domain;
using FamilyBudget.Infrastructure;

var assemblies = new List<Assembly>
{
    typeof(IApiAssembly).Assembly,
    typeof(IApplicationAssembly).Assembly,
    typeof(IDomainAssembly).Assembly,
    typeof(IInfrastructureAssembly).Assembly,
};

var builder = WebApplication.CreateBuilder(args);

#region Services

var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddApi()
    .AddInfrastructure(configuration)
    .AddApplication()
    .AddDomain();

services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies.ToArray()));

#endregion

#region Application

var application = builder.Build();

application
    .UseApi()
    .UseInfrastructure()
    .UseApplication()
    .UseDomain();

#endregion

application.Run();

/*

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
 */