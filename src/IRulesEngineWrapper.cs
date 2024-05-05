using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace RulesEngineWrapper;

public interface IRulesEngineWrapper
{
    // public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs);
    // public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams);
    // public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters);
    // public void ClearWorkflows();
    public Task<bool> RemoveWorkflow(params string[] workflowNames);
    // public bool ContainsWorkflow(string workflowName);
    public Task<bool> AddOrUpdateWorkflow(params Workflow[] Workflows);
    public Task<bool> AddWorkflow(params Workflow[] Workflows);
    // public List<string> GetAllRegisteredWorkflowNames();
}
