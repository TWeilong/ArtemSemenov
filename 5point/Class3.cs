using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _5pointswork
{
    public class OrderEventArgs:EventArgs
    {
        public int OrderID { get; set; }
        public string? StageName { get; set; }

        
        
    }
    public class OrderProcessor {
        public event EventHandler<OrderEventArgs>? OrderStateChanged;
        public void FireEvent(int orderid, string stagename) {
            var args = new OrderEventArgs { OrderID = orderid, StageName = stagename };
            OrderStateChanged?.Invoke(this, args);
        }
        public void ApplyDiscounts(Order order) {
            Assembly assemblys = Assembly.GetExecutingAssembly();
            var types = assemblys.GetTypes().Where(t => typeof(IDiscountRule).IsAssignableFrom(t) && !t.IsInterface);
            order.FinalAmount=order.TotalAmount;
            foreach (var t in types)
            {
                var activ = Activator.CreateInstance(t) as IDiscountRule;
                if (activ != null) { 
                    order.FinalAmount=order.TotalAmount-activ.Discount(order);
                }
            } 
        }

        public async Task ProcessBatch(List<Order> orders)
        {
            var semaphore = new SemaphoreSlim(5);

            var tasks = orders.Select(async order =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var validator = new OrderValidator();
                    bool isValid = validator.Validate(order);
                    if (!isValid)
                    {
                        Console.WriteLine($"Заказ #{order.ID} отклонён — обязательные поля не заполнены");
                        return;
                    }

                    FireEvent(order.ID, "Проверка пройдена");
                    ApplyDiscounts(order);

                    if (order.FinalAmount < order.TotalAmount)
                        Console.WriteLine($"Заказ #{order.ID} | Цена: {order.TotalAmount} | Скидка! Итого: {order.FinalAmount}");
                    else
                        Console.WriteLine($"Заказ #{order.ID} | Цена: {order.TotalAmount} | Скидок нет");

                    await SendToDeliveryAsync(order);
                    FireEvent(order.ID, "Заказ принят");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка заказа #{order.ID}: {ex.Message}");
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }


        public async Task SendToDeliveryAsync(Order order) {
            await Task.Delay(500);
            if (order.Status != "Оплачен") {
                throw new InvalidOperationException("Заказ НЕ ОПЛАЧЕН!!!");
            }
            Random random = new Random();
            var opa = random.Next(1,101);
            if (opa == 1) {
                throw new InvalidOperationException("Неизвестная ошибка :{");
            }

        }
    }
}
