using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class User
{
    public int IdUser { get; set; }

    public Guid Uuid { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] HashedPassword { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public int RoleId { get; set; }

    public bool? IsBanned { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<EventComment> EventComments { get; set; } = new List<EventComment>();

    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    public virtual ICollection<EventRating> EventRatings { get; set; } = new List<EventRating>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<GlobalMessage> GlobalMessages { get; set; } = new List<GlobalMessage>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Telemetry> Telemetries { get; set; } = new List<Telemetry>();

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
