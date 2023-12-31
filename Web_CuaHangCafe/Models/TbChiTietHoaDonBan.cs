using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbChiTietHoaDonBan
{
    public Guid MaHoaDon { get; set; }

    public int MaSanPham { get; set; }

    public decimal? GiaBan { get; set; }

    public int? GiamGia { get; set; }

    public int? SoLuong { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual TbHoaDonBan MaHoaDonNavigation { get; set; } = null!;

    public virtual TbSanPham MaSanPhamNavigation { get; set; } = null!;
}
