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
    class Program
    {
        static void Main(string[] args)
        {

            RestClient client = new RestClient("https://localhost:44328/");
            ClientManager cm = new ClientManager(client);
            cm.LoginAdmin();
            DatabaseManager dm = new DatabaseManager(client, cm.SessionId);


        }
    }
}
