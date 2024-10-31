using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class IbtContract
{
    public Guid IbtContractId { get; set; }

    public Guid ConfigId { get; set; }

    public Guid CredentialId { get; set; }

    public string? ContractId { get; set; }

    public string Org { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public string ContractName { get; set; } = null!;

    public string SellerQseCode { get; set; } = null!;

    public string SellerName { get; set; } = null!;

    public string BuyerQseCode { get; set; } = null!;

    public string BuyerName { get; set; } = null!;

    public string BuySell { get; set; } = null!;

    public string Counterparty { get; set; } = null!;

    public string StartDate { get; set; } = null!;

    public string EndDate { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Config Config { get; set; } = null!;

    public virtual Credential Credential { get; set; } = null!;
}
