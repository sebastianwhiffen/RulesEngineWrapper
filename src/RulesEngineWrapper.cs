using System.Runtime.InteropServices;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.presentation;
using RulesEngineWrapper.presentation.Options;

namespace RulesEngineWrapper;

public interface IRulesEngineWrapper: IRulesEngine {}

public class RulesEngineWrapper : IRulesEngineWrapper
{
    #region constructors
    private readonly IRulesEngine _rulesEngine;
    private readonly IDataSourceRepository _dataSourceRepository;

    public RulesEngineWrapper(ReSettings options, IDataSourceRepository dataSourceRepository)
    {
        _rulesEngine = new RulesEngine.RulesEngine(options);
        _dataSourceRepository = dataSourceRepository;
    }

    public RulesEngineWrapper(Workflow[] workflows, RulesEngineWrapperOptions options, IDataSourceRepository dataSourceRepository)
        : this(options.reSettings, dataSourceRepository)
    {
        AddWorkflow(workflows);
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
