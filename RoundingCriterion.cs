using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class RoundingCriterion
{
    public Guid RoundingCriteriaId { get; set; }

    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;

    public decimal Bid { get; set; }

    public decimal Offer { get; set; }

    public virtual Config Config { get; set; } = null!;
}
