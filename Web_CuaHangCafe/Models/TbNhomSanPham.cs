using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbNhomSanPham
{
    public string MaNhomSp { get; set; } = null!;

    public string TenNhomSp { get; set; } = null!;

    public virtual ICollection<TbSanPham> TbSanPhams { get; set; } = new List<TbSanPham>();
}
