using System.Diagnostics.CodeAnalysis;

namespace UKG.HCM.Application.Errors;

public class ForbidError : ErrorResult
{
    [SetsRequiredMembers]
    public ForbidError(string message) : base(message)
    {
    }
}