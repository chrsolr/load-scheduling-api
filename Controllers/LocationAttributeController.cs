[ApiController]
public class LocationAttributeController : ControllerBase
{
    [HttpGet("/v1/org/location-attributes")]
    public ActionResult<dynamic> GetByOrg()
    {
        HttpContext.Items.TryGetValue("DecodedJwtToken", out var token);

        var decodedToken = token as DecodedJwtToken;
        var org = decodedToken?.Organizations.FirstOrDefault();

        if (decodedToken is null || org is null)
        {
            return Unauthorized();
        }

        // TODO: Get Location Attribute
        return Ok("Hey");
    }
}
