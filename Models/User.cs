using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Username { get; set; }
    public string? Salt { get; set; }
    public string? Hash { get; set; }
    public string? ProfileImage { get; set; }
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastActive { get; set; }

    // Navigation properties
    // public virtual ICollection<Message> SentMessages { get; set; }
    // public virtual ICollection<Message> ReceivedMessages { get; set; }
    // public virtual ICollection<Friend> Friends { get; set; }
    // public virtual ICollection<Friend> FriendedBy { get; set; }
    // public virtual ICollection<FriendRequest> SentFriendRequests { get; set; }
    // public virtual ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
    // public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
    public object? FavoriteGames { get; internal set; }
    public object? ChatParticipants { get; internal set; }
    public object? BlockedByUsers { get; internal set; }

    // public User()
    // {
    //     SentMessages = new HashSet<Message>();
    //     ReceivedMessages = new HashSet<Message>();
    //     Friends = new HashSet<Friend>();
    //     FriendedBy = new HashSet<Friend>();
    //     SentFriendRequests = new HashSet<FriendRequest>();
    //     ReceivedFriendRequests = new HashSet<FriendRequest>();
    // }
}
