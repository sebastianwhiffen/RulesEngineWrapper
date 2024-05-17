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
        public event EventHandler OnAddWorkflow;
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

        public Task<bool> RemoveWorkflow(params string[] workflowNames)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateWorkflow(params Workflow[] Workflows)
        {
            throw new NotImplementedException();
        }

        public Task AddWorkflow(params Workflow[] Workflows)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllWorkflowNames()
        {
            throw new NotImplementedException();
        }
    }
}
