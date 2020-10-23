using System.ComponentModel.DataAnnotations;

namespace ShopApi.ApiModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
    }
}
