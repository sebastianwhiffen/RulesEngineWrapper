using RulesEngine.Models;

namespace RulesEngineWrapper.presentation.Queries;

public class RulesEngineQueries : IRulesEngineQueries
{
    private readonly IDataSourceRepository _dataSourceRepository;
    public RulesEngineQueries(IDataSourceRepository dataSourceRepository)
    {
        _dataSourceRepository = dataSourceRepository;
    }
    public async Task<IEnumerable<Rule>> GetAllRulesAsync()
    {
        return await _dataSourceRepository.GetAllRulesAsync();
    }

    public async Task<IEnumerable<Workflow>> GetAllWorkflowsAsync()
    {
        return await _dataSourceRepository.GetAllWorkflowsAsync();
    }

    public async Task<Rule> GetRuleAsync(string ruleName)
    {
        return await _dataSourceRepository.GetRuleAsync(ruleName);
    }

    public async Task<Workflow> GetWorkflowAsync(string workflowName)
    {
        return await _dataSourceRepository.GetWorkflowAsync(workflowName);
    }
}
