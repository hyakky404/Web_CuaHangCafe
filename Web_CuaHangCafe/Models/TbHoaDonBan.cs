using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbHoaDonBan
{
    public string MaHoaDon { get; set; } = null!;

    public DateTime? NgayBan { get; set; }

    public decimal? TongTien { get; set; }

    public string SdtkhachHang { get; set; } = null!;

    public virtual TbKhachHang SdtkhachHangNavigation { get; set; } = null!;

    public virtual ICollection<TbChiTietHoaDonBan> TbChiTietHoaDonBans { get; set; } = new List<TbChiTietHoaDonBan>();
}
