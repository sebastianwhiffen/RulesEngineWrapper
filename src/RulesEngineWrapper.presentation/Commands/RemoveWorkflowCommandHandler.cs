using MediatR;
using RulesEngineWrappers.Domain;

namespace RulesEngineWrappers.presentation;
public record RemoveWorkflowCommand(params string[] WorkflowNames) : IRequest<bool>;

public class RemoveWorkflowCommandHandler : IRequestHandler<RemoveWorkflowCommand, bool>
{
    private readonly IWorkflowRepository _workflowRepository;
    public RemoveWorkflowCommandHandler( IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
    }

    public async Task<bool> Handle(RemoveWorkflowCommand request, CancellationToken cancellationToken)
    {
        foreach (string workflowName in request.WorkflowNames)
        {
            await _workflowRepository.Remove(workflowName);
        }

        return await _workflowRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}