using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RulesEngine.Data;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrappers.presentation;

namespace RulesEngineWrappers
{
    public class RulesEngineWrapper : IRulesEngineWrapper
    {
        private readonly IRulesEngine _rulesEngine;
        private readonly IMediator _mediator;
        public RulesEngineWrapper(string[] jsonConfig, RulesEngineWrapperSettings rewSettings = default!) : this(rewSettings)
        {
            var workflows = jsonConfig.Select(JsonConvert.DeserializeObject<Workflow>).ToArray();
            AddOrUpdateWorkflow(workflows!).Wait();
        }

        public RulesEngineWrapper(Workflow[] workflows, RulesEngineWrapperSettings rewSettings = default!) : this(rewSettings)
        {
            AddOrUpdateWorkflow(workflows).Wait();
        }

        public RulesEngineWrapper(RulesEngineWrapperSettings rewSettings = default!)
        {
            rewSettings ??= new RulesEngineWrapperSettings();

            _rulesEngine = new RulesEngine.RulesEngine(rewSettings.reSettings);
            _mediator = rewSettings.mediator ?? new ServiceCollection().AddRulesEngineWrapper<RulesEngineWrapperContext>(rewSettings).BuildServiceProvider().GetRequiredService<IMediator>();
        }

        public async Task<bool> RemoveWorkflow(params string[] workflowNames) => await _mediator.Send(new RemoveWorkflowCommand(workflowNames));

        public async Task AddOrUpdateWorkflow(params Workflow[] workflows) => await _mediator.Publish(new AddOrUpdateWorkflowNotification(_rulesEngine, workflows));

        public async Task<bool> AddWorkflow(params Workflow[] workflows) => await _mediator.Send(new AddWorkflowCommand(workflows));

        public async Task<IEnumerable<string>> GetAllWorkflowNames() => await _mediator.Send(new GetAllWorkflowNamesQuery());

        public async ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs) => await _mediator.Send(new ExecuteAllRulesCommand(_rulesEngine, workflowName, inputs));

        public async ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams) => await _mediator.Send(new ExecuteAllRulesCommand(_rulesEngine, workflowName, ruleParams));

        public async ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters) => await _mediator.Send(new ExecuteActionWorkflowCommand(workflowName, ruleName, ruleParameters));
    }
}
