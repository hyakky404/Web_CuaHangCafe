using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web_CuaHangCafe.Models;

public partial class QlcuaHangCafeContext : DbContext
{
    public QlcuaHangCafeContext()
    {
    }

    public QlcuaHangCafeContext(DbContextOptions<QlcuaHangCafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbChiTietHoaDonBan> TbChiTietHoaDonBans { get; set; }

    public virtual DbSet<TbHoaDonBan> TbHoaDonBans { get; set; }

    public virtual DbSet<TbKhachHang> TbKhachHangs { get; set; }

    public virtual DbSet<TbNhomSanPham> TbNhomSanPhams { get; set; }

    public virtual DbSet<TbPhanHoi> TbPhanHois { get; set; }

    public virtual DbSet<TbQuanTriVien> TbQuanTriViens { get; set; }

    public virtual DbSet<TbSanPham> TbSanPhams { get; set; }

    public virtual DbSet<TbTinTuc> TbTinTucs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VS99KMT\\SQLEXPRESS;Initial Catalog=QLCuaHangCafe;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbChiTietHoaDonBan>(entity =>
        {
            entity.HasKey(e => new { e.MaSanPham, e.MaHoaDon }).HasName("PK__tbChiTie__52F2A93E949DEAA1");

            entity.ToTable("tbChiTietHoaDonBan");

            entity.Property(e => e.MaSanPham).HasMaxLength(6);
            entity.Property(e => e.MaHoaDon).HasMaxLength(14);
            entity.Property(e => e.GiaBan).HasColumnType("money");
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.TbChiTietHoaDonBans)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbChiTiet__MaHoa__44FF419A");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.TbChiTietHoaDonBans)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbChiTiet__MaSan__45F365D3");
        });

        modelBuilder.Entity<TbHoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__tbHoaDon__835ED13B9462EC93");

            entity.ToTable("tbHoaDonBan");

            entity.Property(e => e.MaHoaDon).HasMaxLength(14);
            entity.Property(e => e.NgayBan).HasColumnType("date");
            entity.Property(e => e.SdtkhachHang)
                .HasMaxLength(10)
                .HasColumnName("SDTKhachHang");
            entity.Property(e => e.TongTien).HasColumnType("money");

            entity.HasOne(d => d.SdtkhachHangNavigation).WithMany(p => p.TbHoaDonBans)
                .HasForeignKey(d => d.SdtkhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbHoaDonB__SDTKh__46E78A0C");
        });

        modelBuilder.Entity<TbKhachHang>(entity =>
        {
            entity.HasKey(e => e.SdtkhachHang).HasName("PK__tbKhachH__C87280B1B2C3E773");

            entity.ToTable("tbKhachHang");

            entity.Property(e => e.SdtkhachHang)
                .HasMaxLength(10)
                .HasColumnName("SDTKhachHang");
            entity.Property(e => e.TenKhachHang).HasMaxLength(50);
        });

        modelBuilder.Entity<TbNhomSanPham>(entity =>
        {
            entity.HasKey(e => e.MaNhomSp).HasName("PK__tbNhomSa__5A1AD2F9C35C0FAF");

            entity.ToTable("tbNhomSanPham");

            entity.Property(e => e.MaNhomSp)
                .HasMaxLength(2)
                .HasColumnName("MaNhomSP");
            entity.Property(e => e.TenNhomSp)
                .HasMaxLength(255)
                .HasColumnName("TenNhomSP");
        });

        modelBuilder.Entity<TbPhanHoi>(entity =>
        {
            entity.HasKey(e => e.MaPhanHoi).HasName("PK__tbPhanHo__3458D20F0A138233");

            entity.ToTable("tbPhanHoi");

            entity.Property(e => e.MaPhanHoi).HasMaxLength(6);
            entity.Property(e => e.SoDienThoai).HasMaxLength(10);
            entity.Property(e => e.TieuDe).HasMaxLength(50);
        });

        modelBuilder.Entity<TbQuanTriVien>(entity =>
        {
            entity.HasKey(e => e.TenNguoiDung).HasName("PK__tbQuanTr__57E5A81C0A010FED");

            entity.ToTable("tbQuanTriVien");

            entity.Property(e => e.TenNguoiDung).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
        });

        modelBuilder.Entity<TbSanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__tbSanPha__FAC7442D2ED8D280");

            entity.ToTable("tbSanPham");

            entity.Property(e => e.MaSanPham).HasMaxLength(6);
            entity.Property(e => e.GiaBan).HasColumnType("money");
            entity.Property(e => e.MaNhomSp)
                .HasMaxLength(2)
                .HasColumnName("MaNhomSP");
            entity.Property(e => e.TenSanPham).HasMaxLength(255);

            entity.HasOne(d => d.MaNhomSpNavigation).WithMany(p => p.TbSanPhams)
                .HasForeignKey(d => d.MaNhomSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbSanPham__MaNho__47DBAE45");
        });

        modelBuilder.Entity<TbTinTuc>(entity =>
        {
            entity.HasKey(e => e.MaTinTuc).HasName("PK__tbTinTuc__B53648C0378B3A22");

            entity.ToTable("tbTinTuc");

            entity.Property(e => e.MaTinTuc).HasMaxLength(10);
            entity.Property(e => e.NgayDang).HasColumnType("date");
            entity.Property(e => e.TenNguoiDung).HasMaxLength(50);
            entity.Property(e => e.TieuDe).HasMaxLength(255);

            entity.HasOne(d => d.TenNguoiDungNavigation).WithMany(p => p.TbTinTucs)
                .HasForeignKey(d => d.TenNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbTinTuc__TenNgu__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
