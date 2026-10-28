using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}

public class MiddlewareAndStatusPagesTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MiddlewareAndStatusPagesTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task NonExistentUrl_ReturnsNotFoundStatusAndCustomErrorPage()
    {
        var response = await _client.GetAsync("/nieistnieje/strona-testowa");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
}