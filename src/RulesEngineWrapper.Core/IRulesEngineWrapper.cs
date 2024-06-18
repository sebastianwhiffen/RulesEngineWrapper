using RulesEngine.Interfaces;

namespace RulesEngineWrapper
{
    public interface IRulesEngineWrapper<out T> : IRulesEngineWrapper where T : IRulesEngineWrapper
    {
    }

    public interface IRulesEngineWrapper : IRulesEngine
    {
        RulesEngineWrapperServices RulesEngineWrapperServices { get; set; }
    }
}
