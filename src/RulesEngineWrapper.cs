using MediatR;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;
using RulesEngineWrapper.presentation;

namespace RulesEngineWrapper;

public class RulesEngineWrapper : IRulesEngineWrapper
{
    #region constructors
    private readonly IRulesEngine _rulesEngine;
    private readonly IMediator _mediator;


    private RulesEngineWrapper(ReSettings options,  IMediator mediator)
    {
        _rulesEngine = new RulesEngine.RulesEngine(options);
        _mediator = mediator;
    }

    public RulesEngineWrapper(IEnumerable<Workflow> workflows, RulesEngineWrapperOptions options, IMediator mediator)
        : this(options.reSettings, mediator)
    {
        AddWorkflowToDataSource(workflows);
        AddWorkflowToCache(workflows);
    }

    public RulesEngineWrapper(IEnumerable<WorkflowEntity> workflows, RulesEngineWrapperOptions options, IMediator mediator)
        : this(workflows.ToWorkflows(), options, mediator) { }

    #endregion

    #region public methods
    public async Task<bool> AddWorkflowToDataSource(IEnumerable<Workflow> workflows)
    {
        return await _mediator.Send(new AddWorkflowCommand(workflows));
    }

    public Task<bool> AddWorkflowToCache(IEnumerable<Workflow> workflow)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveWorkflowFromDataSource(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveWorkflowFromCache(string workflowName)
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

    public Task<bool> ClearWorkflowsFromDataSource()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ClearWorkflowsFromCache()
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddOrUpdateWorkflowInDataSource(IEnumerable<Workflow> workflows)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddOrUpdateWorkflowInCache(IEnumerable<Workflow> workflows)
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
}
