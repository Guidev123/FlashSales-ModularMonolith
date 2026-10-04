using Modules.ArchitectureTests.Abstractions;
using NetArchTest.Rules;

namespace Modules.ArchitectureTests.Layers;

public sealed class ModuleTests : BaseTest
{
    [Fact(DisplayName = "Catalog Module Should Only Depend On Contracts Of Other Modules")]
    [Trait("Architecture Tests", "Module Tests")]
    public void CatalogModule_ShouldOnlyDependOn_ContractsOfOtherModules() =>
        ShouldNotDependOnInternalsOfOtherModules(Catalog);

    [Fact(DisplayName = "Launches Module Should Only Depend On Contracts Of Other Modules")]
    [Trait("Architecture Tests", "Module Tests")]
    public void LaunchesModule_ShouldOnlyDependOn_ContractsOfOtherModules() =>
        ShouldNotDependOnInternalsOfOtherModules(Launches);

    [Fact(DisplayName = "Orders Module Should Only Depend On Contracts Of Other Modules")]
    [Trait("Architecture Tests", "Module Tests")]
    public void OrdersModule_ShouldOnlyDependOn_ContractsOfOtherModules() =>
        ShouldNotDependOnInternalsOfOtherModules(Orders);

    [Fact(DisplayName = "Payments Module Should Only Depend On Contracts Of Other Modules")]
    [Trait("Architecture Tests", "Module Tests")]
    public void PaymentsModule_ShouldOnlyDependOn_ContractsOfOtherModules() =>
        ShouldNotDependOnInternalsOfOtherModules(Payments);

    [Fact(DisplayName = "Users Module Should Only Depend On Contracts Of Other Modules")]
    [Trait("Architecture Tests", "Module Tests")]
    public void UsersModule_ShouldOnlyDependOn_ContractsOfOtherModules() =>
        ShouldNotDependOnInternalsOfOtherModules(Users);

    [Fact(DisplayName = "Contracts Should Not Have Dependency On Any Other Module")]
    [Trait("Architecture Tests", "Module Tests")]
    public void Contracts_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        foreach (ModuleAssemblies module in AllModules)
        {
            // Arrange
            string[] otherModules = AllModules
                .Where(other => other != module)
                .SelectMany(other => other.InternalAssemblyNames)
                .ToArray();

            // Act & Assert
            Types.InAssembly(module.Contracts)
                .Should()
                .NotHaveDependencyOnAny(otherModules)
                .GetResult()
                .ShouldBeSuccessful();
        }
    }

    private static void ShouldNotDependOnInternalsOfOtherModules(ModuleAssemblies module)
    {
        // Arrange
        string[] internalsOfOtherModules = AllModules
            .Where(other => other != module)
            .SelectMany(other => other.InternalAssemblyNames)
            .ToArray();

        // Act & Assert
        Types.InAssemblies(module.InternalAssemblies)
            .Should()
            .NotHaveDependencyOnAny(internalsOfOtherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
