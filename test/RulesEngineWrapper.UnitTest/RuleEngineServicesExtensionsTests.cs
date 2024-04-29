using Microsoft.Extensions.DependencyInjection;
using RulesEngineWrapper.presentation;
using RulesEngine.Data;
using Microsoft.EntityFrameworkCore;
using RulesEngineWrapper.Infrastructure;

namespace RulesEngineWrapper.UnitTest;
public class RuleEngineServicesExtensionsTests
{
    [Fact]
    public void Register_RulesEngineWrapper_File()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddRulesEngineWrapper(callingAssembly : typeof(RuleEngineServicesExtensionsTests).Assembly);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        Assert.NotNull(serviceProvider.GetService<IDataSourceRepository>());
        Assert.IsType<FileSourceRepository>(serviceProvider.GetService<IDataSourceRepository>());
    }

    [Fact]
    public void Register_RulesEngineWrapper_Database_SQLite()
    {
        // Arrange
        var services = new ServiceCollection();

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}RulesEngineWrapperTest.db";

        // Act
        services.AddRulesEngineWrapper<RulesEngineContext>(options =>
        {
            options.WrapperDbEnsureCreated = true;
            options.DbContextOptionsAction = dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseSqlite($"Data Source={DbPath}");
            };
        }, callingAssembly: typeof(RuleEngineServicesExtensionsTests).Assembly);

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IRulesEngineWrapper>());
        Assert.IsType<DatabaseSourceRepository>(serviceProvider.GetService<IDataSourceRepository>());
    }
}

