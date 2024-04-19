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
}
