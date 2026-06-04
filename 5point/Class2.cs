using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _5pointswork
{
    public class OrderValidator
    {
        public bool Validate(Order order) {
            PropertyInfo[] types = order.GetType().GetProperties();
            foreach (var type1 in types)
            {
                if (type1.GetCustomAttribute<RequiredAttribute>() != null)
                {
                    var value = type1.GetValue(order);
                    if (value == null|| (value is string s && string.IsNullOrEmpty(s)))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        
    }
}
