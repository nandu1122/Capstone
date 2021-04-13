using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public virtual Product Item { get; set; }
        public virtual Order Order { get; set; }
    }
}

