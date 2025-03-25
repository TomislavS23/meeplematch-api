using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class EventRating
{
    public int IdEventRating { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
