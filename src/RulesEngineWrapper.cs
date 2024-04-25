using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;
using RulesEngineWrapper.presentation;
using RulesEngineWrapper.presentation.Options;

namespace RulesEngineWrapper;

public interface IRulesEngineWrapper: IRulesEngine 
{
    public Task<Workflow> AddWorkflowToDataSource(IEnumerable<Workflow> workflow);
    public Task<Workflow> AddWorkflowToCache(IEnumerable<Workflow> workflow);
    public Task<Workflow> RemoveWorkflowFromDataSource(string workflowName);
    public Task<Workflow> RemoveWorkflowFromCache(string workflowName);
    public Task<Workflow> GetWorkflowFromDataSource(string workflowName);
    public Task<Workflow> GetWorkflowFromCache(string workflowName);
    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromDataSource();
    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromCache();
    public Task<IEnumerable<Workflow>> ClearWorkflowsFromDataSource();
    public Task<IEnumerable<Workflow>> ClearWorkflowsFromCache();
    public Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInDataSource(IEnumerable<Workflow> workflows);
    public Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInCache(IEnumerable<Workflow> workflows);

}

public class RulesEngineWrapper : IRulesEngineWrapper
{
    #region constructors
    private readonly IRulesEngine _rulesEngine;
    private readonly IDataSourceRepository _dataSourceRepository;

    private RulesEngineWrapper(ReSettings options, IDataSourceRepository dataSourceRepository)
    {
        _rulesEngine = new RulesEngine.RulesEngine(options);
        _dataSourceRepository = dataSourceRepository;
    }

    public RulesEngineWrapper(IEnumerable<Workflow> workflows, RulesEngineWrapperOptions options, IDataSourceRepository dataSourceRepository)
        : this(options.reSettings, dataSourceRepository)
    {
        AddWorkflowToDataSource(workflows);
        AddWorkflowToCache(workflows);
    }

    public Task<Workflow> AddWorkflowToDataSource(IEnumerable<Workflow> workflow)
    {
        throw new NotImplementedException();
    }

    public Task<Workflow> AddWorkflowToCache(IEnumerable<Workflow> workflow)
    {
        throw new NotImplementedException();
    }

    public Task<Workflow> RemoveWorkflowFromDataSource(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<Workflow> RemoveWorkflowFromCache(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<Workflow> GetWorkflowFromDataSource(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<Workflow> GetWorkflowFromCache(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromDataSource()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromCache()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Workflow>> ClearWorkflowsFromDataSource()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Workflow>> ClearWorkflowsFromCache()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInDataSource(IEnumerable<Workflow> workflows)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInCache(IEnumerable<Workflow> workflows)
    {
        throw new NotImplementedException();
    }

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

    public void AddWorkflow(params Workflow[] Workflows)
    {
        throw new NotImplementedException();
    }

    public void ClearWorkflows()
    {
        throw new NotImplementedException();
    }

    public void RemoveWorkflow(params string[] workflowNames)
    {
        throw new NotImplementedException();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        throw new NotImplementedException();
    }

    public List<string> GetAllRegisteredWorkflowNames()
    {
        throw new NotImplementedException();
    }

    public void AddOrUpdateWorkflow(params Workflow[] Workflows)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region public methods

    #endregion
}
