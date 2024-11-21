[XmlRoot(
    ElementName = "Envelope",
    Namespace = "http://schemas.xmlsoap.org/soap/envelope/"
)]
public class Envelope
{
    [XmlElement(ElementName = "Body")]
    public Body Body { get; set; } = null!;
}

public class Body
{
    [XmlElement(
        ElementName = "SubmitRequest",
        Namespace = "http://emkt.pjm.com/emkt/xml"
    )]
    public SubmitRequest SubmitRequest { get; set; } = null!;
}

[XmlRoot(
    ElementName = "SubmitRequest",
    Namespace = "http://emkt.pjm.com/emkt/xml"
)]
public class SubmitRequest
{
    [XmlElement(ElementName = "DemandBid")]
    public List<DemandBidSubmitRequest> DemandBids { get; set; } =
        new List<DemandBidSubmitRequest>();
}

public class DemandBidSubmitRequest
{
    [XmlAttribute(AttributeName = "location")]
    public string Location { get; set; } = null!;

    [XmlAttribute(AttributeName = "day")]
    public string Day { get; set; } = null!;

    [XmlElement(ElementName = "DemandBidHourly")]
    public List<DemandBidHourly> DemandBidHourlyList { get; set; } = null!;
}

public class DemandBidHourly
{
    [XmlAttribute(AttributeName = "hour")]
    public int Hour { get; set; }

    [XmlAttribute(AttributeName = "isDuplicateHour")]
    public bool IsDuplicateHour { get; set; }

    [XmlElement(ElementName = "FixedDemand")]
    public decimal FixedDemand { get; set; }
}
