using RulesEngineWrapper.Domain;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public interface IDataSourceRepository
{
    ValueTask<IEnumerable<WorkflowEntity>> AddOrUpdateWorkflow(IEnumerable<WorkflowEntity> workflowEntities);
    ValueTask<IEnumerable<WorkflowEntity>> AddWorkflow(IEnumerable<WorkflowEntity> workflowEntities);
    public void ClearWorkflows();
    public bool ContainsWorkflow(string workflowName);
    ValueTask<List<string>> GetAllRegisteredWorkflowNames();
    ValueTask RemoveWorkflow(params string[] workflowNames);
}