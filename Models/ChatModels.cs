// Models/ChatModels.cs
using System;
using System.Collections.Generic;

namespace api.Models;

public class ChatRoomModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual UserModel Creator { get; set; } = null!;
    public virtual ICollection<ChatRoomMemberModel> Members { get; set; }
    public virtual ICollection<ChatMessageModel> Messages { get; set; }

    public ChatRoomModel()
    {
        CreatedAt = DateTime.UtcNow;
        Members = new List<ChatRoomMemberModel>();
        Messages = new List<ChatMessageModel>();
    }
}

public class ChatRoomMemberModel
{
    public int Id { get; set; }
    public int ChatRoomId { get; set; }
    public int UserId { get; set; }
    public string Role { get; set; } = "member"; // admin, moderator, member
    public DateTime JoinedAt { get; set; }
    public DateTime? LastRead { get; set; }

    public virtual ChatRoomModel ChatRoom { get; set; } = null!;
    public virtual UserModel User { get; set; } = null!;

    public ChatRoomMemberModel()
    {
        JoinedAt = DateTime.UtcNow;
    }
}

public class ChatMessageModel
{
    public int Id { get; set; }
    public int ChatRoomId { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string MessageType { get; set; } = "text"; // text, image, system
    public DateTime SentAt { get; set; }
    public bool IsEdited { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ChatRoomModel ChatRoom { get; set; } = null!;
    public virtual UserModel Sender { get; set; } = null!;

    public ChatMessageModel()
    {
        SentAt = DateTime.UtcNow;
    }
}

public class DirectMessageModel
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string MessageType { get; set; } = "text"; // text, image
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsEdited { get; set; }
    public bool IsDeleted { get; set; }

    public virtual UserModel Sender { get; set; } = null!;
    public virtual UserModel Receiver { get; set; } = null!;

    public DirectMessageModel()
    {
        SentAt = DateTime.UtcNow;
    }
}