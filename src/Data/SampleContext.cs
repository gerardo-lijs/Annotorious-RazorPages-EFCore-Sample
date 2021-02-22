using Microsoft.EntityFrameworkCore;
using Annotorious_RazorPages_EFCore_Sample.Data.Models;

#nullable disable

namespace Annotorious_RazorPages_EFCore_Sample.Data
{
    public partial class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }

        public virtual DbSet<Panorama> Panoramas { get; set; }
        public virtual DbSet<PanoramaAnnotation> PanoramaAnnotations { get; set; }
        public virtual DbSet<PanoramaAnnotationItem> PanoramaAnnotationItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Panorama>(entity =>
            {
                entity.ToTable("Panorama");

                entity.Property(e => e.PanoramaId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PanoramaAnnotation>(entity =>
            {
                entity.HasKey(e => new { e.PanoramaId, e.AnnotationId });

                entity.ToTable("PanoramaAnnotation");

                entity.HasOne(d => d.Panorama)
                    .WithMany(p => p.PanoramaAnnotations)
                    .HasForeignKey(d => d.PanoramaId)
                    .HasConstraintName("FK_PanoramaAnnotation_Panorama");
            });

            modelBuilder.Entity<PanoramaAnnotationItem>(entity =>
            {
                entity.HasKey(e => new { e.PanoramaId, e.AnnotationId, e.ItemId })
                    .HasName("PK_PanoramaAnnotationItem_1");

                entity.ToTable("PanoramaAnnotationItem");

                entity.Property(e => e.ItemId).ValueGeneratedOnAdd();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.PanoramaAnnotationItems)
                    .HasForeignKey(d => d.CreatedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PanoramaAnnotationItem_User");

                entity.HasOne(d => d.PanoramaAnnotation)
                    .WithMany(p => p.PanoramaAnnotationItems)
                    .HasForeignKey(d => new { d.PanoramaId, d.AnnotationId })
                    .HasConstraintName("FK_PanoramaAnnotationItem_PanoramaAnnotation");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.DisplayName).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserPrincipalName).HasMaxLength(256);
            });
        }
    }
}
