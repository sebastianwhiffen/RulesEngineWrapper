using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;
using RulesEngineWrapper.Infrastructure;
using RulesEngineWrapper.presentation;

namespace RulesEngineWrapper;

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

    public RulesEngineWrapper(IEnumerable<WorkflowEntity> workflows, RulesEngineWrapperOptions options, IDataSourceRepository dataSourceRepository)
       : this(workflows.ToWorkflows(), options, dataSourceRepository) { }


    public async Task<IEnumerable<Workflow>> AddWorkflowToDataSource(IEnumerable<Workflow> workflows)
    {
        var workflowEntities = await _dataSourceRepository.AddWorkflow(workflows.ToWorkflowEntities());

        return workflowEntities.ToWorkflows();
    }
    public async Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInDataSource(IEnumerable<Workflow> workflows)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInCache(IEnumerable<Workflow> workflows)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> AddWorkflowToCache(IEnumerable<Workflow> workflow)
    {
        throw new NotImplementedException();
    }

    public async Task<Workflow> RemoveWorkflowFromDataSource(string workflowName)
    {
        throw new NotImplementedException();
    }

    public async Task<Workflow> RemoveWorkflowFromCache(string workflowName)
    {
        throw new NotImplementedException();
    }

    public async Task<Workflow> GetWorkflowFromDataSource(string workflowName)
    {
        throw new NotImplementedException();
    }

    public async Task<Workflow> GetWorkflowFromCache(string workflowName)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> GetAllWorkflowsFromDataSource()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> GetAllWorkflowsFromCache()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> ClearWorkflowsFromDataSource()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workflow>> ClearWorkflowsFromCache()
    {
        throw new NotImplementedException();
    }


    public async ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
    {
        throw new NotImplementedException();
    }

    public async void AddWorkflow(params Workflow[] Workflows)
    {
        throw new NotImplementedException();
    }

    public async void ClearWorkflows()
    {
        throw new NotImplementedException();
    }

    public async void RemoveWorkflow(params string[] workflowNames)
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

    public async void AddOrUpdateWorkflow(params Workflow[] Workflows)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region public methods

    #endregion
}
