using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class Credential
{
    public Guid CredentialId { get; set; }

    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public string? UserId { get; set; }

    public string? UserAccount { get; set; }

    public string? UserPassword { get; set; }

    public string UserSubAccount { get; set; } = null!;

    public string? Pfx { get; set; }

    public string? PfxPassphrase { get; set; }

    public bool IsActive { get; set; }

    public bool IsSuma { get; set; }

    public bool UseCertificate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Config Config { get; set; } = null!;

    public virtual ICollection<IbtContract> IbtContracts { get; set; } = new List<IbtContract>();

    public virtual ICollection<LocationAttribute> LocationAttributes { get; set; } = new List<LocationAttribute>();
}
