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
    public static IEnumerable<object[]> rulesEngineWrappers => DatabaseFixture._containers.Select(container => new object[] { _factory.Create(container).BuildServiceProvider().GetRequiredService<IRulesEngineWrapper>()});
    public static RulesEngineWrapperFactory _factory = new RulesEngineWrapperFactory();

    [Theory]
    [MemberData(nameof(rulesEngineWrappers))]
    public async Task AddWorkflow_ShouldWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");
    }

    [Theory]
    [MemberData(nameof(rulesEngineWrappers))]
    public async Task AddOrUpdateWorkflow_ShouldWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddOrUpdateWorkflow(workflow), "Workflow should be added successfully");
    }

    [Theory]
    [MemberData(nameof(rulesEngineWrappers))]
    public async Task RemoveWorkflow_ShouldWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");
        Assert.True(await rulesEngineWrapper.RemoveWorkflow(workflow.WorkflowName), "Workflow should be removed successfully");
    }
}