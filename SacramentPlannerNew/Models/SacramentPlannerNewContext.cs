using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SacramentPlannerNew.Models
{
    public partial class SacramentPlannerNewContext : DbContext
    {
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<Speakers> Speakers { get; set; }

        public SacramentPlannerNewContext(DbContextOptions<SacramentPlannerNewContext> options)
    : base(options)
{ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.MeetingId).HasColumnName("Meeting_id");

                entity.Property(e => e.ClosingHymn).HasColumnName("Closing_hymn");

                entity.Property(e => e.ClosingPrayer)
                    .IsRequired()
                    .HasColumnName("Closing_Prayer")
                    .HasMaxLength(50);

                entity.Property(e => e.Conductor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IntermediateHymn).HasColumnName("Intermediate_Hymn");

                entity.Property(e => e.OpeningHymn).HasColumnName("Opening_Hymn");

                entity.Property(e => e.OpeningPrayer)
                    .IsRequired()
                    .HasColumnName("Opening_Prayer")
                    .HasMaxLength(50);

                entity.Property(e => e.SacramentHymn).HasColumnName("Sacrament_Hymn");
            });

            modelBuilder.Entity<Speakers>(entity =>
            {
                entity.HasKey(e => e.SpeakerId);

                entity.Property(e => e.SpeakerId).HasColumnName("Speaker_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.MeetingId).HasColumnName("Meeting_id");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.Speakers)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Speakers_Meeting");
            });
        }
    }
}
