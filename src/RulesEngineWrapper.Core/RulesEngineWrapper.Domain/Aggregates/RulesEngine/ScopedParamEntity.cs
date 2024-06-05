using CodenameGenerator;

namespace RulesEngineWrappers.Domain;
public class ScopedParamEntity : Entity
    {
        public string Name { get; set; } = new Generator().Generate();
        public string? Expression { get; set; }
    }