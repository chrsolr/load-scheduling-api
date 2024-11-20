using System.Xml.Serialization;

public class DstDate
{
    [XmlElement]
    public DateTime Start { get; set; }

    [XmlElement]
    public DateTime End { get; set; }
}

[ApiController]
public class DayAheadSubmissionController : ControllerBase
{
    private static string ObjectToXml<T>(T obj)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringWriter stringWriter = new StringWriter())
        {
            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }
    }

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

        Console.WriteLine($"DST Start: {dstDates.startDate}");
        Console.WriteLine($"DST End: {dstDates.endDate}");

        string xml = ObjectToXml(
            new DstDate { Start = dstDates.startDate, End = dstDates.endDate }
        );

        Console.WriteLine($"XML: {xml}");

        return Ok(body);
    }
}
