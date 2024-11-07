[ApiController]
public class DayAheadSubmissionController : ControllerBase
{
    public DayAheadSubmissionController() { }

    [HttpPost("/v1/day-ahead/convert")]
    public ActionResult<DayAheadConvertDTO> ConvertToIsoFormat(
        [FromBody] DayAheadConvertDTO body
    )
    {
        return Ok(body);
    }
}
