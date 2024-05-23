
using RulesEngine.Models;
namespace RulesEngineWrappers.Domain;

public static class RuleExtentions
{

     public static IEnumerable<Rule> ToDTOs (this IEnumerable<RuleEntity> ruleEntities)
    {
        foreach (var ruleEntity in ruleEntities)
        {
            yield return ruleEntity.ToDTO();
        }
    }

    public static Rule ToDTO(this RuleEntity ruleEntity)
    {
        return new Rule
        {
            RuleName = ruleEntity.RuleName,
            Expression = ruleEntity.Expression,
            Operator = ruleEntity.Operator,
            ErrorMessage = ruleEntity.ErrorMessage,
            Enabled = ruleEntity.Enabled,
            RuleExpressionType = RuleExpressionType.LambdaExpression,
            // WorkflowsToInject = ruleEntity.WorkflowsToInject.Select(w => w.WorkflowName).ToList(),
            Rules = ruleEntity.Rules?.Select(r => r.ToDTO()).ToList(),
            LocalParams = ruleEntity.LocalParams?.Select(p => p.ToDTO()).ToList(),
            // Actions = ruleEntity.Actions,
            SuccessEvent = ruleEntity.SuccessEvent
        };
    }

    public static IEnumerable<RuleEntity> ToEntities(this IEnumerable<Rule> rules)
    {
        foreach (var rule in rules)
        {
            yield return rule.ToEntity();
        }
    }

    public static RuleEntity ToEntity(this Rule rule)
    {
        return new RuleEntity
        {
            RuleName = rule.RuleName,
            Expression = rule.Expression,
            Operator = rule.Operator,
            ErrorMessage = rule.ErrorMessage,
            Enabled = rule.Enabled,
            // WorkflowsToInject = rule.WorkflowsToInject.Select(w => new WorkflowEntity { WorkflowName = w }).ToList(),
            Rules = rule.Rules?.Select(r => r.ToEntity()).ToList(),
            LocalParams = rule.LocalParams?.Select(p => p.ToEntity()).ToList(),
            // Actions = rule.Actions,
            SuccessEvent = rule.SuccessEvent
        };
    }
}