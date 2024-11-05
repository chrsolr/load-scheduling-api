[ApiController]
public class LocationAttributeController : ControllerBase
{
    private readonly ILogger<LocationAttributeController> _logger;
    private readonly ILocationAttributeService _locationAttributeService;

    public LocationAttributeController(
        ILogger<LocationAttributeController> logger,
        ILocationAttributeService locationAttributeService
    )
    {
        _logger = logger;
        _locationAttributeService = locationAttributeService;
    }

    [HttpGet("/v1/org/location-attributes")]
    public async Task<ActionResult<List<LocationAttributeDTO>>> GetByOrg()
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

        var locations = await _locationAttributeService.GetByOrg(organization);

        if (locations is null)
        {
            return NotFound();
        }

        return Ok(locations);
    }

    [HttpGet("/v1/org/location-attributes/{configId}")]
    public async Task<ActionResult<LocationAttributeDTO>> GetByConfigId(
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

        var locations = await _locationAttributeService.GetByConfigId(configId);
        if (locations is null)
        {
            return NotFound();
        }

        return Ok(locations);
    }
}
