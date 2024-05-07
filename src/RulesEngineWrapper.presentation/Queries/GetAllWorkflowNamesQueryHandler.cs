using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetAllWorkflowNamesQuery() : IRequest<IEnumerable<string>>;

public class GetAllWorkflowNamesQueryHandler : IRequestHandler<GetAllWorkflowNamesQuery, IEnumerable<string>>
{
    private readonly IRulesEngineWrapperContext _context;
    public GetAllWorkflowNamesQueryHandler(IRulesEngineWrapperContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<string>> Handle(GetAllWorkflowNamesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workflows.Select(w => w.WorkflowName).ToListAsync();        
    }
}