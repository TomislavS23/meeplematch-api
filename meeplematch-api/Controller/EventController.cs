using meeplematch_api.DTO;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/meeplematch/events")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _repository;

    public EventController(IEventRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult FindAll()
    {
        try
        {
            return Ok(_repository.FindAll());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult FindById(int id)
    {
        try
        {
            return Ok(_repository.FindById(id));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public IActionResult Save([FromBody] EventDTO dto)
    {
        try
        {
            _repository.Save(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromBody] EventDTO dto, int id)
    {
        try
        {
            _repository.Update(dto, id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _repository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}