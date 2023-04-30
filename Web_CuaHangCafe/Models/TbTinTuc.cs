using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbTinTuc
{
    public string MaTinTuc { get; set; } = null!;

    public string TieuDe { get; set; } = null!;

    public DateTime? NgayDang { get; set; }

    public string? NoiDung { get; set; }

    public string? HinhAnh { get; set; }

    public string TenNguoiDung { get; set; } = null!;

    public virtual TbQuanTriVien TenNguoiDungNavigation { get; set; } = null!;
}
