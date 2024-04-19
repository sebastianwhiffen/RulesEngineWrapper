using RulesEngine.Interfaces;
using RulesEngine.Models;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public interface IDataSourceRepository
{
    ValueTask<IEnumerable<Workflow>> AddOrUpdateWorkflow(params Workflow[] Workflows);
    ValueTask<IEnumerable<Workflow>> AddWorkflow(params Workflow[] Workflows);
    public void ClearWorkflows();
    public bool ContainsWorkflow(string workflowName);
    ValueTask<List<string>> GetAllRegisteredWorkflowNames();
    ValueTask RemoveWorkflow(params string[] workflowNames);
}