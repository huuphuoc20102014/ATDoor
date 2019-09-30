using System;
using AtDoor.Efs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AtDoor.Efs.Context
{
    public partial class AtDoorContext : DbContext
    {
        public AtDoorContext()
        {
        }

        public AtDoorContext(DbContextOptions<AtDoorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<CardDoor> CardDoor { get; set; }
        public virtual DbSet<Door> Door { get; set; }
        public virtual DbSet<HistoryCard> HistoryCard { get; set; }
        public virtual DbSet<HistoryDoor> HistoryDoor { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=115.78.100.42,8899;Database=AT_DOOR;User Id=sa;Password=1@qweQAZ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkUserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RowStatus)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("FK_CardUsers");
            });

            modelBuilder.Entity<CardDoor>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkCardId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkDoorId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RowStatus)
                    .IsRequired()
                    .IsRowVersion();
            });

            modelBuilder.Entity<Door>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RowStatus)
                    .IsRequired()
                    .IsRowVersion();
            });

            modelBuilder.Entity<HistoryCard>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkCardId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkUserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowStatus)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkCard)
                    .WithMany(p => p.HistoryCard)
                    .HasForeignKey(d => d.FkCardId)
                    .HasConstraintName("FK_HistoryCard_Card");

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.HistoryCard)
                    .HasForeignKey(d => d.FkUserId)
                    .HasConstraintName("FK_HistoryCard_Users");
            });

            modelBuilder.Entity<HistoryDoor>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkCardId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkDoorId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowStatus)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkCard)
                    .WithMany(p => p.HistoryDoor)
                    .HasForeignKey(d => d.FkCardId)
                    .HasConstraintName("FK_HistoryDoor_Card");

                entity.HasOne(d => d.FkDoor)
                    .WithMany(p => p.HistoryDoor)
                    .HasForeignKey(d => d.FkDoorId)
                    .HasConstraintName("FK_HistoryDoor_Door");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkCardId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.RowStatus)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkCard)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FkCardId)
                    .HasConstraintName("FK_UsersCard");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<AtDoor.Efs.Entities.Users> User { get; set; }
    }
}
