using RulesEngine.Models;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public interface IDataSourceRepository
{
    Task<IEnumerable<Workflow>> GetAllWorkflowsAsync();
    Task<IEnumerable<Rule>> GetAllRulesAsync();
    Task<Rule> GetRuleAsync(string ruleName);
    Task<Workflow> GetWorkflowAsync(string workflowName);
    Task<IEnumerable<RuleResultTree>> RunAllRulesAsync(ExecuteAllRulesCommand executeAllRulesCommand);
    Task<object> RunActionWorkflow(ExecuteActionWorkflowCommand executeAllRulesCommand);
    Task<Workflow> AddOrUpdateWorkflows(Workflow workflow);
    Task<bool> RemoveWorkflowByNameAsync(string workflowName);
}