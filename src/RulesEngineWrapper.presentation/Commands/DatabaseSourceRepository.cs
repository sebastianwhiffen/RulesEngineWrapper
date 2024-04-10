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
    #endregion

    //re-do anything below this
    #region commands
    public async Task<IEnumerable<RuleResultTree>> RunAllRulesAsync(ExecuteAllRulesCommand executeAllRulesCommand)
    {
        dynamic input1 = JsonConvert.DeserializeObject<ExpandoObject>(executeAllRulesCommand.data.ToString(), new ExpandoObjectConverter());

        var rp1 = new RuleParameter(executeAllRulesCommand.data.GetType().Name, input1);

        var result = await _rulesEngine.ExecuteAllRulesAsync(executeAllRulesCommand.workflowName, rp1);
        return result;
    }

    public async Task<Workflow> AddOrUpdateWorkflows(Workflow workflow)
    {
        await RemoveWorkflowByNameAsync(workflow.WorkflowName);

        var result = await _rulesEngineContext.Workflows.AddAsync(workflow);

        _rulesEngine.AddOrUpdateWorkflow(result.Entity);
    
        await _rulesEngineContext.SaveChangesAsync();

        return result.Entity;
    }
    
    public async Task<bool> RemoveWorkflowByNameAsync(string workflowName)
    {
           var workflow = await _rulesEngineContext.Workflows
            .Include(w => w.Rules)
            .FirstOrDefaultAsync(w => w.WorkflowName == workflowName);

        if (workflow.Rules.Any())
        {
            _rulesEngineContext.Rules.RemoveRange(workflow.Rules);
        }

        _rulesEngineContext.Workflows.Remove(workflow);
        await _rulesEngineContext.SaveChangesAsync();

        _rulesEngine.RemoveWorkflow(workflow.WorkflowName);

        return true;
    
    }

    #endregion

}
