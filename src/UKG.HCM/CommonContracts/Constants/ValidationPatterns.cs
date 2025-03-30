using System.Text.RegularExpressions;

namespace CommonContracts.Constants;

public static partial class ValidationPatterns
{
    // ToDo: Add a more comprehensive regex pattern for email validation
    private const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    
    /// Regex pattern for validating email addresses
    [GeneratedRegex(Email, RegexOptions.IgnoreCase)]
    public static partial Regex EmailRegex();
}