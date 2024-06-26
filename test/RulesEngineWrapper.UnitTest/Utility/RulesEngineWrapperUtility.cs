using CodenameGenerator;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngineWrapper;
using RulesEngineWrapper.Presentation;

public static class RulesEngineWrapperUtility
{
    public static Generator _generator = new Generator();

    public static Workflow NewWorkflow()
    {
        return new Workflow
        {
            WorkflowName = _generator.Generate(),
            Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName = _generator.Generate(),
                    RuleExpressionType = RuleExpressionType.LambdaExpression,
                    Expression = "1 < 5",
                }
            }
        };
    }
}
