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
        if (
            !HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken)
            || jwtToken is not DecodedJwtToken decodedToken
        )
        {
            return Unauthorized();
        }

        var organization = decodedToken.Organizations.FirstOrDefault();
        if (organization is null)
        {
            return Unauthorized();
        }

        var credentials = await _credentialService.GetByOrg(organization);
        if (credentials is null)
        {
            return NotFound();
        }

        return Ok(credentials);
    }

    [HttpGet("/v1/org/credentials/{configId}")]
    public async Task<ActionResult<CredentialDTO>> GetByConfigId(
        [FromRoute] Guid configId
    )
    {
        if (
            !HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken)
            || jwtToken is not DecodedJwtToken decodedToken
        )
        {
            return Unauthorized();
        }

        var organization = decodedToken.Organizations.FirstOrDefault();
        if (organization is null)
        {
            return Unauthorized();
        }

        var credentials = await _credentialService.GetByConfigId(configId);
        if (credentials is null)
        {
            return NotFound();
        }

        return Ok(credentials);
    }

    [HttpPatch("/v1/org/credentials/{credentialId}/activate")]
    public async Task<ActionResult<dynamic>> Activate(Guid credentialId)
    {
        if (
            !HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken)
            || jwtToken is not DecodedJwtToken decodedToken
        )
        {
            return Unauthorized();
        }

        var organization = decodedToken.Organizations.FirstOrDefault();
        if (organization is null)
        {
            return Unauthorized();
        }

        var activated = await _credentialService.ActivateById(credentialId);

        return Ok(new { success = activated });
    }

    [HttpPatch("/v1/org/credentials/{credentialId}/deactivate")]
    public async Task<ActionResult<dynamic>> Deactivate(Guid credentialId)
    {
        if (
            !HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken)
            || jwtToken is not DecodedJwtToken decodedToken
        )
        {
            return Unauthorized();
        }

        var organization = decodedToken.Organizations.FirstOrDefault();
        if (organization is null)
        {
            return Unauthorized();
        }

        var deactivated = await _credentialService.DeactivateById(credentialId);

        return Ok(new { success = deactivated });
    }
}
