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
            if(bilans != -1.0)
            {
                cashier.Bilans = bilans;
            }
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
            if(discount.SetPrice != -1.0)
            {
                discount.SetPrice = setPrice;
            }
            if (discount.PriceDropAmount != -1.0)
            {
                discount.PriceDropAmount = priceDropAmount;
            }
            if(discount.PriceDropPercent != -1.0)
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

        public DiscountSetItem GetDiscountSetItem(Guid guid)
        {
            //TODO: Repair guid in route
            var request = new RestRequest("api/crud/DiscountSetItem/{guid}").AddUrlSegment("guid", guid);
            request.AddHeader("sessionId", SessionId);
            //request.AddParameter()
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
            var request = new RestRequest("/api/crud/DiscountSetItem");
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

        /*
        public bool UpdateDiscountSetItem(Guid discountId, Guid productId, int quantity)
        {
            var discountSetItem = GetDiscountSetItem()
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
        */

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
    }
}
