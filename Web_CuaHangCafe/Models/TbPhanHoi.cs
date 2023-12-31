using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbPhanHoi
{
    public int MaPhanHoi { get; set; }

    public string TieuDe { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? NoiDung { get; set; }
}
