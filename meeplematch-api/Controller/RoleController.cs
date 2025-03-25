using meeplematch_api.DTO;
using meeplematch_api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace meeplematch_api.Controller;

[ApiController]
[Route("api/role-management/role")]
public class RoleController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet, Authorize]
    public IActionResult GetRoles()
    {
        try
        {
            var roles = _roleRepository.GetRoles();
            return Ok(roles);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}"), Authorize]
    public IActionResult GetRole(int id)
    {
        try
        {
            var role = _roleRepository.GetRole(id);
            return Ok(role);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost, Authorize]
    public IActionResult CreateRole([FromBody] RoleDTO role)
    {
        try
        {
            _roleRepository.InsertRole(role);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}"), Authorize]
    public IActionResult UpdateRole([FromBody] RoleDTO role, int id)
    {
        try
        {
            _roleRepository.UpdateRole(role, id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}"), Authorize]
    public IActionResult DeleteRole(int id)
    {
        try
        {
            _roleRepository.DeleteRole(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}