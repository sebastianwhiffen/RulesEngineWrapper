namespace RulesEngineWrapper.presentation;

using global::RulesEngineWrapper.Domain;
using MediatR;
using RulesEngine.Interfaces;
using RulesEngine.Models;

public record AddOrUpdateWorkflowCommand(IRulesEngine RulesEngine, IEnumerable<Workflow> Workflows) : IRequest<bool>;

public class AddOrUpdateWorkflowCommandHandler
    : IRequestHandler<AddOrUpdateWorkflowCommand, bool>
{
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IMediator _mediator;

    public AddOrUpdateWorkflowCommandHandler(
        IWorkflowRepository workflowRepository,
        IMediator mediator
        )
    {
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<bool> Handle(AddOrUpdateWorkflowCommand message, CancellationToken cancellationToken)
    {
        message.RulesEngine.AddOrUpdateWorkflow(message.Workflows.ToArray());

        foreach (Workflow workflow in message.Workflows)
        {
            var workflowEntity = await _workflowRepository.FindAsync(workflow.WorkflowName);

            if (workflowEntity != null) await _workflowRepository.Update(workflowEntity);

            else await _workflowRepository.AddAsync(workflow.ToWorkflowEntity());
        }

        return await _workflowRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
