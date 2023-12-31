using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbKhachHang
{
    public Guid Id { get; set; }

    public string TenKhachHang { get; set; } = null!;

    public string SdtkhachHang { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public virtual ICollection<TbHoaDonBan> TbHoaDonBans { get; set; } = new List<TbHoaDonBan>();
}
