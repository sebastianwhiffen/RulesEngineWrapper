using RulesEngine.Models;
using RulesEngineWrapper.Domain;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public class DatabaseSourceRepository : IDataSourceRepository
{
    private readonly IRulesEngineWrapperContext _rulesEngineContext;
    public DatabaseSourceRepository(IRulesEngineWrapperContext rulesEngineContext)
    {
        _rulesEngineContext = rulesEngineContext;
    }

    public ValueTask<IEnumerable<WorkflowEntity>> AddOrUpdateWorkflow(params WorkflowEntity[] Workflows)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<WorkflowEntity>> AddWorkflow(params WorkflowEntity[] Workflows)
    {
        throw new NotImplementedException();
    }

    public void ClearWorkflows()
    {
        throw new NotImplementedException();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<string>> GetAllRegisteredWorkflowNames()
    {
        throw new NotImplementedException();
    }

    public ValueTask RemoveWorkflow(params string[] workflowNames)
    {
        throw new NotImplementedException();
    }
}
