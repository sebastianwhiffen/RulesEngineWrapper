
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

        public WorkflowEntity Add(WorkflowEntity workflowEntity)
        {
            return  _context.Workflows.Add(workflowEntity).Entity;
        }
    }
}