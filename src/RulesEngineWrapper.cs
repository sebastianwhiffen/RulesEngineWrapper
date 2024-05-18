using RulesEngine.Models;

namespace RulesEngineWrappers
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper
    {
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
        {
            return _workflowService.ExecuteAllRulesAsync(workflowName, inputs);
        }
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
        {
            return _workflowService.ExecuteAllRulesAsync(workflowName, ruleParams);
        }
        public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
        {
            return _workflowService.ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
        }
        public void RemoveWorkflow(params string[] workflowNames) => OnRemoveWorkflow?.Invoke(this, workflowNames);
        public void AddOrUpdateWorkflow(params Workflow[] workflows) => OnAddOrUpdateWorkflow?.Invoke(this, workflows);
        public void AddWorkflow(params Workflow[] workflows) => OnAddWorkflow?.Invoke(this,workflows);
        public List<string> GetAllRegisteredWorkflowNames() => _workflowService.GetAllRegisteredWorkflowNames();
        public bool ContainsWorkflow(string workflowName) => _workflowService.ContainsWorkflow(workflowName);
        public void ClearWorkflows() => _workflowService.ClearWorkflows();
    }
}
