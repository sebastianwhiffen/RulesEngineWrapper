using RulesEngine.Models;

public interface IRulesEngineQueries 
{
    Task<IEnumerable<Rule>> GetAllRulesAsync();
    
    Task<IEnumerable<Workflow>> GetAllWorkflowsAsync();

    Task<Rule> GetRuleAsync(string ruleName);

    Task<Workflow> GetWorkflowAsync(string workflowName);

}