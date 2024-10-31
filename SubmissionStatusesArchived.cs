using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class SubmissionStatusesArchived
{
    public string SubmissionStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<SubmissionsMetaArchived> SubmissionsMetaArchiveds { get; set; } = new List<SubmissionsMetaArchived>();
}
