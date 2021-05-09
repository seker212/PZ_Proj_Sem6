using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DAL;

namespace DatabaseAPI.Services
{
    public class OrderServices
    {
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        public Task<bool> UpdateStatus(Guid key, DatabaseModels.OrderStatus newStatus)
        {
            if (key == Guid.Empty)
                throw new Exception();
            return Task.Run(() =>
            {
                return _orderRepository.UpdateStatus(key, newStatus);
            });
        }

        public Task<IEnumerable<ApiModels.OrderProducts>> GetKitchenOrders()
        {
            return Task.Run(() => { 
                var orders = _orderRepository.GetKitchenOrders();
                var result = new List<ApiModels.OrderProducts>();
                foreach (var order in orders)
                {
                    var products = new List<ApiModels.Product>();
                    var orderItems = _productRepository.GetOrderProducts(order);
                    foreach (var item in orderItems)
                    {
                        products.Add(new ApiModels.Product(item.id, item.name, item.quantity));
                    }
                    result.Add(new ApiModels.OrderProducts(order.Id, products, order.TicketNumber));
                }
                return result.AsEnumerable();
            });
        }
        public Task<IEnumerable<ApiModels.OrderProducts>> GetServiceOrders()
        {
            return Task.Run(() => {
                var orders = _orderRepository.GetServiceOrders();
                var result = new List<ApiModels.OrderProducts>();
                foreach (var order in orders)
                {
                    var products = new List<ApiModels.Product>();
                    var orderItems = _productRepository.GetOrderProducts(order);
                    foreach (var item in orderItems)
                    {
                        products.Add(new ApiModels.Product(item.id, item.name, item.quantity));
                    }
                    result.Add(new ApiModels.OrderProducts(order.Id, products, order.TicketNumber));
                }
                return result.AsEnumerable();
            });
        }
    }
}
