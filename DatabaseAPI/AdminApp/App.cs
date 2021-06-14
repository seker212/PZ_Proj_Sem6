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

        public void SignIn(ref bool active)
        {
            while(true)
            {
                Console.WriteLine("Wybierz typ użytkownika: 1 - (Administrator) lub  2 (Manager)");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Cm.LoginAdmin();
                    Dm = new DatabaseManager(Client, Cm.SessionId);
                    active = true;
                    break;
                }
                else if (input == "2")
                {
                    Cm.LoginManager();
                    Dm = new DatabaseManager(Client, Cm.SessionId);
                    active = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Błędny typ użytkownika");
                }
            }
        }

        public void CommandHandler()
        {
            bool active = false;
            bool running = true;

            SignIn(ref active);
            while(running)
            {
                string command = Console.ReadLine();
                if(command == "Add cashier")
                {
                    Console.WriteLine("Podaj nazwę kasjera:");
                    string name = Console.ReadLine();
                    bool result = Dm.AddCashier(name);
                    if(result == true)
                    {
                        Console.WriteLine("Dodawanie zakończone powodzeniem");
                    }
                }
                else if(command == "Get cashier")
                {
                    Console.WriteLine("Podaj ID kasjera");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    var cashier = Dm.GetCashier(id);
                    if (cashier != null)
                    {
                        Console.WriteLine("Id: " + cashier.Id);
                        Console.WriteLine("Fullname: " + cashier.FullName);
                        Console.WriteLine("Bilans: " + cashier.Bilans);
                    }
                }
                else if(command == "Get cashiers")
                {
                    var cashiers = Dm.GetCashiers();
                    if(cashiers != null)
                    {
                        foreach(var cashier in cashiers)
                        {
                            Console.WriteLine("Id: " + cashier.Id);
                            Console.WriteLine("Fullname: " + cashier.FullName);
                            Console.WriteLine("Bilans: " + cashier.Bilans);
                        }
                    }
                }
                else if(command == "Update cashier")
                {
                    Console.WriteLine("Podaj ID kasjera");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                    Console.WriteLine("Podaj nazwę kasjera");
                    string input2 = Console.ReadLine();
                    Console.WriteLine("Podaj bilans kasjera");
                    string input3 = Console.ReadLine();
                    double bilans = Convert.ToDouble(input3);
                    bool result = Dm.UpdateCashier(id, input2, bilans);
                    if(result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if(command == "Delete cashier")
                {
                    Console.WriteLine("Podaj ID kasjera");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                }
            }

        }
    }
}
