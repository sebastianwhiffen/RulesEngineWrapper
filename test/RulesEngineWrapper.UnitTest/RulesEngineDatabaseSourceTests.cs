using Microsoft.Extensions.DependencyInjection;
using RulesEngineWrapper.presentation;
using RulesEngine.Data;
using Microsoft.EntityFrameworkCore;

namespace RulesEngineWrapper.UnitTest;
public class RulesEngineDatabaseSourceTests
{
    [Fact]
    public void TestDbQuery()
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

        Assert.NotNull(serviceProvider.GetService<IRulesEngineWrapper>());
        Assert.IsType<DatabaseSourceRepository>(serviceProvider.GetService<IDataSourceRepository>());

        // Additional assertions to test table population and querying
        var dbContext = serviceProvider.GetService<RulesEngineContext>();
        var workflowEntities = dbContext.Workflows.ToList();

        Assert.NotEmpty(workflowEntities); // Verify that the table is populated

        var queryResult = dbContext.Workflows
            .Where(w => w.WorkflowName == "SomeWorkflowName")
            .FirstOrDefault();

        Assert.NotNull(queryResult); // Verify that you can query the table
    }
}

