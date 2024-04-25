using backend.DAL;
using backend.Models;
using backend.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [EnableCors]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var username = loginDTO.Email;
        var password = loginDTO.Password;
        if (username == null || password == null)
        {
            return BadRequest();
        }

        var user = _userRepository.GetByEmail(loginDTO.Email);

        if (user == null)
        {
            return NotFound();
        }

        var loggedUser = await _userRepository.Login(username, password);

        if (!loggedUser)
        {
            return Unauthorized();
        }

        return Ok();
    }

    [EnableCors]
    [HttpPost("register")]
    public Task<UserDTO?> Register(RegisterDTO registerDTO)
    {
        var username = registerDTO.Email;
        var password = registerDTO.Password;
        var name = registerDTO.Name;

        if (username == null || password == null || name == null)
        {
            return null;
        }

        var user = _userRepository.GetByEmail(registerDTO.Email);

        if (user != null)
        {
            return null;
        }

        var newUser = _userRepository.Register(name, username, password);

        return new UserDTO
        {
            Name = newUser.Name,
            Email = newUser.Email,
        };
    }
}
