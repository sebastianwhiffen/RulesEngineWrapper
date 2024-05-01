
namespace RulesEngineWrapper.Domain;

public interface IWorkflowRepository : IRepository<WorkflowEntity>
{
    WorkflowEntity Add(WorkflowEntity workflowEntity);
}