using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _241230802_TranHaiNam_2023.Models;

public partial class QLHangHoaContext : DbContext
{
    public QLHangHoaContext(DbContextOptions<QLHangHoaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HangHoa> HangHoa { get; set; }

    public virtual DbSet<LoaiHang> LoaiHang { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHang);

            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.Gia).HasColumnType("numeric(18,0)");
            entity.Property(e => e.TenHang).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.HangHoa)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HangHoa_LoaiHang");
        });

        modelBuilder.Entity<LoaiHang>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
