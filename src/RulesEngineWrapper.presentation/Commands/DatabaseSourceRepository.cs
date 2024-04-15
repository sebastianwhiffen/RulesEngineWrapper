using System.Dynamic;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Interfaces;
using RulesEngine.Models;

//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public class DatabaseRulesEngineRepository : IDataSourceRepository
{
    private readonly IRulesEngineContext _rulesEngineContext;
    private readonly IRulesEngine _rulesEngine;
    public DatabaseRulesEngineRepository(IRulesEngineContext rulesEngineContext, IRulesEngine rulesEngine)
    {
        _rulesEngineContext = rulesEngineContext;
        _rulesEngine = rulesEngine;
    }

    public Task<Workflow> AddOrUpdateWorkflows(Workflow workflow)
    {
        throw new NotImplementedException();
    }
    #region queries
    public async Task<IEnumerable<Rule>> GetAllRulesAsync()
    {
        return await _rulesEngineContext.Rules.ToListAsync();
    }

    public async Task<IEnumerable<Workflow>> GetAllWorkflowsAsync()
    {
        return await _rulesEngineContext.Workflows.ToListAsync();
    }

    public async Task<Rule> GetRuleAsync(string ruleName)
    {
        return await _rulesEngineContext.Rules.FindAsync(ruleName) ?? throw new KeyNotFoundException(ruleName);
    }

    public async Task<Workflow> GetWorkflowAsync(string workflowName)
    {
        return await _rulesEngineContext.Workflows.FindAsync(workflowName) ?? throw new KeyNotFoundException(workflowName);
    }

    public Task<bool> RemoveWorkflowByNameAsync(string workflowName)
    {
        throw new NotImplementedException();
    }

    public Task<object> RunActionWorkflow(ExecuteActionWorkflowCommand executeAllRulesCommand)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RuleResultTree>> RunAllRulesAsync(ExecuteAllRulesCommand executeAllRulesCommand)
    {
        throw new NotImplementedException();
    }
    #endregion

    //re-do anything below this
    #region commands


    #endregion

}
