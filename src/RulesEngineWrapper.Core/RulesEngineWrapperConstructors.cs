using RulesEngineWrapper.Presentation;

namespace RulesEngineWrapper
{
    public partial class RulesEngineWrapper : IRulesEngineWrapper<RulesEngineWrapper>
    {
        public RulesEngineWrapper(Action<IRulesEngineWrapper<RulesEngineWrapper>> action = null)
        {
            this.AddServiceDefaults();

            action?.Invoke(this);
        }
    }
}
