namespace RulesEngineWrapper;


public interface IConfiguration<T> : IConfiguration
{
    T Entry { get; }
}

public interface IConfiguration {}