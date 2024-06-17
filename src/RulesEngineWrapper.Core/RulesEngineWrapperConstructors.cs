using Microsoft.Extensions.DependencyInjection;
using RulesEngineWrapper.Presentation;

namespace RulesEngineWrapper
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper<RulesEngineWrapper>
    {
        private bool isServiceCollectionModified { get; set; } = false;
        private ServiceCollection Services { get; set; } = new ServiceCollection();
        private IServiceProvider _serviceProvider { get => Services.BuildServiceProvider(); }
        private IWorkflowService _workflowService {get; set;}
        
        public RulesEngineWrapper(Action<IRulesEngineWrapper<RulesEngineWrapper>> action = null)
        {
            this.AddServiceDefaults();

            action?.Invoke(this);
        }

        public ServiceCollection ModifyRulesEngineWrapperServiceCollection(Action<ServiceCollection> action)
        {
            action(Services);
            //the reason we set this is to reduce the number of times we call build service provider
            isServiceCollectionModified = true;
            return Services;
        }

        private IWorkflowService GetWorkflowService()
        {
            if (isServiceCollectionModified)
            {
                _workflowService = _serviceProvider.GetService<IWorkflowService>();
                //set modification state back to false
                isServiceCollectionModified = false;
                return _workflowService;
            }
            
            return _workflowService;
        }
    }
}
