using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.DTO;

namespace api.Services.Interfaces;

public interface INotificationService
{
    Task<int> GetUnreadCountAsync(string userId);
    Task<List<NotificationDTO>> GetRecentNotificationsAsync(string userId, int count);
    Task MarkAsReadAsync(int notificationId, string userId);
    Task MarkAllAsReadAsync(string userId);
}

public class NotificationDTO
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? RelatedEntityId { get; set; }
}
