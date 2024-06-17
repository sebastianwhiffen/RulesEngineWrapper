using Newtonsoft.Json;
using RulesEngine.Data;

namespace RulesEngineWrapper.UnitTest;
public class ConstructorTests
{
    [Fact]
    public void InstantiateRulesEngineWrapperNoParams_ShouldWork()
    {
        Assert.NotNull(new RulesEngineWrapper());
    }

    [Fact]
    public void InstantiateRulesEngineWrapperDefaultSettings_ShouldWork()
    {
        Assert.NotNull(new RulesEngineWrapper(x => x.UseRulesEngine()));
    }

    [Fact]
    public void InstantiateRulesEngineWrapperEnableDatabase_ShouldWork()
    {
        Assert.NotNull(new RulesEngineWrapper(x =>
        x.UseDatabase<RulesEngineWrapperContext>()
        ));
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithJsonConfig_ShouldWork()
    {
        var jsonConfig = new string[] { JsonConvert.SerializeObject(RulesEngineWrapperUtility.NewWorkflow()) };

        Assert.NotNull(new RulesEngineWrapper(jsonConfig));
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithWorkflows_ShouldWork()
    {
        Assert.NotNull(new RulesEngineWrapper(new[] { RulesEngineWrapperUtility.NewWorkflow() }));
    }
}