using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using AdminApp.Models;

namespace AdminApp
{
    class DatabaseManager
    {
        RestClient Client { get; set; }
        string SessionId { get; set; }

        public DatabaseManager(RestClient client, string sessionId)
        {
            Client = client;
            SessionId = sessionId;
        }

        public bool AddCashier(string name)
        {
            Cashier cashier = new Cashier();
            cashier.Id = Guid.NewGuid();
            cashier.FullName = name;
            cashier.Bilans = 0;

            var request = new RestRequest("api/crud/Cashier");
            string jsonBody = JsonConvert.SerializeObject(cashier);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public Cashier GetCashier(Guid guid)
        {
            var request = new RestRequest("api/crud/Cashier/{guid}").AddUrlSegment("guid", guid);
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cashier = JsonConvert.DeserializeObject<Cashier>(response.Content);
                return cashier;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<Cashier> GetCashiers()
        {
            var request = new RestRequest("api/crud/Cashier");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cashier = JsonConvert.DeserializeObject<List<Cashier>>(response.Content);
                return cashier;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateCashier(Guid guid, string name, double bilans)
        {
            var cashier = GetCashier(guid);
            if(name != "")
            {
                cashier.FullName = name;
            }
            cashier.Bilans = bilans;
            var request = new RestRequest("api/crud/Cashier");
            string jsonBody = JsonConvert.SerializeObject(cashier);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool DeleteCashier(Guid guid)
        {
            var cashier = new Cashier
            {
                FullName = "",
                Bilans = 0.0,
                Id = guid
            };
            string jsonBody = JsonConvert.SerializeObject(cashier);
            var request = new RestRequest("api/crud/Cashier");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool AddDiscount(bool isAvailable, double? setPrice, double? priceDropAmount, double? priceDropPercent)
        {
            var discount = new Discount
            {
                Id = Guid.NewGuid(),
                IsAvailable = isAvailable,
                SetPrice = setPrice,
                PriceDropAmount = priceDropAmount,
                PriceDropPercent = priceDropPercent
            };
            var request = new RestRequest("api/crud/Discount");
            string jsonBody = JsonConvert.SerializeObject(discount);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public Discount GetDiscount(Guid guid)
        {
            var request = new RestRequest("api/crud/Discount/{guid}").AddUrlSegment("guid", guid);
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var discount = JsonConvert.DeserializeObject<Discount>(response.Content);
                return discount;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<Discount> GetDiscounts()
        {
            var request = new RestRequest("api/crud/Discount");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var discount = JsonConvert.DeserializeObject<List<Discount>>(response.Content);
                return discount;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateDiscount(Guid guid, bool isAvailable, double setPrice, double priceDropAmount, double priceDropPercent)
        {
            var discount = GetDiscount(guid);
            discount.IsAvailable = isAvailable;
            if(setPrice != -1.0)
            {
                discount.SetPrice = setPrice;
            }
            if (priceDropAmount != -1.0)
            {
                discount.PriceDropAmount = priceDropAmount;
            }
            if (priceDropPercent != -1.0)
            {
                discount.PriceDropPercent = priceDropPercent;
            }
            var request = new RestRequest("api/crud/Discount");
            string jsonBody = JsonConvert.SerializeObject(discount);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool DeleteDiscount(Guid guid)
        {
            var discount = new Discount()
            {
                Id = guid,
                IsAvailable = true,
                SetPrice = 1.0,
                PriceDropAmount = 1.0,
                PriceDropPercent = 1.0
            };
            string jsonBody = JsonConvert.SerializeObject(discount);
            var request = new RestRequest("api/crud/Discount");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool AddDiscountSetItem(Guid discountId, Guid productId, int quantity)
        {
            var discountSetItem = new DiscountSetItem()
            {
                DiscountId = discountId,
                ProductId = productId,
                Quantity = quantity
            };
            var request = new RestRequest("api/crud/DiscountSetItem");
            string jsonBody = JsonConvert.SerializeObject(discountSetItem);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public DiscountSetItem GetDiscountSetItem(Guid discountId, Guid productId)
        {
            var request = new RestRequest("api/crud/DiscountSetItem/instance");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("discountId", discountId);
            request.AddParameter("productId", productId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var discountSetItem = JsonConvert.DeserializeObject<DiscountSetItem>(response.Content);
                return discountSetItem;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<DiscountSetItem> GetDiscountSetItems()
        {
            var request = new RestRequest("api/crud/DiscountSetItem");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var discountSetItem = JsonConvert.DeserializeObject<List<DiscountSetItem>>(response.Content);
                return discountSetItem;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateDiscountSetItem(Guid discountId, Guid productId, int quantity)
        {
            var discountSetItem = GetDiscountSetItem(discountId, productId);
            if(quantity != -1)
            {
                discountSetItem.Quantity = quantity;
            }
            var request = new RestRequest("api/crud/DiscountSetItem");
            string jsonBody = JsonConvert.SerializeObject(discountSetItem);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }
        
        public bool DeleteDiscountSetItem(Guid discountId, Guid productId)
        {
            var discountSetItem = new DiscountSetItem()
            {
                DiscountId = discountId,
                ProductId = productId,
                Quantity = 1
            };
            string jsonBody = JsonConvert.SerializeObject(discountSetItem);
            var request = new RestRequest("api/crud/DiscountSetItem");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool AddOrder(Guid cashierId, OrderStatus status, double price, int ticketNumber)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                CashierId = cashierId,
                Status = EnumCaster.OrderStatusToNumber(status),
                CreatedAt = DateTime.Now,
                Price = price,
                TicketNumber = ticketNumber
            };
            var request = new RestRequest("api/crud/Orders");
            string jsonBody = JsonConvert.SerializeObject(order);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public Order GetOrder(Guid guid)
        {
            var request = new RestRequest("api/crud/Orders/{guid}").AddUrlSegment("guid", guid);
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var order = JsonConvert.DeserializeObject<Order>(response.Content);
                return order;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            var request = new RestRequest("api/crud/Orders");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var orders = JsonConvert.DeserializeObject<List<Order>>(response.Content);
                return orders;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateOrder(Guid guid, Guid cashierId, int status, double price, int ticketNumber)
        {
            var order = GetOrder(guid);
            order.CashierId = cashierId;
            if(status != -1)
            {
                order.Status = status;
            }
            if(price != -1)
            {
                order.Price = price;
            }
            if(ticketNumber != -1)
            {
                order.TicketNumber = ticketNumber;
            }
            var request = new RestRequest("api/crud/Orders");
            string jsonBody = JsonConvert.SerializeObject(order);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool DeleteOrder(Guid guid)
        {
            var order = new Order()
            {
                Id = guid,
                CashierId = Guid.NewGuid(),
                Status = 1,
                CreatedAt = DateTime.Now,
                Price = 1.0,
                TicketNumber = 1
            };
            string jsonBody = JsonConvert.SerializeObject(order);
            var request = new RestRequest("api/crud/Orders");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool AddOrderDiscount(Guid orderId, Guid discountId, int quantity)
        {
            var orderDiscount = new OrderDiscount()
            {
                OrderId = orderId,
                DiscountId = discountId,
                Quantity = quantity
            };
            var request = new RestRequest("api/crud/OrderDiscount");
            string jsonBody = JsonConvert.SerializeObject(orderDiscount);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public OrderDiscount GetOrderDiscount(Guid orderId, Guid discountId)
        {
            var request = new RestRequest("api/crud/OrderDiscount/instance");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("orderId", orderId);
            request.AddParameter("discountId", discountId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var orderDiscount = JsonConvert.DeserializeObject<OrderDiscount>(response.Content);
                return orderDiscount;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<OrderDiscount> GetOrderDiscounts()
        {
            var request = new RestRequest("api/crud/OrderDiscount");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var ordersDiscount = JsonConvert.DeserializeObject<List<OrderDiscount>>(response.Content);
                return ordersDiscount;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateOrderDiscount(Guid orderId, Guid discountId, int quantity)
        {
            var orderDiscount = GetOrderDiscount(orderId, discountId);
            orderDiscount.OrderId = orderId;
            orderDiscount.DiscountId = discountId;
            if (quantity != -1)
            {
                orderDiscount.Quantity = quantity;
            }
            var request = new RestRequest("api/crud/OrderDiscount");
            string jsonBody = JsonConvert.SerializeObject(orderDiscount);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool DeleteOrderDiscount(Guid orderId, Guid discountId)
        {
            var orderDiscount = new OrderDiscount()
            {
                OrderId = orderId,
                DiscountId = discountId,
                Quantity = 1
            };
            string jsonBody = JsonConvert.SerializeObject(orderDiscount);
            var request = new RestRequest("api/crud/OrderDiscount");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool AddOrderItem(Guid orderId, Guid productId, int quantity, double price)
        {
            var orderItem = new OrderItems()
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };
            var request = new RestRequest("api/crud/OrderItems");
            string jsonBody = JsonConvert.SerializeObject(orderItem);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public OrderItems GetOrderItem(Guid orderId, Guid productId)
        {
            var request = new RestRequest("api/crud/OrderItems/instance");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("orderId", orderId);
            request.AddParameter("productId", productId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var orderItem = JsonConvert.DeserializeObject<OrderItems>(response.Content);
                return orderItem;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<OrderItems> GetOrderItems()
        {
            var request = new RestRequest("api/crud/OrderItems");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var ordersItems = JsonConvert.DeserializeObject<List<OrderItems>>(response.Content);
                return ordersItems;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateOrderItem(Guid orderId, Guid productId, int quantity, double price)
        {
            var orderItem = GetOrderItem(orderId, productId);
            orderItem.OrderId = orderId;
            orderItem.ProductId = productId;
            if (quantity != -1)
            {
                orderItem.Quantity = quantity;
            }
            if(price != -1.0)
            {
                orderItem.Price = price;
            }
            var request = new RestRequest("api/crud/OrderItems");
            string jsonBody = JsonConvert.SerializeObject(orderItem);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool DeleteOrderItem(Guid orderId, Guid productId)
        {
            var orderItem = new OrderItems()
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = 1,
                Price = 1.0
            };
            string jsonBody = JsonConvert.SerializeObject(orderItem);
            var request = new RestRequest("api/crud/OrderItems");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool AddProduct(Guid id, string name, double price, int status)
        {
            var product = new Product()
            {
                Id = id,
                Name = name,
                Price = price,
                Status = status
            };
            var request = new RestRequest("api/crud/Product");
            string jsonBody = JsonConvert.SerializeObject(product);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public Product GetProduct(Guid guid)
        {
            var request = new RestRequest("api/crud/Product/{guid}").AddUrlSegment("guid", guid);
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var product = JsonConvert.DeserializeObject<Product>(response.Content);
                return product;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            var request = new RestRequest("api/crud/Product");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(response.Content);
                return products;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return null;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return null;
            }
        }

        public bool UpdateOrderItem(Guid id, string name, double price, int status)
        {
            var product = GetProduct(id);
            product.Id = id;
            if(name != "")
            {
                product.Name = name;
            }
            if(price != -1.0)
            {
                product.Price = price;
            }
            if(status != -1)
            {
                product.Status = status;
            }
            var request = new RestRequest("api/crud/Product");
            string jsonBody = JsonConvert.SerializeObject(product);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }

        public bool DeleteProduct(Guid guid)
        {
            var product = new Product()
            {
                Id = guid,
                Name = "",
                Price = 1.0,
                Status = 1
            };
            string jsonBody = JsonConvert.SerializeObject(product);
            var request = new RestRequest("api/crud/Product");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Brak uprawnień");
                return false;
            }
            else
            {
                Console.WriteLine("Błędne parametry");
                return false;
            }
        }
    }
}
