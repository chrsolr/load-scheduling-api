[ApiController]
[Route("/v1/org/configs")]
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

    [HttpGet]
    public async Task<ActionResult<List<OrgConfigDTO>>> GetAll(
        [FromQuery] bool includeInactive = false
    )
    {
        var configs = await _orgConfigService.GetAll(includeInactive);

        if (configs is null)
        {
            return NotFound();
        }

        return Ok(configs);
    }
}
