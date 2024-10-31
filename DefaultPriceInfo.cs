using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class DefaultPriceInfo
{
    public Guid PriceInfoId { get; set; }

    public Guid ConfigId { get; set; }

    public string Market { get; set; } = null!;

    public string Org { get; set; } = null!;

    public decimal BidPrice { get; set; }

    public decimal OfferPrice { get; set; }

    public virtual Config Config { get; set; } = null!;
}
