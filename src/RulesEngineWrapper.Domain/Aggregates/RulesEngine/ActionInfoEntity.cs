using CodenameGenerator;
using RulesEngineWrapper.Domain;

public class ActionInfoEntity : Entity
{
    public string Name { get; set; } = new Generator().Generate();
    public Dictionary<string, object>? Context { get; set; }

}