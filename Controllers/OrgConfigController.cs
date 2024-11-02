[ApiController]
public class OrgConfigController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IOrgConfigService _orgConfigService;

    public OrgConfigController(
        ILogger<OrgConfigController> logger,
        IOrgConfigService orgConfigService
    )
    {
        _logger = logger;
        _orgConfigService = orgConfigService;
    }

    [HttpGet("/v1/org/configs")]
    public async Task<ActionResult<List<OrgConfigDTO>>> GetByOrg()
    {
        HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken);

        var decodedToken = jwtToken as DecodedJwtToken;

        if (decodedToken is null || !decodedToken.ToolPolicies.Any())
        {
            return Unauthorized();
        }

        var org = decodedToken?.Organizations.FirstOrDefault();

        if (org is null)
        {
            return NotFound();
        }

        var configs = await _orgConfigService.GetByOrg(org);

        if (configs is null)
        {
            return NotFound();
        }

        return Ok(configs);
    }

    [HttpGet("/v1/org/configs/{configId}")]
    public async Task<ActionResult<OrgConfigDTO>> GetByConfigId(
        [FromRoute] Guid configId
    )
    {
        HttpContext.Items.TryGetValue("DecodedJwtToken", out var jwtToken);

        var decodedToken = jwtToken as DecodedJwtToken;
        var org = decodedToken?.Organizations.FirstOrDefault();

        if (decodedToken is null || org is null)
        {
            return Unauthorized();
        }

        var config = await _orgConfigService.GetById(configId);
        if (config is null)
        {
            return NotFound();
        }

        return Ok(config);
    }
}
