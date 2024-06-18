//this class is used to encapsulate the services that are used by the rules engine wrapper
using Microsoft.Extensions.DependencyInjection;

public class RulesEngineWrapperServices
{
    private bool isServiceCollectionModified { get; set; } = false;
    private ServiceCollection Services { get; set; } = new ServiceCollection();
    private IServiceProvider _serviceProvider { get => Services.BuildServiceProvider(); }
    private IWorkflowService _workflowService { get; set; }

    public ServiceCollection ModifyRulesEngineWrapperServiceCollection(Action<ServiceCollection> action)
    {
        action(Services);
        //the reason we set this is to reduce the number of times we call build service provider
        isServiceCollectionModified = true;
        return Services;
    }

    public IWorkflowService GetWorkflowService()
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

    public ServiceCollection GetServices() => Services;

}