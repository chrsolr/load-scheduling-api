[ApiController]
[Route("/v1/org/configs")]
public class OrgConfigurationController : ControllerBase
{
    [HttpGet]
    public ActionResult<dynamic> GetConfigurations()
    {
        return Ok("Get Configurations");
    }
}
