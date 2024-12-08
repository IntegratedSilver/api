using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Services.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    // Define DbSets for all models
    public DbSet<UserModel>? UserInfo { get; set; }
    public DbSet<BlogItemModel>? BlogInfo { get; set; }
    public DbSet<FriendModel>? Friends { get; set; }
    public DbSet<UserGameModel>? UserGames { get; set; }
    public DbSet<ChatRoomModel>? ChatRooms { get; set; }
    public DbSet<ChatRoomMemberModel>? ChatRoomMembers { get; set; }
    public DbSet<ChatMessageModel>? ChatMessages { get; set; }
    public DbSet<DirectMessageModel>? DirectMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Relationships
        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.FriendshipsInitiated)
            .WithOne(f => f.Requester)
            .HasForeignKey(f => f.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.FriendshipsReceived)
            .WithOne(f => f.Addressee)
            .HasForeignKey(f => f.AddresseeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.UserGames)
            .WithOne(ug => ug.User)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Blog Item Relationships
        modelBuilder.Entity<BlogItemModel>()
            .HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Chat Room Relationships
        modelBuilder.Entity<ChatRoomModel>()
            .HasOne(r => r.Creator)
            .WithMany()
            .HasForeignKey(r => r.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChatRoomModel>()
            .HasMany(r => r.Members)
            .WithOne(m => m.ChatRoom)
            .HasForeignKey(m => m.ChatRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChatRoomModel>()
            .HasMany(r => r.Messages)
            .WithOne(m => m.ChatRoom)
            .HasForeignKey(m => m.ChatRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        // Chat Room Member Relationships
        modelBuilder.Entity<ChatRoomMemberModel>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Chat Message Relationships
        modelBuilder.Entity<ChatMessageModel>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Direct Message Relationships
        modelBuilder.Entity<DirectMessageModel>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DirectMessageModel>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        // Friend Relationships
        modelBuilder.Entity<FriendModel>()
            .HasIndex(f => new { f.RequesterId, f.AddresseeId })
            .IsUnique();

        // User Game Relationships
        modelBuilder.Entity<UserGameModel>()
            .HasIndex(ug => new { ug.UserId, ug.GameId })
            .IsUnique();

        // Additional Configurations
        modelBuilder.Entity<UserModel>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<BlogItemModel>()
            .Property(b => b.IsDeleted)
            .HasDefaultValue(false);

        modelBuilder.Entity<ChatRoomModel>()
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<ChatMessageModel>()
            .Property(m => m.SentAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<DirectMessageModel>()
            .Property(m => m.SentAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Set default values for status fields
        modelBuilder.Entity<UserModel>()
            .Property(u => u.Status)
            .HasDefaultValue("offline");

        modelBuilder.Entity<FriendModel>()
            .Property(f => f.Status)
            .HasDefaultValue("pending");

        modelBuilder.Entity<ChatRoomMemberModel>()
            .Property(m => m.Role)
            .HasDefaultValue("member");
    }
}
