namespace RulesEngineWrapper.Domain;

public class RuleActionEntity : Entity
{
    public ActionInfoEntity? OnSuccess { get; set; }
    public ActionInfoEntity? OnFailure { get; set; }
}

