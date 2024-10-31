using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class Submission
{
    public Guid SubmissionId { get; set; }

    public Guid SubmissionGroupId { get; set; }

    public string Org { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public string? ContractName { get; set; }

    public string SchedulingType { get; set; } = null!;

    public string Intervals { get; set; } = null!;

    public string? Message { get; set; }

    public string? MarketResponse { get; set; }

    public string? MarketRequest { get; set; }

    public string? Mrid { get; set; }

    public string? BidId { get; set; }

    public string? SubmitType { get; set; }

    public string? SubmitStatus { get; set; }

    public string SubmissionStatus { get; set; } = null!;

    public string SubmittedBy { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool WasSuccessful { get; set; }
}
