using Microsoft.EntityFrameworkCore;
using ShopApi.ApiModels;
using ShopApi.DomainModels;
using ShopAPI.Data;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDataContext _db;

        public ProductRepository(ShopDataContext db)
        {
            _db = db;
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var product = new Product();
            model.MapTo(product);
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product.MapToDto();
        }

        public async Task DeleteProduct(int id)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return (await _db.Products.AsNoTracking().ToListAsync())
                .Select(p => p.MapToDto())
                .ToList();
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            return (await _db.Products.FirstOrDefaultAsync(p => p.Id == id))?.MapToDto();
        }

        public async Task UpdateProduct(int id, ProductModel updatedProduct)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new EntityNotFoundException<ProductModel>(id);
            }

            updatedProduct.MapTo(product);

            await _db.SaveChangesAsync();
        }
    }
}
