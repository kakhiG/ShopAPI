using Microsoft.EntityFrameworkCore;
using ShopApi.ApiModels;
using ShopApi.DomainModels;
using ShopAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDataContext _db;

        public OrderRepository(ShopDataContext db)
        {
            _db = db;
        }

        public async Task<OrderDetailModel> AddDetailToOrder(int id, OrderDetailModel model)
        {
            if (await OrderExists(id) == false)
            {
                throw new EntityNotFoundException<OrderModel>(id);
            }

            var detail  = new OrderDetail();
            model.MapTo(detail);

            detail.OrderId = id;
            await _db.OrderDetails.AddAsync(detail);
            await _db.SaveChangesAsync();

            return detail.MapToDto();
        }

        public async Task<OrderModel> CreateOrder(OrderModel model)
        {
            var order = new Order();
            model.MapTo(order);

            order.CreatedAt = DateTimeOffset.Now;

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order.MapToDto();
        }

        public async Task DeleteOrder(int id)
        {
            Order order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteDetailFromOrder(int orderId, int detailId)
        {
            if (await OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<OrderModel>(orderId);
            }

            OrderDetail detail  = await _db.OrderDetails.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == detailId);
            if (detail != null)
            {
                _db.OrderDetails.Remove(detail);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            return (await _db.Orders.AsNoTracking().ToListAsync())
                .Select(o => o.MapToDto())
                .ToList();
        }

        public async Task<OrderModel> GetOrder(int id)
        {
            return (await _db.Orders.FirstOrDefaultAsync(o => o.Id == id))?.MapToDto();
        }

        public async Task<OrderDetailModel> GetDetailInOrder(int orderId, int detailId)
        {
            return (await _db.OrderDetails.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == detailId))?.MapToDto();
        }

        public async Task<List<OrderDetailModel>> GetDetailForOrder(int id)
        {
            
            if (await OrderExists(id) == false)
            {
                return null;
            }

            return (await _db.OrderDetails.AsNoTracking()
                .Where(r => r.OrderId == id)
                .ToListAsync())
                    .Select(r => r.MapToDto())
                    .ToList();
        }

        public async Task UpdateOrder(int id, OrderModel updatedOrder)
        {
            Order order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new EntityNotFoundException<OrderModel>(id);
            }

            updatedOrder.MapTo(order);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateDetailInOrder(int orderId, int detailId, OrderDetailModel updatedetail)
        {
            if (await OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<OrderModel>(orderId);
            }

            OrderDetail row = await _db.OrderDetails.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == detailId);
            if (row == null)
            {
                throw new EntityNotFoundException<OrderDetailModel>(detailId);
            }

            updatedetail.MapTo(row);
            await _db.SaveChangesAsync();
        }

        private async Task<bool> OrderExists(int orderId)
        {
            return await _db.Orders.AnyAsync(o => o.Id == orderId);
        }
    }
}

