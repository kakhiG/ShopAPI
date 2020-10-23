using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.DomainModels
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderDetail> Details { get; set; }

        public Order()
        {
        }
    }
}


