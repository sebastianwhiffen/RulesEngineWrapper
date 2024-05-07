using MediatR;
using RulesEngine.Models;
using RulesEngineWrapper;

public record ExecuteActionWorkflowCommand(string WorkflowName, string RuleName, RuleParameter[] RuleParameters) : IRequest<ActionRuleResult>;

public class ExecuteActionWorkflowCommandHandler : IRequestHandler<ExecuteActionWorkflowCommand, ActionRuleResult>
{
    private readonly IRulesEngineWrapper _rulesEngineWrapper;

    public ExecuteActionWorkflowCommandHandler(IRulesEngineWrapper rulesEngineWrapper)
    {
        _rulesEngineWrapper = rulesEngineWrapper;
    }

    public async Task<ActionRuleResult> Handle(ExecuteActionWorkflowCommand request, CancellationToken cancellationToken)
    {
        return await _rulesEngineWrapper.ExecuteActionWorkflowAsync(request.WorkflowName, request.RuleName, request.RuleParameters);
    }
}