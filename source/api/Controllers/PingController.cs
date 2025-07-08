using Microsoft.AspNetCore.Mvc;

namespace NortheastMegabuck.Api.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public sealed class PingController
    : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
        => Ok("Pong");
}
