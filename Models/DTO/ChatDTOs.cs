// Models/DTO/ChatDTOs.cs
namespace api.Models.DTO;

public class ChatRoomDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int MembersCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserProfileDTO? Creator { get; set; }
    public bool IsPrivate { get; set; }
}

public class CreateChatRoomDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public bool IsPrivate { get; set; }
}

public class ChatMessageDTO
{
    public int Id { get; set; }
    public int ChatRoomId { get; set; }
    public UserProfileDTO? Sender { get; set; }
    public string? Content { get; set; }
    public string? MessageType { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsEdited { get; set; }
}

public class SendMessageDTO
{
    public int ChatRoomId { get; set; }
    public string? Content { get; set; }
    public string MessageType { get; set; } = "text";
}

public class DirectMessageDTO
{
    public int Id { get; set; }
    public UserProfileDTO? Sender { get; set; }
    public UserProfileDTO? Receiver { get; set; }
    public string? Content { get; set; }
    public string? MessageType { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsEdited { get; set; }
}

public class SendDirectMessageDTO
{
    public int ReceiverId { get; set; }
    public string? Content { get; set; }
    public string MessageType { get; set; } = "text";
}