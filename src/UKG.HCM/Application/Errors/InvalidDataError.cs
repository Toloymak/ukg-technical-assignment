using System.Diagnostics.CodeAnalysis;

namespace Application.Errors;

public class InvalidDataError : ErrorResult
{
    [SetsRequiredMembers]
    public InvalidDataError(string message) : base(message)
    {
    }
}