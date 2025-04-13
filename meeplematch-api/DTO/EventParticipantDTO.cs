namespace meeplematch_api.DTO;

public class EventParticipantDTO
{
    public int IdEventParticipant { get; set; }

    public int IdEvent { get; set; }

    public int IdUser { get; set; }

    public DateTime? JoinedAt { get; set; }
}