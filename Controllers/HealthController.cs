using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class HealthController : ControllerBase
{
    [HttpGet("health")]
    public ActionResult<dynamic> GetVersion()
    {
        var version = GetType().Assembly?.GetName().Version?.ToString() ?? "N/A";
        var name = GetType().Assembly?.GetName().Name?.ToString() ?? "N/A";

        return Ok(new { version, name });
    }
}
