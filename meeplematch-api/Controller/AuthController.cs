using meeplematch_api.DTO;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("api/meeplematch/auth/")]
public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpGet("login")]
    public IActionResult Login(string username, string password)
    {
        try
        {
            return Ok(_authRepository.Login(username, password));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO register)
    {
        try
        {
            return Ok(_authRepository.Register(register));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut("change-password"), Authorize]
    public IActionResult ChangePassword(string username, string oldPassword, string newPassword)
    {
        try
        {
            _authRepository.ChangePassword(username, oldPassword, newPassword);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}