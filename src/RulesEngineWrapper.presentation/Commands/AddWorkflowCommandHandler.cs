namespace RulesEngineWrapper.presentation;

using global::RulesEngineWrapper.Domain;
using MediatR;
using RulesEngine.Models;

public record AddWorkflowCommand(params Workflow[] Workflows) : IRequest<bool>;

public class AddWorkflowCommandHandler
    : IRequestHandler<AddWorkflowCommand, bool>
{
    // private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
    // private readonly ILogger<AddWorkflowCommandHandler> _logger;
    private readonly IWorkflowRepository _workflowRepository;

    public AddWorkflowCommandHandler(
        // IOrderingIntegrationEventService orderingIntegrationEventService,
        // ILogger<AddWorkflowCommandHandler> logger,
        IWorkflowRepository workflowRepository
        )
    {
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
        // _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        // _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(AddWorkflowCommand message, CancellationToken cancellationToken)
    {
        // Add/Update the Buyer AggregateRoot
        // DDD patterns comment: Add child entities and value-objects through the Aggregate-Root
        // methods and constructor so validations, invariants and business logic 
        // make sure that consistency is preserved across the whole aggregate

        // _logger.LogInformation("Creating Order - Order: {@Order}", order);

        foreach(Workflow workflow in message.Workflows)
        {
           _ = await _workflowRepository.AddAsync(workflow.ToWorkflowEntity());
        }

        return await _workflowRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}


// // Use for Idempotency in Command process
// public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CreateOrderCommand, bool>
// {
//     public CreateOrderIdentifiedCommandHandler(
//         IMediator mediator,
//         IRequestManager requestManager,
//         ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>> logger)
//         : base(mediator, requestManager, logger)
//     {
//     }

//     protected override bool CreateResultForDuplicateRequest()
//     {
//         return true; // Ignore duplicate requests for creating order.
//     }
// }
