using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Models
{
    public class CellPhone
    {
        [Key]
        public int CellPhoneId { get; set; }
        public string CellPhoneName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}
