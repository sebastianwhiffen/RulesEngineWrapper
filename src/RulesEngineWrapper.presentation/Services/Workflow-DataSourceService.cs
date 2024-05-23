using Microsoft.Extensions.Logging;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrappers.Domain;

public class WorkflowDataSourceService : IWorkflowService
{
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IWorkflowService _baseWorkflowService;
    private readonly ILogger<WorkflowDataSourceService> _logger;
    public WorkflowDataSourceService(ILoggerFactory logger, IRulesEngine rulesEngine, IWorkflowRepository workflowRepository)
    {
        _logger = logger != null ? logger.CreateLogger<WorkflowDataSourceService>() : throw new ArgumentNullException(nameof(logger));
        _baseWorkflowService = new WorkflowService(rulesEngine);
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
    }

    public async void AddOrUpdateWorkflow(params Workflow[] workflows)
    {
        foreach (var workflow in workflows)
        {
            if (await _workflowRepository.FindAsync(workflow.WorkflowName) != null)
            {
                _logger.LogDebug("Updating workflow {workflowname}", workflow.WorkflowName);

                await _workflowRepository.Update(workflow.ToEntity());
            }
            else AddWorkflow(workflows);
        }

        _baseWorkflowService.AddOrUpdateWorkflow(workflows);
    }

    public void AddWorkflow(params Workflow[] workflows)
    {
        foreach (var workflow in workflows)
        {
            _logger.LogDebug("Adding workflow {workflowname}", workflow.WorkflowName);

            _workflowRepository.AddAsync(workflow.ToEntity());
        }

        _baseWorkflowService.AddWorkflow(workflows);
    }

    public void ClearWorkflows()
    {
        _logger.LogDebug("Clearing all workflows");
        _baseWorkflowService.ClearWorkflows();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        return _baseWorkflowService.ContainsWorkflow(workflowName) == (_workflowRepository.FindAsync(workflowName).Result != null);
    }

    public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
    {
        _logger.LogDebug("Executing action {ruleName} for workflow {workflowName}", ruleName, workflowName);
        return _baseWorkflowService.ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
    {
        _logger.LogDebug("Executing all rules for workflow {workflowName}", workflowName);
        return _baseWorkflowService.ExecuteAllRulesAsync(workflowName, inputs);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
    {
        _logger.LogDebug("Executing all rules for workflow {workflowName}", workflowName);
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
            _logger.LogDebug("Removing workflow {workflowName}", workflowName);

            _workflowRepository.Remove(workflowName);
        }

        _baseWorkflowService.RemoveWorkflow(workflowNames);
    }
}