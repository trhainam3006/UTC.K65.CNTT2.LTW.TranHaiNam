using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _241230802_TranHaiNam_2026_De1.Models;

public partial class QLTaiSanContext : DbContext
{
    public QLTaiSanContext(DbContextOptions<QLTaiSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiTaiSan> LoaiTaiSan { get; set; }

    public virtual DbSet<TaiSan> TaiSan { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoaiTaiSan>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<TaiSan>(entity =>
        {
            entity.HasKey(e => e.MaTS);

            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.NguyenGia).HasColumnType("numeric(18,0)");
            entity.Property(e => e.TenTS).HasMaxLength(100);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.TaiSan)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiSan_LoaiTaiSan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
