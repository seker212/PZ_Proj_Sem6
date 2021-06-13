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
        public ClientManager()
        {
            RestClient client = new RestClient("https://localhost:44328/");
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
        public void LoginManager()
        {
            string name = null;
            string password = null;
            Login(ref name, ref password);

            var request = new RestRequest("login/manager", );
        }
    }
}
