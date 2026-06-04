using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5pointswork
{
    public class AuditLogger
    {
        public void OnOrderStateChanged(object? sender, OrderEventArgs e) {
            Console.WriteLine($"Номер заказа {e.OrderID}, Статус заказа: {e.StageName}");
        }
    }
}
