using System.Web;
using ChattyMoAPI.Models;
using ChattyMoAPI.Models.Exception;
using ChattyMoAPI.Models.Request;
using ChattyMoAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChattyMoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWithTokenModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName("Authenticate")]
    [Route("[action]")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel authenticationModel)
    {
        try
        {
            var user = await _userRepository.Authenticate(authenticationModel.Username, authenticationModel.Password);
            return Ok(user);
        }
        catch (NonExistentUserException exception)
        {
            _logger.LogDebug(exception, "Tried to authenticate User with non existent email!");
            return Problem("User with this username does not exist!", statusCode: StatusCodes.Status400BadRequest);
        }
        catch (InvalidUserPasswordException exception)
        {
            _logger.LogDebug(exception, "Tried to authenticate User with bad password!");
            return Problem("Invalid password!", statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while auth occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("Register")]
    [Route("[action]")]
    public async Task<IActionResult> Register([FromBody] RegistrationModel userModel)
    {
        try
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

            var user = await _userRepository.Register(userModel.Username, hashedPassword);
            return CreatedAtAction(nameof(GetUserById), new {id = user.Id}, user);
        }
        catch (DuplicateUserUsernameException exception)
        {
            _logger.LogDebug(exception, "Tried to create User with duplicate Username!");
            return Problem("User with this Username already exists!", statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while registering user occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [ActionName("GetUserById")]
    [Route("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            return Ok(user);
        }
        catch (NonExistentUserException exception)
        {
            _logger.LogDebug(exception, "Get user by Id does not exist!");
            return Problem("User does not exist!", statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while getting user by ID occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [ActionName("UpdatePassword")]
    [Route("{id:int}/[action]")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] PasswordChangeModel passwordChangeModel)
    {
        try
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId)
            {
                _logger.LogDebug("Tried to update password of User with Id {Id}", id);
                return Problem("You can not do that!", statusCode: StatusCodes.Status401Unauthorized);
            }

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordChangeModel.NewPassword);

            await _userRepository.UpdatePassword(id, passwordChangeModel.OldPassword, newPasswordHash);
            return Accepted();
        }
        catch (NonExistentUserException exception)
        {
            _logger.LogDebug(exception, "Get user by Id does not exist!");
            return Problem("User does not exist!", statusCode: StatusCodes.Status404NotFound);
        }
        catch (InvalidUserPasswordException exception)
        {
            _logger.LogDebug(exception, "Changing password with wrong old password!");
            return Problem("Old password is not correct password!", statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while updating user password occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<User>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [ActionName("FindByUsername")]
    [Route("[action]/")]
    [Route("[action]/{**username}")]
    public async Task<IActionResult> FindByUsername(string? username = null)
    {
        try
        {
            var users = await _userRepository.FindByUsername(HttpUtility.UrlDecode(username));
            return Ok(users);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while finding user by username occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}