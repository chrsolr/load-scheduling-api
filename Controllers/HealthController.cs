[ApiController]
[Route("/")]
public class HealthController : ControllerBase
{
    [HttpGet("health")]
    public ActionResult<dynamic> GetVersion()
    {
        var version =
            GetType().Assembly?.GetName().Version?.ToString() ?? "N/A";
        var name = "Load Scheduling API";
        var datetime = DateTime.UtcNow;

        return Ok(
            new
            {
                version,
                name,
                dateTime = datetime,
            }
        );
    }
}
