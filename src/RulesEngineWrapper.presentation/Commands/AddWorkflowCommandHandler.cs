using MediatR;
using RulesEngine.Models;
using RulesEngineWrappers.Domain;

namespace RulesEngineWrappers.presentation;
public record AddWorkflowCommand(params Workflow[] Workflows) : IRequest<bool>;

public class AddWorkflowCommandHandler
    : IRequestHandler<AddWorkflowCommand, bool>
{
    private readonly Domain.IWorkflowRepository _workflowRepository;

    public AddWorkflowCommandHandler(
        IWorkflowRepository workflowRepository
        )
    {
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
    }

    public async Task<bool> Handle(AddWorkflowCommand message, CancellationToken cancellationToken)
    {
        foreach (Workflow workflow in message.Workflows)
        {
            await _workflowRepository.AddAsync(workflow.ToWorkflowEntity());
        }

        return await _workflowRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
