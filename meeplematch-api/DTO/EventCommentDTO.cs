namespace meeplematch_api.DTO;

public class EventCommentDTO
{
    public int IdEventComment { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public string Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}