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
        private IDiscountRepository _discountRepository;
        private IPairRepository<DatabaseModels.OrderItems> _orderItemsRepository;
        private IOrderDiscountRepository _orderDiscountRepository;
        private IObjectRepository<DatabaseModels.Cashier> _cashierRepository;
        private static int ticketNumber = 1;
        private readonly object ticketNumberLock;

        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository, IDiscountRepository discountRepository, IPairRepository<DatabaseModels.OrderItems> orderItemsRepository, IOrderDiscountRepository orderDiscountRepository, IObjectRepository<DatabaseModels.Cashier> cashierRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _discountRepository = discountRepository;
            _orderItemsRepository = orderItemsRepository;
            _orderDiscountRepository = orderDiscountRepository;
            _cashierRepository = cashierRepository;
            ticketNumberLock = new object();
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
                int ticketNum;
                DatabaseModels.OrderItems orderItems;
                DatabaseModels.OrderDiscount orderDiscount;

                lock (ticketNumberLock)
                {
                    if(ticketNumber > 99)
                    {
                        ticketNumber = 1;
                    }
                    ticketNum = ticketNumber;
                    ticketNumber++;
                }
                DatabaseModels.Order order = _orderRepository.Insert(new DatabaseModels.Order(id, orderPost.CashierId, DatabaseModels.OrderStatus.Preparing, orderPost.CreatedAt, orderPost.Price, ticketNum));
                foreach (var product in orderPost.Products)
                {
                    double price = _productRepository.GetProductPrice(product.Id);
                    orderItems = _orderItemsRepository.Insert(new DatabaseModels.OrderItems(id, product.Id, product.Count, price));
                }
                foreach(var discount in orderPost.Discounts)
                {
                    if (discount != null)
                    {
                        bool isAvailable = _discountRepository.GetDiscountStatus(discount.Id);
                        if(isAvailable)
                        {
                            orderDiscount = _orderDiscountRepository.Insert(new DatabaseModels.OrderDiscount(id, discount.Id, discount.Count));
                        }
                        else
                        {
                            throw new Exception(); //TODO: Use more descriptive exception
                        }
                    }
                }
                DatabaseModels.Cashier cashier = _cashierRepository.Get(orderPost.CashierId);
                cashier.Bilans += orderPost.Price;
                cashier = _cashierRepository.Update(cashier);

                var orderProductsItems = _productRepository.GetOrderProducts(order);
                var orderPrice = _productRepository.GetOrderProductPrices(order).Sum();
                var orderDiscounts = _orderDiscountRepository.GetOrderDiscounts(order);
                List <ApiModels.DiscountBasic> discountBasics = new List<ApiModels.DiscountBasic>();
                List<ApiModels.Product> orderProducts = new List<ApiModels.Product>();
                foreach(var e in orderDiscounts)
                {
                    discountBasics.Add(new ApiModels.DiscountBasic(e.DiscountId, e.Quantity));
                }
                foreach(var e in orderProductsItems)
                {
                    orderProducts.Add(new ApiModels.Product(e.id, e.name, e.quantity));
                }
                return new ApiModels.OrderPost(cashier.Id, orderPrice, orderProducts, discountBasics, order.CreatedAt);
            });
        }
    }
}
