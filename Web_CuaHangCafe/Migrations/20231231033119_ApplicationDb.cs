using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_CuaHangCafe.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbKhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDTKhachHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbKhachH__3214EC0779854367", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbNhomSanPham",
                columns: table => new
                {
                    MaNhomSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhomSP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbNhomSa__5A1AD2F985F055E7", x => x.MaNhomSP);
                });

            migrationBuilder.CreateTable(
                name: "tbPhanHoi",
                columns: table => new
                {
                    MaPhanHoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbQuanTriVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbTinTuc",
                columns: table => new
                {
                    MaTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgayDang = table.Column<DateOnly>(type: "date", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbTinTuc__B53648C018B441C9", x => x.MaTinTuc);
                });

            migrationBuilder.CreateTable(
                name: "tbHoaDonBan",
                columns: table => new
                {
                    MaHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoHoaDon = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NgayBan = table.Column<DateTime>(type: "datetime", nullable: true),
                    TongTien = table.Column<decimal>(type: "money", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbHoaDon__835ED13BE78BDB82", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK__tbHoaDonB__Custo__45F365D3",
                        column: x => x.CustomerId,
                        principalTable: "tbKhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbSanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GiaBan = table.Column<decimal>(type: "money", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNhomSP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbSanPha__FAC7442DA6D9D8DC", x => x.MaSanPham);
                    table.ForeignKey(
                        name: "FK__tbSanPham__MaNho__46E78A0C",
                        column: x => x.MaNhomSP,
                        principalTable: "tbNhomSanPham",
                        principalColumn: "MaNhomSP");
                });

            migrationBuilder.CreateTable(
                name: "tbChiTietHoaDonBan",
                columns: table => new
                {
                    MaHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "money", nullable: true),
                    GiamGia = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    ThanhTien = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbChiTie__52F2A93ED9FFE6AD", x => new { x.MaSanPham, x.MaHoaDon });
                    table.ForeignKey(
                        name: "FK__tbChiTiet__MaHoa__440B1D61",
                        column: x => x.MaHoaDon,
                        principalTable: "tbHoaDonBan",
                        principalColumn: "MaHoaDon");
                    table.ForeignKey(
                        name: "FK__tbChiTiet__MaSan__44FF419A",
                        column: x => x.MaSanPham,
                        principalTable: "tbSanPham",
                        principalColumn: "MaSanPham");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietHoaDonBan_MaHoaDon",
                table: "tbChiTietHoaDonBan",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_tbHoaDonBan_CustomerId",
                table: "tbHoaDonBan",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "UQ__tbHoaDon__012E9E53087434A1",
                table: "tbHoaDonBan",
                column: "SoHoaDon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbSanPham_MaNhomSP",
                table: "tbSanPham",
                column: "MaNhomSP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbChiTietHoaDonBan");

            migrationBuilder.DropTable(
                name: "tbPhanHoi");

            migrationBuilder.DropTable(
                name: "tbQuanTriVien");

            migrationBuilder.DropTable(
                name: "tbTinTuc");

            migrationBuilder.DropTable(
                name: "tbHoaDonBan");

            migrationBuilder.DropTable(
                name: "tbSanPham");

            migrationBuilder.DropTable(
                name: "tbKhachHang");

            migrationBuilder.DropTable(
                name: "tbNhomSanPham");
        }
    }
}
