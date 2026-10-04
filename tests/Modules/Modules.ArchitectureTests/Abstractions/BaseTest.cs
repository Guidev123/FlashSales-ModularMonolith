using System.Reflection;
using CatalogInfrastructure = Modules.Catalog.Infrastructure.CatalogModule;
using LaunchesInfrastructure = Modules.Launches.Infrastructure.LaunchesModule;
using OrdersInfrastructure = Modules.Orders.Infrastructure.OrdersModule;
using PaymentsInfrastructure = Modules.Payments.Infrastructure.PaymentsModule;
using UsersInfrastructure = Modules.Users.Infrastructure.UsersModule;

namespace Modules.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected const string ModulesNamespace = "Modules";

    protected static readonly ModuleAssemblies Catalog = new(
        "Catalog",
        Modules.Catalog.Domain.AssemblyReference.Assembly,
        Modules.Catalog.Application.AssemblyReference.Assembly,
        Modules.Catalog.Contracts.AssemblyReference.Assembly,
        typeof(Modules.Catalog.Endpoints.EndpointsModule).Assembly,
        typeof(CatalogInfrastructure).Assembly);

    protected static readonly ModuleAssemblies Launches = new(
        "Launches",
        Modules.Launches.Domain.AssemblyReference.Assembly,
        Modules.Launches.Application.AssemblyReference.Assembly,
        Modules.Launches.Contracts.AssemblyReference.Assembly,
        typeof(Modules.Launches.Endpoints.EndpointsModule).Assembly,
        typeof(LaunchesInfrastructure).Assembly);

    protected static readonly ModuleAssemblies Orders = new(
        "Orders",
        Modules.Orders.Domain.AssemblyReference.Assembly,
        Modules.Orders.Application.AssemblyReference.Assembly,
        Modules.Orders.Contracts.AssemblyReference.Assembly,
        typeof(Modules.Orders.Endpoints.EndpointsModule).Assembly,
        typeof(OrdersInfrastructure).Assembly);

    protected static readonly ModuleAssemblies Payments = new(
        "Payments",
        Modules.Payments.Domain.AssemblyReference.Assembly,
        Modules.Payments.Application.AssemblyReference.Assembly,
        Modules.Payments.Contracts.AssemblyReference.Assembly,
        typeof(Modules.Payments.Endpoints.EndpointsModule).Assembly,
        typeof(PaymentsInfrastructure).Assembly);

    protected static readonly ModuleAssemblies Users = new(
        "Users",
        Modules.Users.Domain.AssemblyReference.Assembly,
        Modules.Users.Application.AssemblyReference.Assembly,
        Modules.Users.Contracts.AssemblyReference.Assembly,
        typeof(Modules.Users.Endpoints.EndpointsModule).Assembly,
        typeof(UsersInfrastructure).Assembly);

    protected static readonly ModuleAssemblies[] AllModules = [Catalog, Launches, Orders, Payments, Users];

    protected sealed record ModuleAssemblies(
        string Name,
        Assembly Domain,
        Assembly Application,
        Assembly Contracts,
        Assembly Endpoints,
        Assembly Infrastructure)
    {
        /// <summary>
        /// The assemblies that make up the private implementation of the module.
        /// Contracts are intentionally excluded because they are the module's public surface.
        /// </summary>
        public Assembly[] InternalAssemblies => [Domain, Application, Endpoints, Infrastructure];

        public string[] InternalAssemblyNames =>
            [.. InternalAssemblies.Select(assembly => assembly.GetName().Name!)];
    }
}
