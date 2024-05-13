using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace RulesEngineWrappers
{
    public interface IRulesEngineWrapper
    {
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs);
        public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams);
        public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters);
        public Task<bool> RemoveWorkflow(params string[] workflowNames);
        public Task<bool> AddOrUpdateWorkflow(params Workflow[] Workflows);
        public Task<bool> AddWorkflow(params Workflow[] Workflows);
        public Task<IEnumerable<string>> GetAllWorkflowNames();
    }
}
