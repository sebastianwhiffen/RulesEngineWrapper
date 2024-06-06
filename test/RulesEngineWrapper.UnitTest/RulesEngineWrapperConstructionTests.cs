using Newtonsoft.Json;
using RulesEngineWrapper.Presentation;

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
        Assert.NotNull(new RulesEngineWrapper(new RulesEngineWrapperSettings()));
    }

    [Fact]
    public void InstantiateRulesEngineWrapperEnableDatabase_ShouldWork()
    {
        var ReSettings = new RulesEngineWrapperSettings { UseDatabase = true };
        Assert.NotNull(new RulesEngineWrapper(ReSettings));
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithJsonConfig_ShouldWork()
    {
        var jsonConfig = new string[] { JsonConvert.SerializeObject(RulesEngineWrapperUtility.NewWorkflow()) };
        var re = new RulesEngineWrapper(jsonConfig);

        Assert.NotNull(re);
        Assert.NotNull(re.GetAllRegisteredWorkflowNames());
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithWorkflows_ShouldWork()
    {
        var re = new RulesEngineWrapper(new[] { RulesEngineWrapperUtility.NewWorkflow() });

        Assert.NotNull(re);
        Assert.NotNull(re.GetAllRegisteredWorkflowNames());
    }
}