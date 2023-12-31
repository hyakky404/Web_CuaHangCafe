using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbSanPham
{
    public int MaSanPham { get; set; }

    public string TenSanPham { get; set; } = null!;

    public decimal? GiaBan { get; set; }

    public string? MoTa { get; set; }

    public string? HinhAnh { get; set; }

    public string? GhiChu { get; set; }

    public int MaNhomSp { get; set; }

    public virtual TbNhomSanPham MaNhomSpNavigation { get; set; } = null!;

    public virtual ICollection<TbChiTietHoaDonBan> TbChiTietHoaDonBans { get; set; } = new List<TbChiTietHoaDonBan>();
}
