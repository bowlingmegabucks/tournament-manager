using Microsoft.AspNetCore.Mvc;

namespace NortheastMegabuck.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
internal sealed class PingController 
    : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => Ok("Pong");
}
