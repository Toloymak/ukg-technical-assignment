using System.Diagnostics.CodeAnalysis;

namespace Application.Errors;

public class ErrorResult
{
    public required string Message { get; set; }

    [SetsRequiredMembers]
    public ErrorResult(string message)
    {
        Message = message;
    }
}