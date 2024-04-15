using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public class FileSourceRepository : IDataSourceRepository
{
    public FileSourceRepository()
    {
    }

    public Task<Workflow> AddOrUpdateWorkflows(Workflow workflow)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Rule>> GetAllRulesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> GetAllWorkflowsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Rule> GetRuleAsync(string ruleName)
    {
        throw new NotImplementedException();
    }

    public async Task<Workflow> GetWorkflowAsync(string workflowName)
    {
        throw new NotImplementedException();
    }

    public void RemoveWorkflowByName(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveWorkflowByNameAsync(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<object> RunActionWorkflow(ExecuteActionWorkflowCommand executeAllRulesCommand)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RuleResultTree>> RunAllRulesAsync(ExecuteAllRulesCommand executeAllRulesCommand)
    {
        throw new NotImplementedException();
    }
}
