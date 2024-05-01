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
        //collection params coming soon I hope...
        AddOrUpdateWorkflow(workflows.ToArray());
    }

    public RulesEngineWrapper(IEnumerable<WorkflowEntity> workflows, RulesEngineWrapperOptions options, IMediator mediator)
        : this(workflows.ToWorkflows(), options, mediator) { }

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
        _mediator.Send(new AddOrUpdateWorkflowCommand(Workflows));
    }

    public void AddWorkflow(params Workflow[] Workflows)
    {
        _mediator.Send(new AddWorkflowCommand(Workflows));
    }

    #endregion

}
