using MediatR;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrappers.Domain;

namespace RulesEngineWrappers.presentation;
public record AddOrUpdateWorkflowNotification(IRulesEngine RulesEngine, IEnumerable<Workflow> Workflows) : INotification;

public class AddOrUpdateWorkflowToRulesEngine
    : INotificationHandler<AddOrUpdateWorkflowNotification>
{
    public async Task Handle(AddOrUpdateWorkflowNotification notification, CancellationToken cancellationToken)
    {
        notification.RulesEngine.AddOrUpdateWorkflow(notification.Workflows.ToArray());
    }
}
public class AddOrUpdateWorkflowToDataSource
    : INotificationHandler<AddOrUpdateWorkflowNotification>
{
    private readonly IWorkflowRepository _workflowRepository;
    public AddOrUpdateWorkflowToDataSource(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }
    
    public async Task Handle(AddOrUpdateWorkflowNotification notification, CancellationToken cancellationToken)
    {
        foreach (Workflow workflow in notification.Workflows)
        {
            var workflowEntity = await _workflowRepository.FindAsync(workflow.WorkflowName);

            if (workflowEntity != null) await _workflowRepository.Update(workflowEntity);

            else await _workflowRepository.AddAsync(workflow.ToWorkflowEntity());
        }

        await _workflowRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
