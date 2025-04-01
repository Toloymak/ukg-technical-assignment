using System.Diagnostics.CodeAnalysis;

namespace UKG.HCM.Application.Errors;

public class ErrorResult
{
    public required string Message { get; init; }

    [SetsRequiredMembers]
    public ErrorResult(string message)
    {
        Message = message;
    }
}