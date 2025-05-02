using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class ReportStatus
{
    public int IdReportStatus { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
