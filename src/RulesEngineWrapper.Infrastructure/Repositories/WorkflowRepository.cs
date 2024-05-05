
using Microsoft.EntityFrameworkCore;

namespace RulesEngineWrapper.Domain
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly IRulesEngineWrapperContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public WorkflowRepository(IRulesEngineWrapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<WorkflowEntity> Add(WorkflowEntity workflowEntity)
        {
            return (await _context.Workflows.AddAsync(workflowEntity)).Entity;
        }

        public Task<WorkflowEntity> Update(WorkflowEntity workflow)
        {
            _context.Workflows.Update(workflow);
            return Task.FromResult(workflow);
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

            return workflow;
            
        }
    }
}