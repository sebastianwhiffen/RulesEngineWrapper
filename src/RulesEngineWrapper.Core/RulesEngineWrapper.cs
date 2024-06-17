using RulesEngine.Models;

namespace RulesEngineWrapper
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper
    {
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
        {
            return GetWorkflowService().ExecuteAllRulesAsync(workflowName, inputs);
        }
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
        {
            return GetWorkflowService().ExecuteAllRulesAsync(workflowName, ruleParams);
        }
        public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
        {
            return GetWorkflowService().ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
        }
        public void RemoveWorkflow(params string[] workflowNames) => GetWorkflowService().RemoveWorkflow(workflowNames);
        public void AddOrUpdateWorkflow(params Workflow[] workflows) => GetWorkflowService().AddOrUpdateWorkflow(workflows);
        public void AddWorkflow(params Workflow[] workflows) => GetWorkflowService().AddWorkflow(workflows);
        public List<string> GetAllRegisteredWorkflowNames() => GetWorkflowService().GetAllRegisteredWorkflowNames();
        public bool ContainsWorkflow(string workflowName) => GetWorkflowService().ContainsWorkflow(workflowName);
        public void ClearWorkflows() => GetWorkflowService().ClearWorkflows();
    }
}
