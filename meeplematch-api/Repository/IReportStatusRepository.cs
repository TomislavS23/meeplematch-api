using meeplematch_api.DTO;

namespace meeplematch_api.Repository;

public interface IReportStatusRepository
{
    IEnumerable<ReportStatusDTO> GetAllReportStatuses();
    ReportStatusDTO GetReportStatus(int reportStatusId);
    void InsertReportStatus(ReportStatusDTO reportStatus);
    void UpdateReportStatus(ReportStatusDTO reportStatus, int reportStatusId);
    void DeleteReportStatus(int reportStatusId);
}