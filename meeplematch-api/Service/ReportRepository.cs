using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;

namespace meeplematch_api.Service;

public class ReportRepository : IReportRepository
{
    private readonly IMapper _mapper;
    private readonly MeepleMatchContext _context;

    public ReportRepository(IMapper mapper, MeepleMatchContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IEnumerable<ReportDTO> GetReports()
    {
        return _mapper.Map<IEnumerable<ReportDTO>>(_context.Reports);
    }

    public ReportDTO GetReport(int reportId)
    {
        var entity = _context.Reports.FirstOrDefault(r => r.IdReport == reportId);
        return _mapper.Map<ReportDTO>(entity);
    }

    public void InsertReport(ReportDTO report)
    {
        _context.Reports.Add(_mapper.Map<Report>(report));
        _context.SaveChanges();
    }

    public void UpdateReport(ReportDTO report, int reportId)
    {
        throw new NotImplementedException();
    }

    public void DeleteReport(int reportId)
    {
        var entity = _context.Reports.FirstOrDefault(r => r.IdReport == reportId);
        _context.Reports.Remove(entity);
        _context.SaveChanges();
    }
}