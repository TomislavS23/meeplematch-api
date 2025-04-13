using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class EventComment
{
    public int IdEventComment { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
