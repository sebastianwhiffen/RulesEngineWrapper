using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Interfaces;

namespace RulesEngineWrapper
{
    public interface IRulesEngineWrapper<out T> : IRulesEngineWrapper where T : IRulesEngineWrapper
    {
        T Entity { get; }
    }

    public interface IRulesEngineWrapper : IRulesEngine
    {
        internal ServiceCollection Services { get; set; }
    }
}
