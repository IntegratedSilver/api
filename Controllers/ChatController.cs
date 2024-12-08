using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Models.DTO;
using api.Services;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatService _chatService;

    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("rooms")]
    public async Task<ActionResult<IEnumerable<ChatRoomDTO>>> GetChatRooms()
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        var rooms = await _chatService.GetChatRooms(int.Parse(userId));
        return Ok(rooms);
    }

    [HttpPost("rooms")]
    public ActionResult<ChatRoomDTO> CreateChatRoom(CreateChatRoomDTO createRoom)
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            if (userId == null) return Unauthorized();

            var newRoom = _chatService.CreateChatRoom(int.Parse(userId), createRoom);
            return Ok(newRoom);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("rooms/{roomId}")]
    public ActionResult<ChatRoomDTO> GetChatRoom(int roomId)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        var room = _chatService.GetChatRoom(int.Parse(userId), roomId);
        if (room == null) return NotFound();

        return Ok(room);
    }

    [HttpGet("rooms/{roomId}/messages")]
    public ActionResult<IEnumerable<ChatMessageDTO>> GetRoomMessages(
        int roomId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        var result = _chatService.GetRoomMessages(int.Parse(userId), roomId, page, pageSize);
        return Ok(result);
    }

    [HttpGet("direct")]
    public ActionResult<IEnumerable<DirectMessageDTO>> GetDirectMessages(
        [FromQuery] int userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var currentUserId = User.FindFirst("userId")?.Value;
        if (currentUserId == null) return Unauthorized();

        var result = _chatService.GetDirectMessages(int.Parse(currentUserId), userId, page, pageSize);
        return Ok(result);
    }

    [HttpPost("rooms/{roomId}/join")]
    public async Task<ActionResult> JoinRoom(int roomId)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        var result = await _chatService.JoinRoom(int.Parse(userId), roomId);
        if (!result) return BadRequest("Failed to join room");

        return Ok();
    }

    [HttpPost("rooms/{roomId}/leave")]
    public async Task<ActionResult> LeaveRoom(int roomId)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        var result = await _chatService.LeaveRoom(int.Parse(userId), roomId);
        if (!result) return BadRequest("Failed to leave room");

        return Ok();
    }
}