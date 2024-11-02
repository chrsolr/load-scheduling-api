[ApiController]
public class CredentialController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ICredentialService _credentialService;

    public CredentialController(
        ILogger<CredentialController> logger,
        ICredentialService credentialService
    )
    {
        _logger = logger;
        _credentialService = credentialService;
    }

    [HttpGet("/v1/org/credentials")]
    public async Task<ActionResult<List<CredentialDTO>>> GetByOrg()
    {
        HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken);

        var decodedToken = jwtToken as DecodedJwtToken;
        var org = decodedToken?.Organizations.FirstOrDefault();

        if (decodedToken is null || org is null)
        {
            return Unauthorized();
        }

        var credentials = await _credentialService.GetByOrg(org);

        if (credentials is null)
        {
            return NotFound();
        }

        return Ok(credentials);
    }
}
