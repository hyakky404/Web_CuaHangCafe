using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbQuanTriVien
{
    public string TenNguoiDung { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public virtual ICollection<TbTinTuc> TbTinTucs { get; set; } = new List<TbTinTuc>();
}
