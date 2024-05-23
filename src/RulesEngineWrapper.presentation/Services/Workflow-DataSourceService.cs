using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrappers.Domain;

public class WorkflowDataSourceService : IWorkflowService
{
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IWorkflowService _baseWorkflowService;
    public WorkflowDataSourceService(IRulesEngine rulesEngine,IWorkflowRepository workflowRepository)
    {
        _baseWorkflowService = new WorkflowService(rulesEngine);
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
    }

    public async void AddOrUpdateWorkflow(params Workflow[] workflows)
    {
        foreach (var workflow in workflows)
        {
            if (await _workflowRepository.FindAsync(workflow.WorkflowName) != null)
            {
                await _workflowRepository.Update(workflow.ToEntity());
            }
            else
            {
                AddWorkflow(workflows);
            }
        }
        
        _baseWorkflowService.AddOrUpdateWorkflow(workflows);
    }

    public void AddWorkflow(params Workflow[] workflows)
    {
        foreach (var workflow in workflows)_workflowRepository.AddAsync(workflow.ToEntity());

        _baseWorkflowService.AddWorkflow(workflows);
    }

    public void ClearWorkflows()
    {   
        _baseWorkflowService.ClearWorkflows();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        return _baseWorkflowService.ContainsWorkflow(workflowName) == (_workflowRepository.FindAsync(workflowName).Result != null);
    }

    public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
    {
        return _baseWorkflowService.ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
    {
        return _baseWorkflowService.ExecuteAllRulesAsync(workflowName, inputs);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
    {
        return _baseWorkflowService.ExecuteAllRulesAsync(workflowName, ruleParams);
    }

    public List<string> GetAllRegisteredWorkflowNames()
    {
        return _baseWorkflowService.GetAllRegisteredWorkflowNames();
    }

    public void RemoveWorkflow(params string[] workflowNames)
    {
        foreach (var workflowName in workflowNames)
        {
            _workflowRepository.Remove(workflowName);
        }
        
        _baseWorkflowService.RemoveWorkflow(workflowNames);
    }
}