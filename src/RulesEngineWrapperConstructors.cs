using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RulesEngine.Models;
using RulesEngineWrappers.Presentation;

namespace RulesEngineWrappers;

public partial class RulesEngineWrapper : IRulesEngineWrapper
{
    private readonly IWorkflowService _workflowService;      

    public RulesEngineWrapper(string[] jsonConfig, RulesEngineWrapperSettings rewSettings = default!) : this(rewSettings)
    {
        var workflows = jsonConfig.Select(JsonConvert.DeserializeObject<Workflow>).ToArray();
        AddOrUpdateWorkflow(workflows!);
    }

    public RulesEngineWrapper(Workflow[] workflows, RulesEngineWrapperSettings rewSettings = default!) : this(rewSettings)
    {
        AddOrUpdateWorkflow(workflows);
    }

    public RulesEngineWrapper(RulesEngineWrapperSettings rewSettings = default!)
    {
        _workflowService  = new ServiceCollection()
        .AddRulesEngineWrapper(rewSettings)
        .BuildServiceProvider()
        .GetRequiredService<IWorkflowService>();

    }
}