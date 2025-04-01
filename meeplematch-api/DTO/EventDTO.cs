namespace meeplematch_api.DTO;

public class EventDTO
{
    public int IdEvent { get; set; }

    public Guid? Uuid { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Game { get; set; }

    public string Location { get; set; }

    public DateTime EventDate { get; set; }

    public int? Capacity { get; set; }

    public int? MinParticipants { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}