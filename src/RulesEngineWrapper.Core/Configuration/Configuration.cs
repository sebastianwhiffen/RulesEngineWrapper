namespace RulesEngineWrapper;

public class Configuration<T> : IConfiguration<T>
{
    public T Entry { get; }

    public Configuration(T entry) => Entry = entry;

}