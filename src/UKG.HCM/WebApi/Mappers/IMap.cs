using JetBrains.Annotations;

namespace UKG.HCM.WebApi.Mappers;

/// Map one type to another
public interface IMap<in TSource, out TDestination>
{
    [Pure]
    TDestination Map(TSource source);
}