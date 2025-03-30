using System.Diagnostics.CodeAnalysis;

namespace UKG.HCM.Application.Errors;

public class InvalidDataError : ErrorResult
{
    [SetsRequiredMembers]
    public InvalidDataError(string message) : base(message)
    {
    }
}