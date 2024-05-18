using RulesEngine.Models;

namespace RulesEngineWrappers
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper
    {
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
        {
            throw new NotImplementedException();
        }
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
        {
            throw new NotImplementedException();
        }
        public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
        {
            throw new NotImplementedException();
        }
        public void RemoveWorkflow(params string[] workflowNames) => OnRemoveWorkflow?.Invoke(this, workflowNames);
        public void AddOrUpdateWorkflow(params Workflow[] workflows) => OnAddOrUpdateWorkflow?.Invoke(this, workflows);
        public void AddWorkflow(params Workflow[] workflows) => OnAddWorkflow?.Invoke(this,workflows);
        public List<string> GetAllRegisteredWorkflowNames() => _workflowService.GetAllRegisteredWorkflowNames();
        public bool ContainsWorkflow(string workflowName) => _workflowService.ContainsWorkflow(workflowName);
        public void ClearWorkflows() => _workflowService.ClearWorkflows();
    }
}
