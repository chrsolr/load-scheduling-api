[ApiController]
public class ConfigController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IConfigService _configService;

    public ConfigController(
        ILogger<ConfigController> logger,
        IConfigService configService
    )
    {
        _logger = logger;
        _configService = configService;
    }

    [HttpGet("/v1/org/configs")]
    public async Task<ActionResult<List<ConfigDTO>>> GetByOrg()
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

        var configs = await _configService.GetByOrg(organization);
        if (configs is null)
        {
            return NotFound();
        }

        return Ok(configs);
    }

    [HttpGet("/v1/org/configs/{configId}")]
    public async Task<ActionResult<ConfigDTO>> GetByConfigId(
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

        var config = await _configService.GetById(configId);
        if (config is null)
        {
            return NotFound();
        }

        return Ok(config);
    }

    [HttpPatch("/v1/org/configs/{configId}/activate")]
    public async Task<ActionResult<dynamic>> Activate(Guid configId)
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

        var activated = await _configService.ActivateById(configId);

        return Ok(new { success = activated });
    }

    [HttpPatch("/v1/org/configs/{configId}/deactivate")]
    public async Task<ActionResult<dynamic>> Deactivate(Guid configId)
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

        var deactivated = await _configService.DeactivateById(configId);

        return Ok(new { success = deactivated });
    }
}
