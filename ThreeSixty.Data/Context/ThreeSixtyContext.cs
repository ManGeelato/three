using Microsoft.EntityFrameworkCore;

namespace ThreeSixty.Data.Context
{
    public class ThreeSixtyContext : DbContext, IThreeSixtyContext
    {
        public ThreeSixtyContext()
        {
        }

        public ThreeSixtyContext(DbContextOptions<ThreeSixtyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ActivityAuditTrail> ActivityAuditTrails { get; set; } = null!;
        public virtual DbSet<ApplicationError> ApplicationErrors { get; set; } = null!;
        public virtual DbSet<Entity> Entities { get; set; } = null!;
        public virtual DbSet<Incident> Incidents { get; set; } = null!;
        public virtual DbSet<IncidentHistory> IncidentHistories { get; set; } = null!;
        public virtual DbSet<IncidentStatus> IncidentStatuses { get; set; } = null!;
        public virtual DbSet<IncidentStatusReason> IncidentStatusReasons { get; set; } = null!;
        public virtual DbSet<IncidentType> IncidentTypes { get; set; } = null!;
        public virtual DbSet<Suburb> Suburbs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NEV106A;Database=ThreeSixty;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ActivityAuditTrail>(entity =>
            {
                entity.ToTable("ActivityAuditTrail", "adt");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details).HasMaxLength(4000);

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityAuditTrails)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityAuditTrail_Activity");
            });

            modelBuilder.Entity<ApplicationError>(entity =>
            {
                entity.ToTable("ApplicationError", "adt");

                entity.Property(e => e.Environment).HasMaxLength(1000);

                entity.Property(e => e.LogLevel).HasMaxLength(50);

                entity.Property(e => e.Logger).HasMaxLength(500);

                entity.Property(e => e.MachineName).HasMaxLength(100);

                entity.Property(e => e.Thread).HasMaxLength(1000);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable("Entity", "trn");

                entity.Property(e => e.Address).HasMaxLength(4000);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FifthName).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FourthName).HasMaxLength(50);

                entity.Property(e => e.IdentityNumber).HasMaxLength(50);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastNane).HasMaxLength(50);

                entity.Property(e => e.MobilePhoneNumber).HasMaxLength(15);

                entity.Property(e => e.RegistrationName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SecondName).HasMaxLength(50);

                entity.Property(e => e.ThirdName).HasMaxLength(50);

                entity.Property(e => e.WorkPhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("Incident", "trn");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IncidentDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LocationDescription).HasMaxLength(2000);

                entity.Property(e => e.LocationXcoordinate)
                    .HasColumnType("decimal(18, 18)")
                    .HasColumnName("LocationXCoordinate");

                entity.Property(e => e.LocationYcoordinate)
                    .HasColumnType("decimal(18, 18)")
                    .HasColumnName("LocationYCoordinate");

                entity.Property(e => e.LongDescription).HasMaxLength(4000);

                entity.Property(e => e.ShortDescription).HasMaxLength(200);

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incident_Entity");

                entity.HasOne(d => d.IncidentStatus)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.IncidentStatusId)
                    .HasConstraintName("FK_Incident_IncidentStatus");

                entity.HasOne(d => d.IncidentStatusReason)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.IncidentStatusReasonId)
                    .HasConstraintName("FK_Incident_IncidentStatusReason");

                entity.HasOne(d => d.IncidentType)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.IncidentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incident_IncidentType");
            });

            modelBuilder.Entity<IncidentHistory>(entity =>
            {
                entity.ToTable("IncidentHistory", "trn");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(4000);

                entity.HasOne(d => d.Incident)
                    .WithMany(p => p.IncidentHistories)
                    .HasForeignKey(d => d.IncidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentHistory_IncidentId");

                entity.HasOne(d => d.IncidentStatus)
                    .WithMany(p => p.IncidentHistories)
                    .HasForeignKey(d => d.IncidentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentHistory_IncidentStatusId");

                entity.HasOne(d => d.IncidentStatusReason)
                    .WithMany(p => p.IncidentHistories)
                    .HasForeignKey(d => d.IncidentStatusReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentHistory_IncidentStatusReason");
            });

            modelBuilder.Entity<IncidentStatus>(entity =>
            {
                entity.ToTable("IncidentStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IncidentStatusReason>(entity =>
            {
                entity.ToTable("IncidentStatusReason", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IncidentType>(entity =>
            {
                entity.ToTable("IncidentType", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Suburb>(entity =>
            {
                entity.ToTable("Suburb", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

        }
    }
}
