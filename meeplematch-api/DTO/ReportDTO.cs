namespace meeplematch_api.DTO;

public class ReportDTO
{
    public int IdReport { get; set; }

    public int ReportedBy { get; set; }

    public int? EventId { get; set; }

    public string Reason { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

}