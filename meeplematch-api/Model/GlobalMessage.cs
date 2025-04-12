using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class GlobalMessage
{
    public int IdGlobalMessage { get; set; }

    public string Message { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;
}
