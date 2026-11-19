using Microsoft.EntityFrameworkCore;
using TranHaiNam_241230802.Models;

namespace TranHaiNam_241230802.Data;

public partial class QLNhaSachContext : DbContext
{
    public QLNhaSachContext(DbContextOptions<QLNhaSachContext> options)
        : base(options) { }

    public virtual DbSet<Sach> Sach { get; set; }
    public virtual DbSet<TheLoai> TheLoai { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.ToTable("TheLoai");
            entity.HasKey(e => e.MaTL).HasName("PK_TheLoai");
            entity.Property(e => e.TenTL).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.ToTable("Sach");
            entity.HasKey(e => e.MaSach).HasName("PK_Sach");
            entity.Property(e => e.TenSach).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Gia).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.HasOne(e => e.MaTLNavigation)
                .WithMany(e => e.Sach)
                .HasForeignKey(e => e.MaTL)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sach_TheLoai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
