using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;

namespace meeplematch_api.Service;

public class ReportStatusRepository : IReportStatusRepository
{
    private readonly IMapper _mapper;
    private readonly MeepleMatchContext _context;

    public ReportStatusRepository(IMapper mapper, MeepleMatchContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IEnumerable<ReportStatusDTO> GetAllReportStatuses()
    {
        return _mapper.Map<IEnumerable<ReportStatusDTO>>(_context.ReportStatuses);
    }

    public ReportStatusDTO GetReportStatus(int reportStatusId)
    {
        return _mapper.Map<ReportStatusDTO>(
            _context.ReportStatuses.FirstOrDefault(s => s.IdReportStatus == reportStatusId));
    }

    public void InsertReportStatus(ReportStatusDTO reportStatus)
    {
        _context.ReportStatuses.Add(_mapper.Map<ReportStatus>(reportStatus));
        _context.SaveChanges();
    }

    public void UpdateReportStatus(ReportStatusDTO reportStatus, int reportStatusId)
    {
        var entity = _context.ReportStatuses.FirstOrDefault(s => s.IdReportStatus == reportStatusId);
        entity.Title = reportStatus.Title;

        _context.SaveChanges();
    }

    public void DeleteReportStatus(int reportStatusId)
    {
        var entity = _context.ReportStatuses.FirstOrDefault(s => s.IdReportStatus == reportStatusId);
        _context.ReportStatuses.Remove(entity);
        _context.SaveChanges();
    }
}