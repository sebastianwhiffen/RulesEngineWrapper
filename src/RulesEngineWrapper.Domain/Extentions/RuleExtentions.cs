
using System.Diagnostics;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;

public static class RuleExtentions
{

     public static IEnumerable<Rule> ToRules (this IEnumerable<RuleEntity> ruleEntities)
    {
        foreach (var ruleEntity in ruleEntities)
        {
            yield return ruleEntity.ToRule();
        }
    }

    public static Rule ToRule(this RuleEntity ruleEntity)
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
            Rules = ruleEntity.Rules?.Select(r => r.ToRule()).ToList(),
            LocalParams = ruleEntity.LocalParams?.Select(p => p.ToScopedParam()).ToList(),
            // Actions = ruleEntity.Actions,
            SuccessEvent = ruleEntity.SuccessEvent
        };
    }

    public static IEnumerable<RuleEntity> ToRuleEntities(this IEnumerable<Rule> rules)
    {
        foreach (var rule in rules)
        {
            yield return rule.ToRuleEntity();
        }
    }

    public static RuleEntity ToRuleEntity(this Rule rule)
    {
        return new RuleEntity
        {
            RuleName = rule.RuleName,
            Expression = rule.Expression,
            Operator = rule.Operator,
            ErrorMessage = rule.ErrorMessage,
            Enabled = rule.Enabled,
            // WorkflowsToInject = rule.WorkflowsToInject.Select(w => new WorkflowEntity { WorkflowName = w }).ToList(),
            Rules = rule.Rules?.Select(r => r.ToRuleEntity()).ToList(),
            LocalParams = rule.LocalParams?.Select(p => p.ToScopedParamEntity()).ToList(),
            // Actions = rule.Actions,
            SuccessEvent = rule.SuccessEvent
        };
    }
}