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
    private readonly IRulesEngineWrapperContext _rulesEngineContext;
    private readonly IRulesEngine _rulesEngine;
    public DatabaseRulesEngineRepository(IRulesEngineWrapperContext rulesEngineContext, IRulesEngine rulesEngine)
    {
        _rulesEngineContext = rulesEngineContext;
        _rulesEngine = rulesEngine;
    }

    public ValueTask<IEnumerable<Workflow>> AddOrUpdateWorkflow(params Workflow[] Workflows)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<Workflow>> AddWorkflow(params Workflow[] Workflows)
    {
        throw new NotImplementedException();
    }

    public void ClearWorkflows()
    {
        throw new NotImplementedException();
    }

    public bool ContainsWorkflow(string workflowName)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<string>> GetAllRegisteredWorkflowNames()
    {
        throw new NotImplementedException();
    }

    public ValueTask RemoveWorkflow(params string[] workflowNames)
    {
        throw new NotImplementedException();
    }
}
