using RulesEngine.Models;
using Microsoft.Extensions.DependencyInjection;
using DotNet.Testcontainers.Containers;
using Testcontainers.PostgreSql;
using RulesEngineWrapper.presentation;
using RulesEngine.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RulesEngineWrapper.UnitTest;
public class RulesEngineWrapperTests
{
    [Theory]
    [MemberData(nameof(ContainerUtility.GetDatabaseConfigurations), MemberType = typeof(ContainerUtility))]
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
    }
}
