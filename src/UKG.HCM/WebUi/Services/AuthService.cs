using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using WebUi.Models;

namespace WebUi.Services;

public class AuthService : AuthenticationStateProvider
{
    private readonly HttpClient _client;
    private string? _accessToken;

    public AuthService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var response = await _client.PostAsJsonAsync("/auth/login", request);
        if (!response.IsSuccessStatusCode) return false;

        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        _accessToken = auth?.AccessToken;
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        return true;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (string.IsNullOrWhiteSpace(_accessToken))
        {
            var anon = new ClaimsPrincipal(new ClaimsIdentity());
            return Task.FromResult(new AuthenticationState(anon));
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(_accessToken);

        var identity = new ClaimsIdentity(token.Claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }
}