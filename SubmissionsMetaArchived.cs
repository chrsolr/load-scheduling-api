using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class SubmissionsMetaArchived
{
    public Guid SubmissionMetaId { get; set; }

    public Guid SubmissionGroupId { get; set; }

    public string AggregateKey { get; set; } = null!;

    public string Org { get; set; } = null!;

    public string Market { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string StartFlowDate { get; set; } = null!;

    public string EndFlowDate { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string? SubmissionLog { get; set; }

    public string? SubmissionStatus { get; set; }

    public string? Submissions { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SubmissionStatusesArchived? SubmissionStatusNavigation { get; set; }
}
