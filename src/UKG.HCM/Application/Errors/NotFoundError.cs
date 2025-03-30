using System.Diagnostics.CodeAnalysis;

namespace Application.Errors;

public class NotFoundError : ErrorResult
{
    [SetsRequiredMembers]
    public NotFoundError(string message) : base(message)
    {
    }
}