using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/meeplematch/event-participant")]
public class EventParticipantController : ControllerBase
{
    private readonly IEventParticipantRepository _eventParticipantRepository;

    public EventParticipantController(IEventParticipantRepository eventParticipantRepository)
    {
        _eventParticipantRepository = eventParticipantRepository;
    }

    [HttpGet("{eventId:int}")]
    public IActionResult FindParticipantsByEventId(int eventId)
    {
        try
        {
            return Ok(_eventParticipantRepository.FindEventParticipantsByEventId(eventId));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public IActionResult InsertEventParticipant(EventParticipantDTO participant)
    {
        try
        {
            _eventParticipantRepository.InsertEventParticipant(participant);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public IActionResult RemoveEventParticipant(int eventId, int userId)
    {
        try
        {
            _eventParticipantRepository.RemoveEventParticipant(eventId, userId);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{eventId:int}")]
    public IActionResult RemoveEventParticipants(int eventId)
    {
        try
        {
            _eventParticipantRepository.RemoveEventParticipants(eventId);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}