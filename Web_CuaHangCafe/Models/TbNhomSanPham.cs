using System;
using System.Collections.Generic;

namespace Web_CuaHangCafe.Models;

public partial class TbNhomSanPham
{
    public int MaNhomSp { get; set; }

    public string TenNhomSp { get; set; } = null!;

    public virtual ICollection<TbSanPham> TbSanPhams { get; set; } = new List<TbSanPham>();
}
