using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class DefaultNop
{
    public Guid NopId { get; set; }

    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Bid { get; set; }

    public int Offer { get; set; }

    public virtual Config Config { get; set; } = null!;
}
