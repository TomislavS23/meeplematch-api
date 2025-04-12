using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class Telemetry
{
    public int IdTelemetry { get; set; }

    public int? UserId { get; set; }

    public string EventType { get; set; } = null!;

    public string? EventData { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
