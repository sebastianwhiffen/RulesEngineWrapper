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
        AddOrUpdateWorkflow(workflows.ToArray()).GetAwaiter().GetResult();
    }

    public RulesEngineWrapper(IEnumerable<WorkflowEntity> workflows, RulesEngineWrapperOptions options, IMediator mediator)
        : this(workflows.ToWorkflows(), options, mediator) { }

    #endregion

    public async Task<bool> RemoveWorkflow(params string[] workflowNames) => await _mediator.Send(new RemoveWorkflowCommand(workflowNames));

    public async Task<bool> AddOrUpdateWorkflow(params Workflow[] Workflows) => await _mediator.Send(new AddOrUpdateWorkflowCommand(_rulesEngine, Workflows));

    public async Task<bool> AddWorkflow(params Workflow[] Workflows) => await _mediator.Send(new AddWorkflowCommand(Workflows));

    public async Task<IEnumerable<string>> GetAllWorkflowNames() => await _mediator.Send(new GetAllWorkflowNamesQuery());

    public async ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs) => await _mediator.Send(new ExecuteAllRulesCommand(_rulesEngine, workflowName, inputs));

    public async ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams) => await _mediator.Send(new ExecuteAllRulesCommand(_rulesEngine, workflowName, ruleParams));

    public async ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters) => await _mediator.Send(new ExecuteActionWorkflowCommand(workflowName, ruleName, ruleParameters));
}
