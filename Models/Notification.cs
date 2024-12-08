using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Notification
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public string? Message { get; set; }
    public NotificationType Type { get; set; }
    public string? RelatedEntityId { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public enum NotificationType
{
    FriendRequest,
    FriendRequestAccepted,
    NewPost,
    PostLike,
    PostComment,
    MessageReceived,
    SystemNotification,
    GameActivity
}


