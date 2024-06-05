using CodenameGenerator;

namespace RulesEngineWrappers.Domain;

public class RuleEntity : Entity
{
    public string RuleName { get; set; } = new Generator().Generate();
    public string? Operator { get; set; }
    public string? ErrorMessage { get; set; }
    public bool Enabled { get; set; } = true;
    public IEnumerable<RuleEntity>? Rules { get; set; }
    public IEnumerable<ScopedParamEntity>? LocalParams { get; set; }
    public string? Expression { get; set; }
    public IEnumerable<RuleActionEntity>? Actions { get; set; }
    public string? SuccessEvent { get; set; }

}