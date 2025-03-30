using JetBrains.Annotations;

namespace UKG.HCM.WebApi.Mappers;

/// Map one type to another
public interface IMapWithKey<in TKey, in TSource, out TDestination>
{
    [Pure]
    TDestination Map(TKey key, TSource source);
}