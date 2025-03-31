using JetBrains.Annotations;

namespace WebUi.Models;

[UsedImplicitly]
public class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
}