using SkiNet.Domain.Entities;

namespace SkiNet.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetAllProductsBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetAllProductsTypesAsync();
        Task<Product> GetProductByIdAsync(int id);
    }
}
