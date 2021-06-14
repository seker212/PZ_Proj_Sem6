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
    class App
    {
        RestClient Client { get; set; }
        ClientManager Cm { get; set; }
        DatabaseManager Dm { get; set; }

        public App()
        {
           Client  = new RestClient("https://localhost:44328/");
           Cm = new ClientManager(Client);
        }


    }
}
