using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RulesEngine.Models;
using RulesEngineWrapper.Presentation;

namespace RulesEngineWrapper
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper<RulesEngineWrapper>
    {
        private readonly IWorkflowService _workflowService;

        public RulesEngineWrapper(string[] jsonConfig, Action<IRulesEngineWrapper<RulesEngineWrapper>> action = null) : this(action)
        {
            var workflows = jsonConfig.Select(JsonConvert.DeserializeObject<Workflow>).ToArray();
            AddOrUpdateWorkflow(workflows!);
        }

        public RulesEngineWrapper(Workflow[] workflows, Action<IRulesEngineWrapper<RulesEngineWrapper>> action = null) : this(action)
        {
            AddOrUpdateWorkflow(workflows);
        }

        public RulesEngineWrapper(Action<IRulesEngineWrapper<RulesEngineWrapper>> action = null)
        {
            this.AddServiceDefaults();

            action?.Invoke(this);

            var serviceProvider = Services.BuildServiceProvider();
            
            _workflowService = serviceProvider.GetRequiredService<IWorkflowService>();
        }
        
        public ServiceCollection Services {get; set;} = new ServiceCollection();

    }
}
