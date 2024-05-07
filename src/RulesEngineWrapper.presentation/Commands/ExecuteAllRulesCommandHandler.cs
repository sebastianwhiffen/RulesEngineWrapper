using MediatR;
using RulesEngine.Models;
using RulesEngineWrapper;

public class ExecuteAllRulesCommand() : IRequest<List<RuleResultTree>>
{
    public string WorkflowName { get; private set; }
    public List<RuleParameter> RuleParams { get; private set; }
    public ExecuteAllRulesCommand(string workflowName, params object[] inputs) : this()
    {
        WorkflowName = workflowName;

        for (var i = 0; i < inputs.Length; i++)
        {
            var input = inputs[i];
            RuleParams.Add(new RuleParameter($"input{i + 1}", input));
        }
    }
    public ExecuteAllRulesCommand(string workflowName, params RuleParameter[] ruleParams) : this()
    {
        WorkflowName = workflowName;
        RuleParams = ruleParams.ToList();
    }
}

public class ExecuteAllRulesCommandHandler : IRequestHandler<ExecuteAllRulesCommand, List<RuleResultTree>>
{
    private readonly IRulesEngineWrapper _rulesEngineWrapper;

    public ExecuteAllRulesCommandHandler(IRulesEngineWrapper rulesEngineWrapper)
    {
        _rulesEngineWrapper = rulesEngineWrapper;
    }

    public async Task<List<RuleResultTree>> Handle(ExecuteAllRulesCommand request, CancellationToken cancellationToken)
    {

        return await _rulesEngineWrapper.ExecuteAllRulesAsync(request.WorkflowName, request.RuleParams.ToArray());
    }
}