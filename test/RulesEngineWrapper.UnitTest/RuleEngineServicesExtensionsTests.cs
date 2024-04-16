using Microsoft.Extensions.DependencyInjection;
using RulesEngineWrapper.presentation;
using RulesEngine.Data;
using Microsoft.EntityFrameworkCore;

namespace RulesEngineWrapper.UnitTest;
public class RuleEngineServicesExtensionsTests
{
    [Fact]
    public void Register_RulesEngineWrapper_File()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddRulesEngineWrapper();

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
        });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        Assert.NotNull(serviceProvider.GetService<IDataSourceRepository>());
        Assert.IsType<DatabaseRulesEngineRepository>(serviceProvider.GetService<IDataSourceRepository>());
    }
}

