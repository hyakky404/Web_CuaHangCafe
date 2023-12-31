using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbTinTuc
{
    public int MaTinTuc { get; set; }

    public string TieuDe { get; set; } = null!;

    public DateOnly? NgayDang { get; set; }

    public string? NoiDung { get; set; }

    public string? HinhAnh { get; set; }
}
