
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RulesEngineWrappers.Domain
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly IRulesEngineWrapperContext _context;
        private readonly ILogger<WorkflowRepository> _logger;
        public IUnitOfWork UnitOfWork => _context;

        public WorkflowRepository(IRulesEngineWrapperContext context, ILogger<WorkflowRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<WorkflowEntity> AddAsync(WorkflowEntity workflowEntity)
        {
            var workflow = await _context.Workflows.AddAsync(workflowEntity);
            _logger.LogInformation("Added workflow {workflowName}", workflowEntity.WorkflowName);
            
            return workflow.Entity;
        }

        public Task<WorkflowEntity> Update(WorkflowEntity workflow)
        {
            var result = _context.Workflows.Update(workflow).Entity;
            _logger.LogInformation("Updated workflow {workflowName}", workflow.WorkflowName);

            return Task.FromResult(result);
        }

        public async Task<WorkflowEntity> FindAsync(string workflowName)
        {
            var workflow = await _context.Workflows
                .Where(b => b.WorkflowName == workflowName)
                .SingleOrDefaultAsync();

            return workflow;
        }

        public async Task<WorkflowEntity> FindByIdAsync(Guid id)
        {
            var workflow = await _context.Workflows
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            return workflow;
        }

        public async Task<WorkflowEntity> Remove(string workflowName)
        {
            var workflow = await FindAsync(workflowName);

            if (workflow != null)
            {
                _context.Workflows.Remove(workflow);
            }

            _logger.LogInformation("Removed workflow {workflowName}", workflowName);

            return workflow;
        }
    }
}