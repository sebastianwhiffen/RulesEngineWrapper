using RulesEngine.Interfaces;
using RulesEngine.Models;

public class WorkflowService : IWorkflowService
{
    private readonly IRulesEngine _rulesEngine;
    public WorkflowService(IRulesEngine rulesEngine)
    {
        _rulesEngine = rulesEngine;
    }
    public virtual void AddOrUpdateWorkflow(params Workflow[] workflows) => _rulesEngine.AddOrUpdateWorkflow(workflows);
    public void AddWorkflow(params Workflow[] workflows) => _rulesEngine.AddWorkflow(workflows);
    public void ClearWorkflows() => _rulesEngine.ClearWorkflows();
    public bool ContainsWorkflow(string workflowName) => _rulesEngine.ContainsWorkflow(workflowName);
    public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
    => _rulesEngine.ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
    => _rulesEngine.ExecuteAllRulesAsync(workflowName, inputs);
    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
     => _rulesEngine.ExecuteAllRulesAsync(workflowName, ruleParams);
    public List<string> GetAllRegisteredWorkflowNames() => _rulesEngine.GetAllRegisteredWorkflowNames();
    public void RemoveWorkflow(params string[] workflowNames) => _rulesEngine.RemoveWorkflow(workflowNames);
}