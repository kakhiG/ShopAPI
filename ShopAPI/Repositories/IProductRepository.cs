using System.Collections.Generic;
using System.Threading.Tasks;
using ShopApi.ApiModels;


namespace ShopApi.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProduct(int id);
        Task<ProductModel> CreateProduct(ProductModel product);
        Task UpdateProduct(int id, ProductModel product);
        Task DeleteProduct(int id);
    }
}