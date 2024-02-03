using Microsoft.AspNetCore.Mvc;

namespace Foodiy.Api.Controllers;

public class DiagnosticsController : Controller
{
    [HttpGet("ping")]
    public string Ping()
    {
        return "pong";
    }
}
