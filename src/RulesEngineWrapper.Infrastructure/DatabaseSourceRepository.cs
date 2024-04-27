using RulesEngine.Models;
using RulesEngineWrapper.Domain;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.Infrastructure;

public class DatabaseSourceRepository : IDataSourceRepository
{
    private readonly IRulesEngineWrapperContext _rulesEngineContext;
    public DatabaseSourceRepository(IRulesEngineWrapperContext rulesEngineContext)
    {
        _rulesEngineContext = rulesEngineContext;
    }

    public async ValueTask<IEnumerable<WorkflowEntity>> AddOrUpdateWorkflow(IEnumerable<WorkflowEntity> workflows)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IEnumerable<WorkflowEntity>> AddWorkflow(IEnumerable<WorkflowEntity> workflows)
    {
        await _rulesEngineContext.Workflows.AddRangeAsync(workflows);
        await _rulesEngineContext.SaveChangesAsync();
        return workflows;
    }

    public async void ClearWorkflows()
    {
        throw new NotImplementedException();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<List<string>> GetAllRegisteredWorkflowNames()
    {
        throw new NotImplementedException();
    }

    public async ValueTask RemoveWorkflow(params string[] workflowNames)
    {
        throw new NotImplementedException();
    }
}
