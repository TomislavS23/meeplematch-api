namespace meeplematch_api.DTO;

public class EventRatingDTO
{
    public int IdEventRating { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public DateTime? CreatedAt { get; set; }
}