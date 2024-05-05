using RulesEngine.Models;
using Microsoft.Extensions.DependencyInjection;
using CodenameGenerator;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace RulesEngineWrapper.UnitTest;

[Collection("Database collection")]
public class RulesEngineWrapperTests
{
    static RulesEngineWrapperTests()
    {
        DatabaseFixture.EnsureInitializedAsync().GetAwaiter().GetResult();
    }

    public static IEnumerable<object[]> ServiceCollections => DatabaseFixture.ServiceCollections.Select(sc => new object[] { sc });

    [Theory]
    [MemberData(nameof(ServiceCollections))]
    public async Task AddWorkflow_ShouldWorkCorrectly(IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var rulesEngineWrapper = scope.ServiceProvider.GetRequiredService<IRulesEngineWrapper>();
            var generator = new Generator();

            var workflow = new Workflow
            {
                WorkflowName = generator.Generate(),
                Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName = generator.Generate(),
                    RuleExpressionType = RuleExpressionType.LambdaExpression,
                    Expression = "1 < 5",
                }
            }
            };

            Assert.True(await rulesEngineWrapper.AddWorkflow(workflow), "Workflow should be added successfully");
        }
    }

    [Theory]
    [MemberData(nameof(ServiceCollections))]
    public async Task AddOrUpdateWorkflow_ShouldWorkCorrectly(IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var rulesEngineWrapper = scope.ServiceProvider.GetRequiredService<IRulesEngineWrapper>();
            var generator = new Generator();

            var workflow = new Workflow
            {
                WorkflowName = generator.Generate(),
                Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName = generator.Generate(),
                    RuleExpressionType = RuleExpressionType.LambdaExpression,
                    Expression = "1 < 5",
                }
            }
            };
            Assert.True(await rulesEngineWrapper.AddOrUpdateWorkflow(workflow), "Workflow should be added successfully");
        }
    }
}