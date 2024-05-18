using RulesEngine.Models;

public interface IWorkflowService
{
    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs);
    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams);
    public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters);
    public void RemoveWorkflow(params string[] workflowNames);
    public void AddWorkflow(params Workflow[] workflows);
    public void AddOrUpdateWorkflow(params Workflow[] workflows);
    public List<string> GetAllRegisteredWorkflowNames();
    public bool ContainsWorkflow(string workflowName);
    public void ClearWorkflows();
}