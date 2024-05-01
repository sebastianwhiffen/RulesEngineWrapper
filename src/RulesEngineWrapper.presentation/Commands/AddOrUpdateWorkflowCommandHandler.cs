namespace RulesEngineWrapper.presentation;

using global::RulesEngineWrapper.Domain;
using MediatR;
using RulesEngine.Models;

public record AddOrUpdateWorkflowCommand(IEnumerable<Workflow> Workflows) : IRequest<bool>;

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
        message.Workflows.ToList().ForEach(w => _workflowRepository.FindAsync(w.WorkflowName).ContinueWith(async t =>
        {
            if (t.Result == null)
            {
                await _mediator.Send(new AddWorkflowCommand(w));
            }
            else
            {
                _workflowRepository.Update(w.ToWorkflowEntity());
            }
        }));

        return await _workflowRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
