using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/meeplematch/event-comment")]
public class EventCommentController : ControllerBase
{
    private readonly IEventCommentRepository _eventCommentRepository;

    public EventCommentController(IEventCommentRepository eventCommentRepository)
    {
        _eventCommentRepository = eventCommentRepository;
    }

    [HttpGet("{eventId:int}")]
    public IActionResult GetEventComments(int eventId)
    {
        try
        {
            return Ok(_eventCommentRepository.GetEventComments(eventId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult InsertEventComment(EventCommentDTO eventComment)
    {
        try
        {
            _eventCommentRepository.InsertEventComment(eventComment);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult UpdateEventComment(EventCommentDTO eventComment, int eventCommentId)
    {
        try
        {
            _eventCommentRepository.UpdateEventComment(eventComment, eventCommentId);
            return Ok();
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public IActionResult DeleteEventComment(int eventCommentId)
    {
        try
        {
            _eventCommentRepository.DeleteEventComment(eventCommentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}