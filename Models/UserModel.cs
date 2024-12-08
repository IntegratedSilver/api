using System;
using System.Collections.Generic;

namespace api.Models;

    public class UserModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public string? Avatar { get; set; }
        public string? Status { get; set; } // online, offline, in-game
        public DateTime LastActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        // Navigation properties
        public virtual ICollection<FriendModel>? FriendshipsInitiated { get; set; }
        public virtual ICollection<FriendModel>? FriendshipsReceived { get; set; }
        public virtual ICollection<UserGameModel>? UserGames { get; set; }

        public UserModel()
        {
            CreatedAt = DateTime.UtcNow;
            LastActive = DateTime.UtcNow;
            Status = "offline";
            IsDeleted = false;
        }

    }
    public class UserGameModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime AddedAt { get; set; }
        
        public virtual UserModel? User { get; set; }
    }

    public class FriendModel
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public int AddresseeId { get; set; }
        public string Status { get; set; } = "pending"; // pending, accepted, blocked
        public DateTime CreatedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        
        public virtual UserModel? Requester { get; set; }
        public virtual UserModel? Addressee { get; set; }

        public FriendModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }