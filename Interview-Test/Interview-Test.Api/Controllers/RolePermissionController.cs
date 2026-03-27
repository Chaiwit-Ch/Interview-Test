using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionController : ControllerBase
{
    private readonly IRolePermissionRepository _permissionRepository;

    public RolePermissionController(IRolePermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [HttpGet("GetRolePermission")]
    public ActionResult GetRolePermission()
    {
        var result = _permissionRepository.GetRolePermission();
        return Ok(result);
    }

    [HttpPost("CreateRolePermission")]
    public ActionResult CreateRolePermission(RolePermissionMappingModel rolePermission)
    {
        try
        {
            var result = _permissionRepository.CreateRolePermission(rolePermission);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}