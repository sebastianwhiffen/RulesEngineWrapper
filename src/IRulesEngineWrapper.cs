using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace RulesEngineWrapper;

public interface IRulesEngineWrapper: IRulesEngine 
{
    public Task<bool> AddWorkflowToDataSource(IEnumerable<Workflow> workflow);
    public Task<bool> AddWorkflowToCache(IEnumerable<Workflow> workflow);
    public Task<bool> RemoveWorkflowFromDataSource(string workflowName);
    public Task<bool> RemoveWorkflowFromCache(string workflowName);
    public Task<Workflow> GetWorkflowFromDataSource(string workflowName);
    public Task<Workflow> GetWorkflowFromCache(string workflowName);
    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromDataSource();
    public Task<IEnumerable<Workflow>> GetAllWorkflowsFromCache();
    public Task<bool> ClearWorkflowsFromDataSource();
    public Task<bool> ClearWorkflowsFromCache();
    public Task<bool> AddOrUpdateWorkflowInDataSource(IEnumerable<Workflow> workflows);
    public Task<bool> AddOrUpdateWorkflowInCache(IEnumerable<Workflow> workflows);

}
