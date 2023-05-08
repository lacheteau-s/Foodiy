using Microsoft.AspNetCore.Mvc;

namespace Foodiy.Api.Controllers;

[ApiController]
public class DiagnosticsController : ControllerBase
{
    [HttpGet("ping")]
    public string Ping()
    {
        return "pong";
    }
}
