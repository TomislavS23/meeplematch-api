using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class Report
{
    public int IdReport { get; set; }

    public int ReportedBy { get; set; }

    public int? EventId { get; set; }

    public string Reason { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User ReportedByNavigation { get; set; } = null!;

    public virtual ReportStatus StatusNavigation { get; set; } = null!;
}
