using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/meeplematch/report")]
public class ReportController : ControllerBase
{
    private readonly IReportRepository _reportRepository;

    public ReportController(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    [HttpGet]
    public IActionResult GetReports()
    {
        try
        {
            return Ok(_reportRepository.GetReports());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetReport(int id)
    {
        try
        {
            return Ok(_reportRepository.GetReport(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult InsertReport(ReportDTO report)
    {
        try
        {
            _reportRepository.InsertReport(report);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult UpdateReport(ReportDTO report, int id)
    {
        try
        {
            _reportRepository.UpdateReport(report, id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteReport(int id)
    {
        try
        {
            _reportRepository.DeleteReport(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}