using RulesEngineWrapper.Domain;

public class Rule_Rules : Entity
{
    public Guid RuleId { get; set; }
    public Guid RelatedRuleId { get; set; }

}