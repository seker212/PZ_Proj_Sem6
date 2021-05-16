using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DatabaseAPI.DAL;

namespace DatabaseAPI.Services
{
    public class OrderServices : IOrderServices
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private static int ticketNumber = 0;
        private static int usingResource = 0;

        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

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
            return Task.Run(() =>
            {
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
            return Task.Run(() =>
            {
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

        public Task<ApiModels.OrderPost> PostOrder(ApiModels.OrderPost orderPost)
        {
            return Task.Run(() =>
            {
                var id = Guid.NewGuid();
                if(0 == Interlocked.Exchange(ref usingResource, 1))
                {
                    DatabaseModels.Order order = new DatabaseModels.Order(id, orderPost.CashierId, DatabaseModels.OrderStatus.Preparing, orderPost.CreatedAt, orderPost.Price, ticketNumber);
                    Interlocked.Exchange(ref usingResource, 0);
                }
                foreach (var product in orderPost.Products)
                {
                    double price = _productRepository.GetProductPrice(product.Id);
                    DatabaseModels.OrderItems orderItems = new DatabaseModels.OrderItems(id, product.Id, product.Count, price);
                }
                foreach(var discount in orderPost.Discounts)
                {
                    if (discount != null)
                    {

                    }
                }
                //DatabaseModels.Order order = new DatabaseModels.Order(Guid.NewGuid(), orderPost.CashierId, DatabaseModels.OrderStatus.Preparing, orderPost.CreatedAt, orderPost.Price, );
            });
        }
    }
}
