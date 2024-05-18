using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrappers.Domain;

public class WorkflowDataSourceService : WorkflowService, IWorkflowService
{
    private readonly IWorkflowRepository _workflowRepository;
    public WorkflowDataSourceService(IRulesEngine rulesEngine, IWorkflowRepository workflowRepository) : base(rulesEngine)
    {
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
    }
    public override void AddOrUpdateWorkflow(params Workflow[] workflows)
    {
        base.AddOrUpdateWorkflow(workflows);
    }
}