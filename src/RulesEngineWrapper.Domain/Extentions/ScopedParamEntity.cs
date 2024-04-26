using RulesEngine.Models;
using RulesEngineWrapper.Domain;

public static class ScopedParamEntityExtentions
{

    public static IEnumerable<ScopedParam> ToScopedParam(this IEnumerable<ScopedParamEntity> scopedParamEntities)
    {
        foreach(ScopedParamEntity scopedParamEntity in scopedParamEntities)
        {
            yield return scopedParamEntity.ToScopedParam();
        }

    }

    public static ScopedParam ToScopedParam(this ScopedParamEntity scopedParamEntity)
    {
        return new ScopedParam 
        {
            Name = scopedParamEntity.Name,
            Expression = scopedParamEntity.Expression
        };
    }

    public static IEnumerable<ScopedParamEntity> ToScopedParamEntity(this IEnumerable<ScopedParam> scopedParams)
    {
        foreach(ScopedParam scopedParam in scopedParams)
        {
            yield return scopedParam.ToScopedParamEntity();
        }
    }

    public static ScopedParamEntity ToScopedParamEntity(this ScopedParam scopedParam)
    {
        return new ScopedParamEntity 
        {
            Name = scopedParam.Name,
            Expression = scopedParam.Expression
        };
    }
} 