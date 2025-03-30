using System.Diagnostics.CodeAnalysis;

namespace UKG.HCM.Application.Errors;

public class NotFoundError : ErrorResult
{
    [SetsRequiredMembers]
    public NotFoundError(string message) : base(message)
    {
    }
}