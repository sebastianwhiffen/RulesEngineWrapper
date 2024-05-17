using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrappers.presentation;

namespace RulesEngineWrappers
{
    public interface IRulesEngineWrapper
    {
        public event EventHandler OnAddWorkflow;
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs);
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams);
        public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters);
        public Task<bool> RemoveWorkflow(params string[] workflowNames);
        public Task AddOrUpdateWorkflow(params Workflow[] Workflows);
        public Task AddWorkflow(params Workflow[] Workflows);
        public Task<IEnumerable<string>> GetAllWorkflowNames();
    }
}
