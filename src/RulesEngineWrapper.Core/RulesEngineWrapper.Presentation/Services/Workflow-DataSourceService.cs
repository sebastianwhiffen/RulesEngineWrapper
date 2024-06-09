using Microsoft.Extensions.Logging;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;

public class WorkflowDataSourceService : IWorkflowService
{
    private readonly IWorkflowRepository _workflowRepository;
    private readonly ILogger<WorkflowDataSourceService> _logger;
    private readonly IRulesEngine _rulesEngine;
    public WorkflowDataSourceService(ILogger<WorkflowDataSourceService> logger, IRulesEngine rulesEngine, IWorkflowRepository workflowRepository)
    {
        _logger = logger;
        _rulesEngine = rulesEngine;
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
    }

    public async void AddOrUpdateWorkflow(params Workflow[] workflows)
    {
        foreach (var workflow in workflows)
        {
            if (await _workflowRepository.FindAsync(workflow.WorkflowName) != null)
            {
                _logger.LogInformation("Updating workflow {workflowname}", workflow.WorkflowName);

                await _workflowRepository.Update(workflow.ToEntity());
            }
            else AddWorkflow(workflows);
        }

        _rulesEngine.AddOrUpdateWorkflow(workflows);
    }

    public void AddWorkflow(params Workflow[] workflows)
    {
        foreach (var workflow in workflows)
        {
            _logger.LogInformation("Adding workflow {workflowname}", workflow.WorkflowName);

            _workflowRepository.AddAsync(workflow.ToEntity());
        }

        _rulesEngine.AddWorkflow(workflows);
    }

    public void ClearWorkflows()
    {
        _logger.LogInformation("Clearing all workflows");
        _rulesEngine.ClearWorkflows();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        return _rulesEngine.ContainsWorkflow(workflowName) == (_workflowRepository.FindAsync(workflowName).Result != null);
    }

    public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
    {
        _logger.LogInformation("Executing action {ruleName} for workflow {workflowName}", ruleName, workflowName);
        return _rulesEngine.ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
    {
        _logger.LogInformation("Executing all rules for workflow {workflowName}", workflowName);
        return _rulesEngine.ExecuteAllRulesAsync(workflowName, inputs);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
    {
        _logger.LogInformation("Executing all rules for workflow {workflowName}", workflowName);
        return _rulesEngine.ExecuteAllRulesAsync(workflowName, ruleParams);
    }

    public List<string> GetAllRegisteredWorkflowNames()
    {
        return _rulesEngine.GetAllRegisteredWorkflowNames();
    }

    public void RemoveWorkflow(params string[] workflowNames)
    {
        foreach (var workflowName in workflowNames)
        {
            _logger.LogInformation("Removing workflow {workflowName}", workflowName);

            _workflowRepository.Remove(workflowName);
        }

        _rulesEngine.RemoveWorkflow(workflowNames);
    }
}