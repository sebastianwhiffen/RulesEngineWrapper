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

    private RulesEngineWrapper(ReSettings options, IMediator mediator)
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

    #endregion
    #region commands
    // public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs) =>
    //     _mediator.Send(new ExecuteAllRulesCommand(workflowName, inputs));
   

    // public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams) =>
    //     _mediator.Send(new ExecuteAllRulesCommand(workflowName, ruleParams));

    // public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters) =>
    //  _mediator.Send(new ExecuteActionWorkflowCommand(workflowName, ruleName, ruleParameters));


    // public void ClearWorkflows() => _mediator.Send(new ClearWorkflowsCommand());


    public async Task<bool> RemoveWorkflow(params string[] workflowNames) => await _mediator.Send(new RemoveWorkflowCommand(workflowNames));


    // public bool ContainsWorkflow(string workflowName) => _mediator.Send(new ContainsWorkflowQuery(workflowName));
   
    public async Task<bool> AddOrUpdateWorkflow(params Workflow[] Workflows) => await _mediator.Send(new AddOrUpdateWorkflowCommand(Workflows));

    public async Task<bool> AddWorkflow(params Workflow[] Workflows) => await _mediator.Send(new AddWorkflowCommand(Workflows));

    #endregion
    
    #region queries
    // public List<string> GetAllRegisteredWorkflowNames() => _mediator.Send(new GetAllRegisteredWorkflowNamesQuery());
    



    #endregion

}
