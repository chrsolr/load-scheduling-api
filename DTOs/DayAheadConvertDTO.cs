public record DayAheadConvertDTO
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = String.Empty;

    [JsonPropertyName("userPassword")]
    public string UserPassword { get; set; } = String.Empty;

    [JsonPropertyName("demandBids")]
    public List<DemandBid> DemandBids { get; set; } = new List<DemandBid>();
}

public record DemandBid
{
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = String.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = String.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = String.Empty;

    [JsonPropertyName("intervals")]
    public List<DemandBidInterval> Intervals { get; set; } =
        new List<DemandBidInterval>();
}

public record DemandBidInterval
{
    [JsonPropertyName("interval")]
    public int? Interval { get; set; }

    [JsonPropertyName("load")]
    public decimal? Load { get; set; }
}
