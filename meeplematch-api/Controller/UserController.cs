using meeplematch_api.DTO;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/user-management/user")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet, Authorize]
    public IActionResult GetUsers()
    {
        try
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}"), Authorize]
    public IActionResult GetUser(int id)
    {
        try
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}"), Authorize]
    public IActionResult UpdateUser([FromBody] UserDTO user, int id)
    {
        try
        {
            _userRepository.UpdateUser(user, id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "admin")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            _userRepository.DeleteUser(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}