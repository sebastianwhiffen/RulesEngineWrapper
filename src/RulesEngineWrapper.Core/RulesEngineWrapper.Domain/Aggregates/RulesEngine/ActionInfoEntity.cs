namespace RulesEngineWrappers.Domain;

using CodenameGenerator;
public class ActionInfoEntity : Entity
{
    public string Name { get; set; } = new Generator().Generate();
    public ICollection<ActionInfoContext> Contexts { get; set; } = new List<ActionInfoContext>();
}

public class ActionInfoContext : Entity
{
    public ActionInfoEntity ActionInfoEntity { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}