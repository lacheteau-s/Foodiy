using Foodiy.Api.Controllers;

namespace Foodiy.Api.UnitTests.Controllers;

public class DiagnosticsControllerTests
{
    [Fact]
    public void Ping_ReturnsPong()
    {
        var controller = new DiagnosticsController();
        
        var result = controller.Ping();

        Assert.Equal("pong", result);
    }
}