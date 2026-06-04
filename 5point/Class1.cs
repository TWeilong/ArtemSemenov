using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5pointswork
{
    public class Order
    {
        public int ID{ get; set; }
        [Required]
        public string? UserEmail{get;set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public decimal FinalAmount { get; set; }
    }

    public class RequiredAttribute : Attribute {}
}
