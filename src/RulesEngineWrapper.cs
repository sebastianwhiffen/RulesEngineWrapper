using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.presentation;

namespace RulesEngineWrapper;

public class RulesEngineWrapper : IRulesEngine
{
    #region constructors
    private readonly RulesEngine.RulesEngine _rulesEngine;
    private readonly IDataSourceRepository _dataSourceRepository;

    public RulesEngineWrapper(RulesEngine.RulesEngine rulesEngine, IDataSourceRepository dataSourceRepository)
    {
        _rulesEngine = rulesEngine;
        _dataSourceRepository = dataSourceRepository; 
    }

    public RulesEngineWrapper(string[] jsonConfig, ReSettings reSettings = null) : this(reSettings)
    {
        _rulesEngine = new RulesEngine.RulesEngine(jsonConfig, reSettings);
    }

    public RulesEngineWrapper(Workflow[] Workflows, ReSettings reSettings = null) : this(reSettings)
    {
         _rulesEngine = new RulesEngine.RulesEngine(Workflows, reSettings);
    }

    public RulesEngineWrapper(ReSettings reSettings = null)
    {
        _rulesEngine = new RulesEngine.RulesEngine(reSettings);
    }

    #endregion
   
    #region public methods
    public void AddOrUpdateWorkflow(params Workflow[] Workflows)
    {   
        _dataSourceRepository.AddOrUpdateWorkflow(Workflows);
        _rulesEngine.AddOrUpdateWorkflow(Workflows);
    }

    public void AddWorkflow(params Workflow[] Workflows)
    {
        _dataSourceRepository.AddWorkflow(Workflows);
        _rulesEngine.AddWorkflow(Workflows);
    }

    public void ClearWorkflows()
    {
        _dataSourceRepository.ClearWorkflows();
        _rulesEngine.ClearWorkflows();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        _rulesEngine.ContainsWorkflow(workflowName);
        return _rulesEngine.ContainsWorkflow(workflowName);
    }

    public ValueTask<ActionRuleResult> ExecuteActionWorkflowAsync(string workflowName, string ruleName, RuleParameter[] ruleParameters)
    {
        return _rulesEngine.ExecuteActionWorkflowAsync(workflowName, ruleName, ruleParameters);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params object[] inputs)
    {
        return _rulesEngine.ExecuteAllRulesAsync(workflowName, inputs);
    }

    public ValueTask<List<RuleResultTree>> ExecuteAllRulesAsync(string workflowName, params RuleParameter[] ruleParams)
    {
        return _rulesEngine.ExecuteAllRulesAsync(workflowName, ruleParams);
    }

    public List<string> GetAllRegisteredWorkflowNames()
    {
        _dataSourceRepository.GetAllRegisteredWorkflowNames();
        return _rulesEngine.GetAllRegisteredWorkflowNames();
    }

    public void RemoveWorkflow(params string[] workflowNames)
    {
        _dataSourceRepository.RemoveWorkflow(workflowNames);
        _rulesEngine.RemoveWorkflow(workflowNames);
    }
    #endregion
}
 