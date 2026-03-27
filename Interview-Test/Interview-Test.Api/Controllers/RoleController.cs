using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet("GetRole")]
    public ActionResult GetRole()
    {
        var result = _roleRepository.GetRole();
        return Ok(result);
    }

    [HttpPost("CreateRole")]
    public ActionResult CreateRole(RoleModel role)
    {
        try
        {
            var result = _roleRepository.CreateRole(role);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}