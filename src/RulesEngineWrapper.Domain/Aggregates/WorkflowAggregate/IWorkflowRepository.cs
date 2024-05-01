
namespace RulesEngineWrapper.Domain;

public interface IWorkflowRepository : IRepository<WorkflowEntity>
{
    WorkflowEntity Add(WorkflowEntity workflowEntity);

    Task<WorkflowEntity> FindByIdAsync(Guid id);

    Task<WorkflowEntity> FindAsync(string workflowName);

    WorkflowEntity Update(WorkflowEntity workflow);
}