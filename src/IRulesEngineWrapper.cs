using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace RulesEngineWrapper;

public interface IRulesEngineWrapper: IRulesEngine 
{
    public Task<IEnumerable<Workflow>> AddWorkflowToDataSource(IEnumerable<Workflow> workflow);
    public Task<IEnumerable<Workflow>> AddWorkflowToCache(IEnumerable<Workflow> workflow);
    public Task<Workflow> RemoveWorkflowFromDataSource(string workflowName);
    public Task<Workflow> RemoveWorkflowFromCache(string workflowName);
    public Task<Workflow> GetWorkflowFromDataSource(string workflowName);
    public Task<Workflow> GetWorkflowFromCache(string workflowName);
    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromDataSource();
    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromCache();
    public Task<IEnumerable<Workflow>> ClearWorkflowsFromDataSource();
    public Task<IEnumerable<Workflow>> ClearWorkflowsFromCache();
    public Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInDataSource(IEnumerable<Workflow> workflows);
    public Task<IEnumerable<Workflow>> AddOrUpdateWorkflowInCache(IEnumerable<Workflow> workflows);

}
