using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class EventParticipant
{
    public int IdEventParticipant { get; set; }

    public int IdEvent { get; set; }

    public int IdUser { get; set; }

    public DateTime? JoinedAt { get; set; }

    public virtual Event IdEventNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
