[ApiController]
[Route("/v1/internal")]
public class InternalController : ControllerBase
{
    [HttpGet("location-attributes")]
    public ActionResult<dynamic> GetLocationAttributes()
    {
        // TODO: Implement
        return Ok();
    }

    // [HttpPost("bids-offers/{market}/update-status/{submittedDate}")]
    // public ActionResult<dynamic> RunBidsAndOffersUpdateStatus(
    //     [FromRoute] string market,
    //     [FromRoute, RegularExpression(@"\d{4}-\d{2}-\d{2}")]
    //         string submittedDate
    // )
    // {
    //     if (market.ToUpperInvariant() != Market.ERCOT.ToString())
    //     {
    //         return BadRequest($"Market {market} is not supported");
    //     }
    //
    //     // TODO: Implement
    //
    //     return Ok(new { market, submittedDate });
    // }
}
