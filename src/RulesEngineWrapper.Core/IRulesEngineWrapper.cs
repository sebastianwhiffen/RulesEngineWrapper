using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Interfaces;

namespace RulesEngineWrapper
{
    public interface IRulesEngineWrapper<out T> : IRulesEngineWrapper where T : IRulesEngineWrapper
    {
    }

    public interface IRulesEngineWrapper : IRulesEngine
    {
        ServiceCollection ModifyRulesEngineWrapperServiceCollection(Action<ServiceCollection> action);
    }
}
