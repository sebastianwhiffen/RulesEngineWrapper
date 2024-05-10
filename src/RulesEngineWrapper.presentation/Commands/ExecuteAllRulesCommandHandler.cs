using MediatR;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper;

public class ExecuteAllRulesCommand() : IRequest<List<RuleResultTree>>
{
    public string WorkflowName { get; private set; }
    public List<RuleParameter> RuleParams { get; private set; } = [];
    public IRulesEngine RulesEngine { get; private set; }
    public ExecuteAllRulesCommand(IRulesEngine rulesEngine, string workflowName, params object[] inputs) : this()
    {
        RulesEngine = rulesEngine;

        WorkflowName = workflowName;

        for (var i = 0; i < inputs.Length; i++)
        {
            var input = inputs[i];
            RuleParams.Add(new RuleParameter($"input{i + 1}", input));
        }
    }
    public ExecuteAllRulesCommand(IRulesEngine rulesEngine, string workflowName, params RuleParameter[] ruleParams) : this()
    {
        RulesEngine = rulesEngine;
        WorkflowName = workflowName;
        RuleParams = ruleParams.ToList();
    }
}

public class ExecuteAllRulesCommandHandler : IRequestHandler<ExecuteAllRulesCommand, List<RuleResultTree>>
{
    public ExecuteAllRulesCommandHandler()
    {
    }

    public async Task<List<RuleResultTree>> Handle(ExecuteAllRulesCommand request, CancellationToken cancellationToken)
    {
        return await request.RulesEngine.ExecuteAllRulesAsync(request.WorkflowName, request.RuleParams.ToArray());
    }
}