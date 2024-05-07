using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineWrapper.UnitTest;

[Collection("Database collection")]
public class RulesEngineWrapperTests
{
    static RulesEngineWrapperTests()
    {
        DatabaseFixture.EnsureInitializedAsync().GetAwaiter().GetResult();
    }
    public static IEnumerable<object[]> ServiceCollections => DatabaseFixture._containers.Select(sc => new object[] { sc });
    public static RulesEngineWrapperFactory _factory = new RulesEngineWrapperFactory();

    [Theory]
    [MemberData(nameof(ServiceCollections))]
    public async Task AddWorkflow_ShouldWork(IContainer container)
    {
        var serviceProvider = _factory.Create(container).BuildServiceProvider();

        var rulesEngineWrapper = serviceProvider.GetRequiredService<IRulesEngineWrapper>();

        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");
    }

    [Theory]
    [MemberData(nameof(ServiceCollections))]
    public async Task AddOrUpdateWorkflow_ShouldWork(IContainer container)
    {
        var serviceProvider = _factory.Create(container).BuildServiceProvider();

        var rulesEngineWrapper = serviceProvider.GetRequiredService<IRulesEngineWrapper>();

        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddOrUpdateWorkflow(workflow), "Workflow should be added successfully");
    }

    [Theory]
    [MemberData(nameof(ServiceCollections))]
    public async Task RemoveWorkflow_ShouldWork(IContainer container)
    {
        var serviceProvider = _factory.Create(container).BuildServiceProvider();

        var rulesEngineWrapper = serviceProvider.GetRequiredService<IRulesEngineWrapper>();

        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");
        Assert.True(await rulesEngineWrapper.RemoveWorkflow(workflow.WorkflowName), "Workflow should be removed successfully");
    }
}