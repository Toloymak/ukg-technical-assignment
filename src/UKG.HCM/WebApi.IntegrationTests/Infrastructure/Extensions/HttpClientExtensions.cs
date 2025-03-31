using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using UKG.HCM.WebApi.Endpoints.Login;

namespace WebApi.IntegrationTests.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static async Task LoginAs(this HttpClient client, LogInRequest loginRequest)
    {
        var loginResponse = await client.PostAsJsonAsync("/auth/login", loginRequest);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var loginContent = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginContent!.AccessToken);   
    }
}