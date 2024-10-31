using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class PriceInfo
{
    public Guid PriceInfoId { get; set; }

    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int IntervalStart { get; set; }

    public int IntervalEnd { get; set; }

    public decimal BidPrice { get; set; }

    public decimal OfferPrice { get; set; }

    public virtual Config Config { get; set; } = null!;
}
