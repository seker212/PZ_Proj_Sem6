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
        public UserType Type { get; set; }
        public RestClient Client { get; set; }
        public string SessionId { get; set; }

        public ClientManager(RestClient client)
        {
            Client = client;
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
            var response= Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Type = UserType.Manager;
                SessionId = response.Content.Trim('"');
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
            var response = Client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Type = UserType.Admin;
                SessionId = response.Content.Trim('"');
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
