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

            var request = new RestRequest("api/crud/cashier");
            string jsonBody = JsonConvert.SerializeObject(cashier);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Cashier GetCashier(Guid guid)
        {
            var request = new RestRequest("api/crud/cashier/{guid}").AddUrlSegment("guid", guid);
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cashier = JsonConvert.DeserializeObject<Cashier>(response.Content);
                return cashier;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Cashier> GetCashiers()
        {
            var request = new RestRequest("api/crud/cashier");
            request.AddHeader("sessionId", SessionId);
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var cashier = JsonConvert.DeserializeObject<List<Cashier>>(response.Content);
                return cashier;
            }
            else
            {
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
            var request = new RestRequest("api/crud/cashier");
            string jsonBody = JsonConvert.SerializeObject(cashier);
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCashier(Guid guid)
        {
            var cashier = new Cashier();
            cashier.FullName = "";
            cashier.Bilans = 0.0;
            cashier.Id = guid;
            string jsonBody = JsonConvert.SerializeObject(cashier);
            var request = new RestRequest("api/crud/cashier");
            request.AddHeader("sessionId", SessionId);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            var response = Client.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
