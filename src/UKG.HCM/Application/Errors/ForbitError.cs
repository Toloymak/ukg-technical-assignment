using System.Diagnostics.CodeAnalysis;

namespace Application.Errors;

public class ForbidError : ErrorResult
{
    [SetsRequiredMembers]
    public ForbidError(string message) : base(message)
    {
    }
}