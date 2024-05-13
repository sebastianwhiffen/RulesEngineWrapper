using Newtonsoft.Json;
using RulesEngine.Models;

namespace RulesEngineWrappers.UnitTest;
public class RulesEngineWrapperConstructionTests
{
    static RulesEngineWrapperConstructionTests()
    {
        // TestContainersFixture.EnsureInitializedAsync().GetAwaiter().GetResult();
    }
    // public static IEnumerable<object[]> containers => TestContainersFixture._containers.Select(c => new object[] { c });

    [Fact]
    public void InstantiateRulesEngineWrapperNoParams_ShouldWork()
    {
        var re = new RulesEngineWrapper();

        Assert.NotNull(re);
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithJsonConfig_ShouldWork()
    {
        var jsonConfig = new string[] { JsonConvert.SerializeObject(RulesEngineWrapperUtility.NewWorkflow()) };
        var re = new RulesEngineWrapper(jsonConfig);

        Assert.NotNull(re);
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithWorkflows_ShouldWork()
    {
        var re = new RulesEngineWrapper(new[] { RulesEngineWrapperUtility.NewWorkflow() });

        Assert.NotNull(re);
    }
}