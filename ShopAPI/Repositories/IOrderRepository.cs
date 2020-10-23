using System.Collections.Generic;
using System.Threading.Tasks;
using ShopApi.ApiModels;

namespace ShopApi.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrder(int id);
        Task<OrderModel> CreateOrder(OrderModel order);
        Task UpdateOrder(int id, OrderModel updatedOrder);
        Task DeleteOrder(int id);
        Task<List<OrderDetailModel>> GetDetailForOrder(int id);
        Task<OrderDetailModel> AddDetailToOrder(int id, OrderDetailModel detail);
        Task<OrderDetailModel> GetDetailInOrder(int orderId, int detailId);
        Task UpdateDetailInOrder(int orderId, int detailId, OrderDetailModel updatedetail);
        Task DeleteDetailFromOrder(int orderId, int detailId);
    }
}

