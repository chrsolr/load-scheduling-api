using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class Config
{
    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Market { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();

    public virtual ICollection<DefaultNop> DefaultNops { get; set; } = new List<DefaultNop>();

    public virtual ICollection<DefaultPriceInfo> DefaultPriceInfos { get; set; } = new List<DefaultPriceInfo>();

    public virtual ICollection<IbtContract> IbtContracts { get; set; } = new List<IbtContract>();

    public virtual ICollection<LocationAttribute> LocationAttributes { get; set; } = new List<LocationAttribute>();

    public virtual ICollection<PriceInfo> PriceInfos { get; set; } = new List<PriceInfo>();

    public virtual RoundingCriterion? RoundingCriterion { get; set; }
}
