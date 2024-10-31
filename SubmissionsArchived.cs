using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class SubmissionsArchived
{
    public Guid SubmissionId { get; set; }

    public Guid TransactionId { get; set; }

    public string Org { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Intervals { get; set; } = null!;

    public string? MarketResponse { get; set; }

    public string? Message { get; set; }

    public string? MarketRequest { get; set; }

    public string? Mrid { get; set; }

    public string? BidId { get; set; }

    public string? SubmitType { get; set; }

    public string? SubmitStatus { get; set; }

    public Guid? SubmissionMetaId { get; set; }

    public string SubmittedBy { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool WasSuccessful { get; set; }
}
