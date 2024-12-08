using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.DTO;

namespace api.Services.Interfaces;
public interface IFriendService
{
    Task<FriendRequestResponseDTO> SendFriendRequest(int senderId, int receiverId);
    Task<FriendRequestResponseDTO> AcceptFriendRequest(int requestId, int userId);
    Task<bool> RejectFriendRequest(int requestId, int userId);
    Task<List<FriendDTO>> GetFriendsList(int userId);
    Task<List<FriendRequestDTO>> GetPendingRequests(int userId);
    Task<bool> RemoveFriend(int userId, int friendId);
    Task<bool> BlockUser(int userId, int blockedUserId, string reason);
    Task<bool> UnblockUser(int userId, int blockedUserId);
    Task<List<BlockedUserDTO>> GetBlockedUsers(int userId);
    Task<List<FriendDTO>> GetMutualFriends(int userId1, int userId2);
    Task<List<FriendStatusDTO>> GetFriendsStatus(int userId);
    Task<List<FriendActivityDTO>> GetFriendsActivity(int userId);
}

public class FriendActivityDTO
{
}

public class FriendStatusDTO
{
}

public class BlockedUserDTO
{
}

public class FriendDTO
{
    public string? ProfileImage { get; internal set; }
    public string? FriendName { get; internal set; }
    public int FriendId { get; internal set; }
    public int UserId { get; internal set; }
    public int Id { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
    public string? Status { get; internal set; }
}

public class FriendRequestResponseDTO
{
    internal int SenderId;

    public bool Success { get; internal set; }
    public string? Message { get; internal set; }
    public int ReceiverId { get; internal set; }
}