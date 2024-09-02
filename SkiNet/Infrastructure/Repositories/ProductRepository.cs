using Microsoft.EntityFrameworkCore;
using SkiNet.Domain.Entities;
using SkiNet.Domain.Interfaces;
using SkiNet.Infrastructure.Data;

namespace SkiNet.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _storeContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductsBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductsTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }
    }
}
