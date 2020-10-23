using ShopApi.DomainModels;

namespace ShopApi.ApiModels
{
    public static class MappingExtensions
    {
        public static void MapTo(this ProductModel model, Product product)
        {
            product.Name = model.Name;
            product.Category = model.Category;
            product.Price = model.Price;
        }

        public static ProductModel MapToDto(this Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };
        }

        public static void MapTo(this OrderModel model, Order order)
        {
            order.CustomerName = model.CustomerName;
            order.CustomerEmail = model.CustomerEmail;
            order.CustomerAddress = model.CustomerAddress;
        }

        public static OrderModel MapToDto(this Order order)
        {
            return new OrderModel
            {
                Id = order.Id,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName
            };
        }

        public static void MapTo(this OrderDetailModel model, OrderDetail row)
        {
            row.Quantity = model.Quantity;
            row.SingleProductPrice = model.SingleProductPrice;
            row.ProductId = model.ProductId;
        }

        public static OrderDetailModel MapToDto(this OrderDetail detail)
        {
            return new OrderDetailModel
            {
                Id = detail.Id,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                SingleProductPrice = detail.SingleProductPrice
            };
        }
    }
}
