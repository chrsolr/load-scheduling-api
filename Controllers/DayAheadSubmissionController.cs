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


        var groupedByLocation = body
            .DemandBids.GroupBy(v => v.Location)
            .Select(v => new { Key = v.Key, Items = v });

        var model = groupedByLocation
            .Select(v => new Envelope
            {
                Body = new Body
                {
                    SubmitRequest = new SubmitRequest
                    {
                        DemandBids = v
                            .Items.Select(v => new DemandBidSubmitRequest
                            {
                                Location = v.Location,
                                Day = v.Date,
                                DemandBidHourlyList = v
                                    .Intervals.Select(y => new DemandBidHourly
                                    {
                                        Hour = y.Interval ?? 0,
                                        FixedDemand = y.Load ?? 0,
                                        IsDuplicateHour = false,
                                    })
                                    .ToList(),
                            })
                            .ToList(),
                    },
                },
            })
            .ToList();

        string xml = XmlConverter.ObjectToXmlString(model);

        return Ok(
            model.Select(v => XmlConverter.ObjectToXmlString(v)).ToList()
        );
    }
}
