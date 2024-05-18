using RulesEngine.Models;

namespace RulesEngineWrappers;

public partial class RulesEngineWrapper : IRulesEngineWrapper
{
    public event EventHandler<Workflow[]> OnExecuteAllRulesAsync = delegate { };
    public event EventHandler<Workflow[]> OnExecuteActionWorkflowAsync = delegate { };
    public event EventHandler<string[]> OnRemoveWorkflow = delegate { };
    public event EventHandler<Workflow[]> OnAddOrUpdateWorkflow = delegate { };
    public event EventHandler<Workflow[]> OnAddWorkflow = delegate { };
}