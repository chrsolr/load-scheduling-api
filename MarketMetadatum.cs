using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class MarketMetadatum
{
    public Guid MarketMetadataId { get; set; }

    public string Market { get; set; } = null!;

    public string Timezone { get; set; } = null!;

    public string CloseTime { get; set; } = null!;

    public string ClearingTime { get; set; } = null!;

    public short MaxDate { get; set; }

    public string Region { get; set; } = null!;
}
