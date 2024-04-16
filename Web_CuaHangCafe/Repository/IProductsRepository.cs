using Web_CuaHangCafe.ViewModels;

namespace Web_CuaHangCafe.Repository
{
    public interface IProductsRepository
    {
        List<ProductViewModel> GetProducts();
        ProductViewModel GetProductById(int id);
        CreateProductViewModel CreateProduct(ProductViewModel product);
        void Update(ProductViewModel product);
        void Delete(int id);
    }
}
