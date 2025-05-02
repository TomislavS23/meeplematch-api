using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class Event
{
    public int IdEvent { get; set; }

    public Guid? Uuid { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Game { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public int? Capacity { get; set; }

    public int? MinParticipants { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<EventComment> EventComments { get; set; } = new List<EventComment>();

    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    public virtual ICollection<EventRating> EventRatings { get; set; } = new List<EventRating>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
