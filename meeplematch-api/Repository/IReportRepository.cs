using meeplematch_api.DTO;

namespace meeplematch_api.Repository;

public interface IReportRepository
{
    IEnumerable<ReportDTO> GetReports();
    ReportDTO GetReport(int reportId);
    void InsertReport(ReportDTO report);
    void UpdateReport(ReportDTO report, int reportId);
    void DeleteReport(int reportId);
}