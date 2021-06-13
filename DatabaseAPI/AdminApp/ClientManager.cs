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
    public class ClientManager
    {
        public UserType type;
        public RestClient client;
        public string sessionId;

        public ClientManager()
        {
            client = new RestClient("https://localhost:44328/");
        }

        void Login(ref string name, ref string password)
        {
            Console.Write("Login: ");
            name = Console.ReadLine();
            Console.Write("Password: ");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }
        }
        public bool LoginManager()
        {
            string name = null;
            string password = null;
            Login(ref name, ref password);

            var request = new RestRequest("api/User/login/manager");
            request.AddParameter("username", name);
            request.AddParameter("password", password);
            request.RequestFormat = DataFormat.Json;
            var response= client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                type = UserType.Manager;
                sessionId = response.Content;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoginAdmin()
        {
            string name = null;
            string password = null;
            Login(ref name, ref password);

            var request = new RestRequest("api/User/login/admin");
            request.AddParameter("username", name);
            request.AddParameter("password", password);
            //request.RequestFormat = DataFormat.Json;
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                type = UserType.Admin;
                sessionId = response.Content;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void test()
        {
            var request = new RestRequest("api/Product/available", DataFormat.Json);
            var response = client.Get(request);
            //var products = JsonConvert.DeserializeObject<List<ProductData>>(response.Content);
        }
    }
}
