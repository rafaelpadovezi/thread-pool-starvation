using System.Threading.Tasks;
using AspnetTemplate.Tests.Support;
using Xunit;

namespace AspnetTemplate.Tests;

public class ProgramTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _factory;

    public ProgramTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_HealthCheck_ShouldReturnHealthy()
    {
        var client = _factory.CreateClient();

        var result = await client.GetStringAsync("health");

        Assert.Equal("Healthy", result);

    }
}