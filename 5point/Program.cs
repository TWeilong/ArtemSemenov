using System.ComponentModel.DataAnnotations;

namespace _5pointswork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderProcessor processor = new OrderProcessor();
            AuditLogger logger = new AuditLogger();
            processor.OrderStateChanged += logger.OnOrderStateChanged;
            OrderValidator validator = new OrderValidator();
            var orders = new List<Order>
            { 
                new Order { ID = 1, UserEmail = "test@mail.ru", TotalAmount = 1500, Status = "Оплачен" },
                new Order { ID = 2, UserEmail = "", TotalAmount = 500, Status = "Оплачен" },
                new Order { ID = 3, UserEmail = "test2@mail.ru", TotalAmount = 300, Status = "Не оплачен" },
                new Order { ID = 4, UserEmail = "user@макс.ru", TotalAmount = 800, Status = "Оплачен" },
                new Order { ID = 5, UserEmail = null, TotalAmount = 600, Status = "Оплачен" },
            };

            processor.ProcessBatch(orders).Wait();

        }

    }
}
