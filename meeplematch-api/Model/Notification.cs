using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class Notification
{
    public int IdNotification { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
