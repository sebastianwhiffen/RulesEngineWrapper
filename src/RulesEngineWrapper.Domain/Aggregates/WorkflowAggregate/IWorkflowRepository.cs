
namespace RulesEngineWrapper.Domain;

public interface IWorkflowRepository : IRepository<WorkflowEntity>
{
    Task<WorkflowEntity> AddAsync(WorkflowEntity workflowEntity);

    Task<WorkflowEntity> FindByIdAsync(Guid id);

    Task<WorkflowEntity> FindAsync(string workflowName);

    Task<WorkflowEntity> Update(WorkflowEntity workflow);

    Task<WorkflowEntity> Remove(string workflowName);
}