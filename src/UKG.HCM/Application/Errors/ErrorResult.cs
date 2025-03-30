using System.Diagnostics.CodeAnalysis;

namespace UKG.HCM.Application.Errors;

public class ErrorResult
{
    public required string Message { get; set; }

    [SetsRequiredMembers]
    public ErrorResult(string message)
    {
        Message = message;
    }
}