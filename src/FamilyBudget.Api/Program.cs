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

services
    .AddApi()
    .AddInfrastructure()
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