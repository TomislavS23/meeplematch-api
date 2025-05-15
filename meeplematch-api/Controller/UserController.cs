using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("/api/meeplematch/user")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    //[HttpGet, Authorize(Roles = "admin")]
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

    //[HttpGet("{id:int}"), Authorize(Roles = "admin")]
    [HttpGet("{id:int}"), Authorize]
    public IActionResult GetUser(int id)
    {
        try
        {
            var user = _userRepository.GetUser(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("public/{id:int}")]
    public IActionResult GetPublicUser(int id)
    {
        try
        {
            var user = _userRepository.GetUser(id);
            if (user == null) return NotFound();

            var publicDto = _mapper.Map<PublicUserDTO>(user);

            return Ok(publicDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("public/{name}")]
    public IActionResult GetPublicUserByName(string name)
    {
        try
        {
            var user = _userRepository.GetUsers().FirstOrDefault(u => u.Username.Equals(name));
            if (user == null) return NotFound();

            var publicDto = _mapper.Map<PublicUserDTO>(user);

            return Ok(publicDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}"), Authorize(Roles = "admin")]
    public IActionResult UpdateUser([FromBody] CreateUserDTO user, int id)
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

    [HttpPut("me"), Authorize]
    public IActionResult UpdateSelf([FromBody] CreateUserDTO userDto)
    {
        try
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("No such user exists.");
            }

            var user = _userRepository.GetUsers().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _userRepository.UpdateUser(userDto, user.IdUser);

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

    [HttpPost, Authorize(Roles = "admin")]
    public IActionResult CreateUser([FromBody] CreateUserDTO newUser)
    {
        try
        {
            _userRepository.CreateUser(newUser);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    //
    [HttpPut("forget/{id:int}"), Authorize]
    public IActionResult AnonymizeUser(int id)
    {
        try
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            user.FirstName = "Deleted";
            user.LastName = "User";
            user.Username = $"deleted_user_{user.IdUser}_{timestamp}";
            user.Email = $"deleted_{user.IdUser}_{timestamp}@example.com";
            user.ImagePath = "/assets/images/neutral_profile.png";
            user.IsMale = true;
            user.HashedPassword = Guid.NewGuid().ToByteArray(); 
            user.Salt = Guid.NewGuid().ToByteArray();
            user.IsBanned = true;
            user.UpdatedAt = DateTime.UtcNow;

            CreateUserDTO dto = _mapper.Map<CreateUserDTO>(user);

            _userRepository.UpdateUser(_mapper.Map<CreateUserDTO>(user), user.IdUser);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}