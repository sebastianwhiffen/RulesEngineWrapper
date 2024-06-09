using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RulesEngine.Models;

namespace RulesEngineWrapper
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper<RulesEngineWrapper>
    {
        private readonly IWorkflowService _workflowService;

        public RulesEngineWrapper(string[] jsonConfig, Action<IConfiguration<IRulesEngineWrapper<RulesEngineWrapper>>> action = null) : this(action)
        {
            var workflows = jsonConfig.Select(JsonConvert.DeserializeObject<Workflow>).ToArray();
            AddOrUpdateWorkflow(workflows!);
        }

        public RulesEngineWrapper(Workflow[] workflows, Action<IConfiguration<IRulesEngineWrapper<RulesEngineWrapper>>> action = null) : this(action)
        {
            AddOrUpdateWorkflow(workflows);
        }

        public RulesEngineWrapper(Action<IConfiguration<IRulesEngineWrapper<RulesEngineWrapper>>> action = null)
        {
            action?.Invoke(new Configuration<IRulesEngineWrapper<RulesEngineWrapper>>(this));

            var serviceProvider = Services.BuildServiceProvider();
            
            _workflowService = serviceProvider.GetRequiredService<IWorkflowService>();
            
        }

        public ServiceCollection Services {get; set;} = new ServiceCollection();

        public RulesEngineWrapper Entity => this;
    }
}
