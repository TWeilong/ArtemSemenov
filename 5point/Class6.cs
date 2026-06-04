using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5pointswork
{
    public class DiscountRules
    {
        public class MaxDiscount:IDiscountRule {
            public decimal Discount(Order order) {
                if (order.UserEmail?.EndsWith("макс.ru")==true) {
                    return order.TotalAmount * 0.9m;
                }
                return 0;
            }
        }
        public class SumDiscount:IDiscountRule
        {
            public decimal Discount(Order order)
            {
                if (order.TotalAmount>1000) {
                    return order.TotalAmount * 0.0067m;
                }
                return 0;
            }
        }
    }
}
