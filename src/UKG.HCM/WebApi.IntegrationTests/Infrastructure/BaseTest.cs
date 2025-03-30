using AutoFixture;

namespace WebApi.IntegrationTests.Infrastructure;

public class BaseIntegrationTest : IAsyncDisposable
{
    private CustomWebApplicationFactory? _webApplicationFactory;
    
    protected readonly Fixture Fixture = new();
    
    [SetUp]
    public async Task Setup()
    {
        _webApplicationFactory = new CustomWebApplicationFactory();
        await _webApplicationFactory.InitializeAsync();
    }

    protected HttpClient GetClient()
        => _webApplicationFactory?.CreateClient()
           ?? throw new InvalidOperationException("WebApplicationFactory is not initialized.");

    [TearDown]
    public async Task TearDown()
    {
        if (_webApplicationFactory != null)
            await _webApplicationFactory.DisposeAsync();
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_webApplicationFactory != null)
            await _webApplicationFactory.DisposeAsync();
    }
}