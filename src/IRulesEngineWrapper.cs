using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace RulesEngineWrapper;

public interface IRulesEngineWrapper
{
    // public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs);
    // public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams);
    // public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters);
    // public void ClearWorkflows();
    // public void RemoveWorkflow(params string[] workflowNames);
    // public bool ContainsWorkflow(string workflowName);
    public void AddOrUpdateWorkflow(params Workflow[] Workflows);
    public void AddWorkflow(params Workflow[] Workflows);
    // public List<string> GetAllRegisteredWorkflowNames();
}
