using JetBrains.Annotations;

namespace UKG.HCM.WebApi.Endpoints;

/// An endpoint definition
/// Used to register endpoints in the WebApi assembly
[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface IEndpointDefinition
{
    /// Defines an endpoint
    static abstract void Define(IEndpointRouteBuilder builder);
}