using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace RulesEngineWrappers
{
    public interface IRulesEngineWrapper : IRulesEngine
    {
        public event EventHandler<Workflow[]> OnAddWorkflow;
        public event EventHandler<Workflow[]> OnExecuteAllRulesAsync;
        public event EventHandler<Workflow[]> OnExecuteActionWorkflowAsync;
        public event EventHandler<string[]> OnRemoveWorkflow;
        public event EventHandler<Workflow[]> OnAddOrUpdateWorkflow;
    }
}
