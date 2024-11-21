[ApiController]
public class DayAheadSubmissionController : ControllerBase
{
    [HttpPost("/v1/day-ahead/convert")]
    public ActionResult<DayAheadConvertDTO> ConvertToIsoFormat(
        [FromBody] DayAheadConvertDTO body
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

        var dstDates = DateUtils.GetDaylightSavingTimeDates("America/Chicago");

        Console.WriteLine($"DST Start: {dstDates.StartDate}");
        Console.WriteLine($"DST End: {dstDates.EndDate}");

        // TODO:
        // - Clean Up
        // - Split by Location, Date
        // - Implement DST logic

        var demandBids = body
            .DemandBids.GroupBy(v => v.Location)
            .SelectMany(v =>
                v.Select(x => new DemandBidSubmitRequest
                {
                    Location = x.Location,
                    Day = x.Date,
                    DemandBidHourlyList = x
                        .Intervals.Select(y => new DemandBidHourly
                        {
                            Hour = y.Interval ?? 0,
                            FixedDemand = y.Load ?? 0,
                            IsDuplicateHour = false,
                        })
                        .ToList(),
                })
            )
            .ToList();

        var convertedXmlString = new Envelope
        {
            Body = new Body
            {
                SubmitRequest = new SubmitRequest { DemandBids = demandBids },
            },
        };

        string xml = XmlConverter.ObjectToXmlString(convertedXmlString);

        return Ok(xml);
    }
}
