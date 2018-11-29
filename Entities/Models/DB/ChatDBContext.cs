using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Encrypt;

namespace Entities
{
    public partial class ChatDBContext : DbContext
    {
        EncryptManager encrypt = new EncryptManager();
        private static string conectionStrings = "";


        public ChatDBContext()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var builder = new ConfigurationBuilder().AddJsonFile(path, optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string conectionStringsEncrypted = configuration["Settings:conectionStrings"];
            conectionStrings = encrypt.DecryptTextBase64(conectionStringsEncrypted);
        }

        public ChatDBContext(DbContextOptions<ChatDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserSessions> UserSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conectionStrings);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");

                entity.Property(e => e.DestinyUserId).HasColumnName("DestinyUserID");

                entity.Property(e => e.MessageDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.OriginUserId).HasColumnName("OriginUserID");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ChatRoom_Messages");

                entity.HasOne(d => d.OriginUser)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.OriginUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OriginUserId_Messages");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("IX_User")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CompanyDepartment).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UserSessions>(entity =>
            {
                entity.Property(e => e.UserSessionsId).HasColumnName("UserSessionsID");

                entity.Property(e => e.ConnectionId).HasMaxLength(50);

                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.LogoutDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

            });
        }
    }
}
