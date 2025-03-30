using AutoFixture;

namespace WebApi.IntegrationTests.Infrastructure;

public class BaseIntegrationTest
{
    private readonly CustomWebApplicationFactory _webApplicationFactory;
    
    protected readonly Fixture Fixture = new Fixture();

    public BaseIntegrationTest()
    {
        _webApplicationFactory = new CustomWebApplicationFactory();
    }

    public HttpClient GetClient() => _webApplicationFactory.CreateClient();
}