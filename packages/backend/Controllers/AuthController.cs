using backend.DAL;
using backend.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var username = loginDTO.Email;
        var password = loginDTO.Password;

        var user = _userRepository.GetByEmail(username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var isAuthenticated = await _userRepository.Login(username, password);

        if (!isAuthenticated)
        {
            return Unauthorized("Incorrect credentials.");
        }

        return Ok();
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody]  RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var username = registerDTO.Email;
        var password = registerDTO.Password;
        var name = registerDTO.Name;

        var user = _userRepository.GetByEmail(registerDTO.Email);

        if (user != null)
        {
            return Conflict("The username is already registered.");
        }

        var newUser = _userRepository.Register(name, username, password);

        if (newUser == null)
        {
            return StatusCode(500, "There was a problem registering the user.");
        }

        var userDTO = new UserDTO { Name = newUser.Name, Email = newUser.Email };

        return Ok(userDTO);
    }
}
