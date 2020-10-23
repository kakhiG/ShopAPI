using System.ComponentModel.DataAnnotations;

namespace ShopApi.ApiModels
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal SingleProductPrice { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
