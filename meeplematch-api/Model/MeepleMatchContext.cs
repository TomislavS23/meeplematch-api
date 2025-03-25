using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace meeplematch_api.Model;

public partial class MeepleMatchContext : DbContext
{
    public MeepleMatchContext(DbContextOptions<MeepleMatchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventComment> EventComments { get; set; }

    public virtual DbSet<EventParticipant> EventParticipants { get; set; }

    public virtual DbSet<EventRating> EventRatings { get; set; }

    public virtual DbSet<GlobalMessage> GlobalMessages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportStatus> ReportStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<Telemetry> Telemetries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("event_pkey");

            entity.ToTable("event");

            entity.HasIndex(e => e.Uuid, "event_uuid_key").IsUnique();

            entity.HasIndex(e => e.EventDate, "idx_event_date");

            entity.HasIndex(e => e.Game, "idx_event_game");

            entity.HasIndex(e => e.Type, "idx_event_type");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EventDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("event_date");
            entity.Property(e => e.Game)
                .HasMaxLength(100)
                .HasColumnName("game");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.MinParticipants)
                .HasDefaultValue(2)
                .HasColumnName("min_participants");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Uuid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_created_by_fkey");
        });

        modelBuilder.Entity<EventComment>(entity =>
        {
            entity.HasKey(e => e.IdEventComment).HasName("event_comment_pkey");

            entity.ToTable("event_comment");

            entity.HasIndex(e => e.EventId, "idx_event_comment_event");

            entity.Property(e => e.IdEventComment).HasColumnName("id_event_comment");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.EventComments)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_comment_event_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.EventComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_comment_user_id_fkey");
        });

        modelBuilder.Entity<EventParticipant>(entity =>
        {
            entity.HasKey(e => e.IdEventParticipant).HasName("event_participant_pkey");

            entity.ToTable("event_participant");

            entity.HasIndex(e => new { e.IdEvent, e.IdUser }, "event_participant_id_event_id_user_key").IsUnique();

            entity.HasIndex(e => e.IdEvent, "idx_event_participant_event");

            entity.HasIndex(e => e.IdUser, "idx_event_participant_user");

            entity.Property(e => e.IdEventParticipant).HasColumnName("id_event_participant");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_at");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.IdEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_participant_id_event_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_participant_id_user_fkey");
        });

        modelBuilder.Entity<EventRating>(entity =>
        {
            entity.HasKey(e => e.IdEventRating).HasName("event_rating_pkey");

            entity.ToTable("event_rating");

            entity.HasIndex(e => new { e.EventId, e.UserId }, "event_rating_event_id_user_id_key").IsUnique();

            entity.HasIndex(e => e.EventId, "idx_event_rating_event");

            entity.Property(e => e.IdEventRating).HasColumnName("id_event_rating");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.EventRatings)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("event_rating_event_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.EventRatings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("event_rating_user_id_fkey");
        });

        modelBuilder.Entity<GlobalMessage>(entity =>
        {
            entity.HasKey(e => e.IdGlobalMessage).HasName("global_message_pkey");

            entity.ToTable("global_message");

            entity.Property(e => e.IdGlobalMessage).HasColumnName("id_global_message");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Message).HasColumnName("message");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GlobalMessages)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("global_message_created_by_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification).HasName("notification_pkey");

            entity.ToTable("notification");

            entity.HasIndex(e => e.UserId, "idx_notification_user");

            entity.Property(e => e.IdNotification).HasColumnName("id_notification");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_user_id_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.IdReport).HasName("report_pkey");

            entity.ToTable("report");

            entity.HasIndex(e => e.EventId, "idx_report_event");

            entity.HasIndex(e => e.Reason, "idx_report_reason");

            entity.Property(e => e.IdReport).HasColumnName("id_report");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.ReportedBy).HasColumnName("reported_by");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Event).WithMany(p => p.Reports)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("report_event_id_fkey");

            entity.HasOne(d => d.ReportedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReportedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("report_reported_by_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("report_status_fkey");
        });

        modelBuilder.Entity<ReportStatus>(entity =>
        {
            entity.HasKey(e => e.IdReportStatus).HasName("report_status_pkey");

            entity.ToTable("report_status");

            entity.HasIndex(e => e.Title, "report_status_title_key").IsUnique();

            entity.Property(e => e.IdReportStatus).HasColumnName("id_report_status");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.IdSubscriptionPlan).HasName("subscription_plan_pkey");

            entity.ToTable("subscription_plan");

            entity.Property(e => e.IdSubscriptionPlan).HasColumnName("id_subscription_plan");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Telemetry>(entity =>
        {
            entity.HasKey(e => e.IdTelemetry).HasName("telemetry_pkey");

            entity.ToTable("telemetry");

            entity.HasIndex(e => e.UserId, "idx_telemetry_user");

            entity.Property(e => e.IdTelemetry).HasColumnName("id_telemetry");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EventData)
                .HasColumnType("jsonb")
                .HasColumnName("event_data");
            entity.Property(e => e.EventType)
                .HasMaxLength(100)
                .HasColumnName("event_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Telemetries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("telemetry_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.IsBanned, "idx_user_banned");

            entity.HasIndex(e => e.Email, "idx_user_email");

            entity.HasIndex(e => e.Username, "idx_user_username");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "user_username_key").IsUnique();

            entity.HasIndex(e => e.Uuid, "user_uuid_key").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HashedPassword).HasColumnName("hashed_password");
            entity.Property(e => e.IsBanned)
                .HasDefaultValue(false)
                .HasColumnName("is_banned");
            entity.Property(e => e.RoleId)
                .HasDefaultValue(1)
                .HasColumnName("role_id");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Uuid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_id_fkey");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.IdUserSubscription).HasName("user_subscription_pkey");

            entity.ToTable("user_subscription");

            entity.HasIndex(e => e.UserId, "idx_user_subscription_user");

            entity.Property(e => e.IdUserSubscription).HasColumnName("id_user_subscription");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.SubscriptionPlanId).HasColumnName("subscription_plan_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_subscription_subscription_plan_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_subscription_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
