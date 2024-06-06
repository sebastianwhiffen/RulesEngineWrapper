using RulesEngine.Models;
namespace RulesEngineWrapper.Domain;

public static class ScopedParamEntityExtentions
{

    public static IEnumerable<ScopedParam> ToDTOs(this IEnumerable<ScopedParamEntity> scopedParamEntities)
    {
        foreach(ScopedParamEntity scopedParamEntity in scopedParamEntities)
        {
            yield return scopedParamEntity.ToDTO();
        }

    }

    public static ScopedParam ToDTO(this ScopedParamEntity scopedParamEntity)
    {
        return new ScopedParam 
        {
            Name = scopedParamEntity.Name,
            Expression = scopedParamEntity.Expression
        };
    }

    public static IEnumerable<ScopedParamEntity> ToEntities(this IEnumerable<ScopedParam> scopedParams)
    {
        foreach(ScopedParam scopedParam in scopedParams)
        {
            yield return scopedParam.ToEntity();
        }
    }

    public static ScopedParamEntity ToEntity(this ScopedParam scopedParam)
    {
        return new ScopedParamEntity 
        {
            Name = scopedParam.Name,
            Expression = scopedParam.Expression
        };
    }
} 