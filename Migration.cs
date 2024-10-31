using System;
using System.Collections.Generic;

namespace LoadSchedulingAPI;

public partial class Migration
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
