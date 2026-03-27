using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionController(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [HttpGet("GetPermission")]
    public ActionResult GetPermission()
    {
        var result = _permissionRepository.GetPermission();
        return Ok(result);
    }

    [HttpPost("CreatePermission")]
    public ActionResult CreatePermission(PermissionModel permission)
    {
        try
        {
            var result = _permissionRepository.CreatePermission(permission);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}