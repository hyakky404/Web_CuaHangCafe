using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.ViewModels;

namespace Web_CuaHangCafe.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductViewModel> GetProducts()
        {
            var products = _context.TbSanPhams.Select(p => new ProductViewModel
            {
                MaSanPham = p.MaSanPham,
                TenSanPham = p.TenSanPham,
                GiaBan = p.GiaBan,
                MoTa = p.MoTa,
                HinhAnh = p.HinhAnh,
                GhiChu = p.GhiChu,
                LoaiSanPham = p.MaNhomSp.ToString(),
            });

            return products.ToList();
        }

        public CreateProductViewModel CreateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(ProductViewModel product)
        {
            throw new NotImplementedException();
        }
    }
}
