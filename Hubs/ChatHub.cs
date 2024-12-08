using Microsoft.AspNetCore.SignalR;
using api.Models.DTO;
using api.Services;

namespace api.Hubs;

public class ChatHub : Hub
{
    private readonly ChatService _chatService;
    private readonly UserService _userService;
    private static readonly Dictionary<int, HashSet<string>> _userConnections = new();

    public ChatHub(ChatService chatService, UserService userService)
    {
        _chatService = chatService;
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        if (userId != null)
        {
            var userIdInt = int.Parse(userId);
            
            if (!_userConnections.ContainsKey(userIdInt))
            {
                _userConnections[userIdInt] = new HashSet<string>();
            }
            _userConnections[userIdInt].Add(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            await _userService.UpdateUserStatus(userIdInt, "online");

            var userProfile = await _userService.GetUserProfile(userIdInt);
            var friends = await _userService.GetFriends(userIdInt);
            foreach (var friend in friends)
            {
                await Clients.Group($"user_{friend.Id}")
                    .SendAsync("UserOnlineStatus", userProfile, true);
            }
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        if (userId != null)
        {
            var userIdInt = int.Parse(userId);
            
            if (_userConnections.ContainsKey(userIdInt))
            {
                _userConnections[userIdInt].Remove(Context.ConnectionId);
                if (!_userConnections[userIdInt].Any())
                {
                    _userConnections.Remove(userIdInt);
                    await _userService.UpdateUserStatus(userIdInt, "offline");
                    
                    var userProfile = await _userService.GetUserProfile(userIdInt);
                    var friends = await _userService.GetFriends(userIdInt);
                    foreach (var friend in friends)
                    {
                        await Clients.Group($"user_{friend.Id}")
                            .SendAsync("UserOnlineStatus", userProfile, false);
                    }
                }
            }
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(SendMessageDTO message)
    {
        var userId = GetUserId();
        if (userId == null) return;

        var chatMessage = await _chatService.AddMessage(userId.Value, message);
        if (chatMessage != null)
        {
            await Clients.Group($"room_{message.ChatRoomId}")
                .SendAsync("ReceiveMessage", chatMessage);
        }
    }

    public async Task SendDirectMessage(SendDirectMessageDTO message)
    {
        var userId = GetUserId();
        if (userId == null) return;

        var directMessage = await _chatService.AddDirectMessage(userId.Value, message);
        if (directMessage != null)
        {
            await Clients.Group($"user_{message.ReceiverId}")
                .SendAsync("ReceiveDirectMessage", directMessage);
            await Clients.Caller
                .SendAsync("ReceiveDirectMessage", directMessage);
        }
    }

    public async Task JoinRoom(int roomId)
    {
        var userId = GetUserId();
        if (userId == null) return;

        await Groups.AddToGroupAsync(Context.ConnectionId, $"room_{roomId}");
        await Clients.Group($"room_{roomId}")
            .SendAsync("UserJoinedRoom", roomId, await _userService.GetUserProfile(userId.Value));
    }

    public async Task LeaveRoom(int roomId)
    {
        var userId = GetUserId();
        if (userId == null) return;

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room_{roomId}");
        await Clients.Group($"room_{roomId}")
            .SendAsync("UserLeftRoom", roomId, await _userService.GetUserProfile(userId.Value));
    }

    public async Task MarkMessageAsRead(int messageId)
    {
        var userId = GetUserId();
        if (userId == null) return;

        await _chatService.MarkMessageAsRead(userId.Value, messageId);
    }

    public async Task UserTyping(int roomId, bool isTyping)
    {
        var userId = GetUserId();
        if (userId == null) return;

        var user = await _userService.GetUserProfile(userId.Value);
        await Clients.Group($"room_{roomId}")
            .SendAsync("UserTypingStatus", roomId, user, isTyping);
    }

    private int? GetUserId()
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        return userId != null ? int.Parse(userId) : null;
    }
}