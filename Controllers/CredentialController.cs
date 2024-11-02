[ApiController]
public class CredentialController : ControllerBase
{
    public CredentialController() { }

    [HttpGet("/v1/org/credential")]
    public ActionResult<dynamic> GetByOrg()
    {
        HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken);

        var decodedToken = jwtToken as DecodedJwtToken;
        var org = decodedToken?.Organizations.FirstOrDefault();

        if (decodedToken is null || org is null)
        {
            return Unauthorized();
        }

        // TODO: Get credentials dto here

        return Ok(new { });
    }
}
