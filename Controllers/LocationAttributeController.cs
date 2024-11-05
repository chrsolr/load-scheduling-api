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
        HttpContext.Items.TryGetValue("DecodedJwtToken", out var token);

        var decodedToken = token as DecodedJwtToken;
        var org = decodedToken?.Organizations.FirstOrDefault();

        if (decodedToken is null || org is null)
        {
            return Unauthorized();
        }

        var locations = await _locationAttributeService.GetByOrg(org);

        if (locations is null)
        {
            return NotFound();
        }

        return Ok(locations);
    }
}
