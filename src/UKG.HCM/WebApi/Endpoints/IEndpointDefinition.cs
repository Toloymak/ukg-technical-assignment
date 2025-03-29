using JetBrains.Annotations;

namespace UKG.HCM.WebApi.Endpoints;

/// An endpoint definition
[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface IEndpointDefinition
{
    /// Defines an endpoint
    static abstract void Define(IEndpointRouteBuilder builder);
}