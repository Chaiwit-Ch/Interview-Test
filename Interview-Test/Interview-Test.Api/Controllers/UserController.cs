using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetUsers")]
    public ActionResult GetUsers()
    {
        var result = _userRepository.GetUsers();

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("GetUserById/{id}")]
    public ActionResult GetUserById(string? id)
    {
        var result = _userRepository.GetUserById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("CreateUser")]
    public ActionResult CreateUser()
    {
        var result = _userRepository.CreateUser(null);

        if (result <= 0)
            return BadRequest("Create user failed");

        return Ok(result);
    }
}