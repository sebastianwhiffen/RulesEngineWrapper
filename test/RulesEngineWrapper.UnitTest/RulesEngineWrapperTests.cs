using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineWrappers.UnitTest;

[Collection("Database collection")]
public class RulesEngineWrapperTests
{
    static RulesEngineWrapperTests()
    {
        TestContainersFixture.EnsureInitializedAsync().GetAwaiter().GetResult();
    }
    public static IEnumerable<object[]> rulesEngineWrappers => TestContainersFixture._containers.Select(container => new object[] { _factory.Create(container).BuildServiceProvider().GetRequiredService<IRulesEngineWrapper>() });

    
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
    public async Task AddWorkflow_ShouldntWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");

        await Assert.ThrowsAsync<DbUpdateException>(async () => await rulesEngineWrapper.AddWorkflow(workflow));
    }

    [Theory]
    [MemberData(nameof(rulesEngineWrappers))]
    public async Task AddOrUpdateWorkflow_ShouldWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        await rulesEngineWrapper.AddOrUpdateWorkflow(workflow);
    }

    [Theory]
    [MemberData(nameof(rulesEngineWrappers))]
    public async Task RemoveWorkflow_ShouldWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();

        Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");
        Assert.True(await rulesEngineWrapper.RemoveWorkflow(workflow.WorkflowName), "Workflow should be removed successfully");
    }

    [Theory]
    [MemberData(nameof(rulesEngineWrappers))]
    public async Task GetAllWorkflowNames_ShouldWork(IRulesEngineWrapper rulesEngineWrapper)
    {
        var workflow = RulesEngineWrapperUtility.NewWorkflow();
        var workflow1 = RulesEngineWrapperUtility.NewWorkflow();

        foreach (var wf in new[] { workflow, workflow1 })
        {
            Assert.True(await rulesEngineWrapper.AddWorkflow(wf), "Workflow should be added successfully");
        }

        Assert.True((await rulesEngineWrapper.GetAllWorkflowNames()).Count() > 1, "There should be more than one workflow");
    }
}