using ChattyMoAPI.Models.Request;
using ChattyMoAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChattyMoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatMessageController : ControllerBase
{
    private readonly IChatMessageRepository _chatMessageRepositoryRepository;
    private readonly ILogger<ChatMessageController> _logger;

    public ChatMessageController(IChatMessageRepository chatMessageRepositoryRepository,
        ILogger<ChatMessageController> logger)
    {
        _chatMessageRepositoryRepository = chatMessageRepositoryRepository;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [ActionName("Create")]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] ChatMessageCreateModel chatMessageCreateModel)
    {
        try
        {
            var currentUserId = int.Parse(User.Identity.Name);
            await _chatMessageRepositoryRepository.Create(currentUserId, chatMessageCreateModel.Message);
            return Accepted();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while updating creating chat message occured");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatMessageWithUserModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [ActionName("GetLatest")]
    [Route("[action]")]
    public async Task<IActionResult> GetLatest()
    {
        try
        {
            var messages = await _chatMessageRepositoryRepository.GetLatest();
            return Ok(messages);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while loading latest messages");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}