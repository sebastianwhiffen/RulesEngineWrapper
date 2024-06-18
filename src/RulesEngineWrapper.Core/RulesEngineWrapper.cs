using RulesEngine.Models;

namespace RulesEngineWrapper
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper
    {
        public RulesEngineWrapperServices RulesEngineWrapperServices { get; set; } = new RulesEngineWrapperServices();

        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
        {
            return RulesEngineWrapperServices.GetWorkflowService().ExecuteAllRulesAsync(workflowName, inputs);
        }
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
        {
            return RulesEngineWrapperServices.GetWorkflowService().ExecuteAllRulesAsync(workflowName, ruleParams);
        }
        public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
        {
            return RulesEngineWrapperServices.GetWorkflowService().ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
        }
        public void RemoveWorkflow(params string[] workflowNames) => RulesEngineWrapperServices.GetWorkflowService().RemoveWorkflow(workflowNames);
        public void AddOrUpdateWorkflow(params Workflow[] workflows) => RulesEngineWrapperServices.GetWorkflowService().AddOrUpdateWorkflow(workflows);
        public void AddWorkflow(params Workflow[] workflows) => RulesEngineWrapperServices.GetWorkflowService().AddWorkflow(workflows);
        public List<string> GetAllRegisteredWorkflowNames() => RulesEngineWrapperServices.GetWorkflowService().GetAllRegisteredWorkflowNames();
        public bool ContainsWorkflow(string workflowName) => RulesEngineWrapperServices.GetWorkflowService().ContainsWorkflow(workflowName);
        public void ClearWorkflows() => RulesEngineWrapperServices.GetWorkflowService().ClearWorkflows();
    }
}
