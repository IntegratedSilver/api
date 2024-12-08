using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO;

public class NotificationDTO
{
    public NotificationType? Type { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? RelatedEntityId { get; set; }
}

public class NotificationType
{
    public static NotificationType? FriendRequest { get; internal set; }
}

public interface INotificationService
{
}