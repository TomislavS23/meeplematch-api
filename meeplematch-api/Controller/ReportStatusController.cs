using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/meeplematch/report-status")]
public class ReportStatusController : ControllerBase
{
    private readonly IReportStatusRepository _reportStatusRepository;

    public ReportStatusController(IReportStatusRepository reportStatusRepository)
    {
        _reportStatusRepository = reportStatusRepository;
    }

    [HttpGet]
    public IActionResult GetAllReportStatuses()
    {
        try
        {
            return Ok(_reportStatusRepository.GetAllReportStatuses());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetReportStatusById(int id)
    {
        try
        {
            return Ok(_reportStatusRepository.GetReportStatus(id));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public IActionResult InsertReportStatus(ReportStatusDTO reportStatus)
    {
        try
        {
            _reportStatusRepository.InsertReportStatus(reportStatus);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public IActionResult UpdateReportStatus(ReportStatusDTO reportStatus, int id)
    {
        try
        {
            _reportStatusRepository.UpdateReportStatus(reportStatus, id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteReportStatus(int id)
    {
        try
        {
            _reportStatusRepository.DeleteReportStatus(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}