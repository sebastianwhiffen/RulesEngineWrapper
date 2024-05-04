using RulesEngine.Models;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using Microsoft.EntityFrameworkCore;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace RulesEngineWrapper.UnitTest;

public class RulesEngineWrapperTests()
{
    [Theory]
    [MemberData(nameof(ContainerUtility.GetServiceSetups), MemberType = typeof(ContainerUtility))]
    public async void AddWorkflowToDataSource_ShouldWorkCorrectly(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var rulesEngineWrapper = serviceProvider.GetRequiredService<IRulesEngineWrapper>();

        var workflows = new List<Workflow>
        {
            new Workflow
            {
                WorkflowName = "Test Workflow",
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        RuleName = "Test Rule",
                        RuleExpressionType = RuleExpressionType.LambdaExpression,
                        Expression = "1 < 5",
                    }
                }
            }
        };

        // Act
        workflows.ForEach(w => rulesEngineWrapper.AddWorkflow(w));

        //assert

        var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<RulesEngineContext>();
        var workflow = await context.Workflows.Include(w => w.Rules).FirstOrDefaultAsync(w => w.WorkflowName == "Test Workflow");

        Assert.NotNull(workflow);
        Assert.Equal("Test Workflow", workflow.WorkflowName);
    }
}
