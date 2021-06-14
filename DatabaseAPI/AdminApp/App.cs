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
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Id: " + cashier.Id);
                            Console.WriteLine("Fullname: " + cashier.FullName);
                            Console.WriteLine("Bilans: " + cashier.Bilans);
                            Console.WriteLine("------------------------------------");
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
                    bool result = Dm.DeleteCashier(id);
                    if(result == true)
                    {
                        Console.WriteLine("Usuwanie zakończone powodzeniem");
                    }
                }
                else if(command == "Add discount")
                {
                    double? setPrice;
                    double? priceDropAmount;
                    double? priceDropPercent;

                    Console.WriteLine("Podaj dostępność (true/false)");
                    bool input1 = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Podaj zmienioną cenę");
                    string input2 = Console.ReadLine();
                    if(input2 == "" || input2 == "\n")
                    {
                        setPrice = null;
                    }
                    else
                    {
                        setPrice = Convert.ToDouble(input2);
                    }
                    Console.WriteLine("Podaj obniżenie ceny");
                    string input3 = Console.ReadLine();
                    if (input3 == "" || input3 == "\n")
                    {
                        priceDropAmount = null;
                    }
                    else
                    {
                        priceDropAmount = Convert.ToDouble(input3);
                    }
                    Console.WriteLine("Podaj procentowe obniżenie ceny");
                    string input4 = Console.ReadLine();
                    if (input4 == "" || input4 == "\n")
                    {
                        priceDropPercent = null;
                    }
                    else
                    {
                        priceDropPercent = Convert.ToDouble(input4);
                    }
                    bool result = Dm.AddDiscount(input1, setPrice, priceDropAmount, priceDropPercent);
                    if(result == true)
                    {
                        Console.WriteLine("Poprawnie dodano zniżkę");
                    }
                }
                else if(command == "Get discount")
                {
                    Console.WriteLine("Podaj ID zniżki");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                    var discount = Dm.GetDiscount(id);
                    if(discount != null)
                    {
                        Console.WriteLine("Id: " + discount.Id);
                        Console.WriteLine("IsAvailable: " + discount.IsAvailable);
                        Console.WriteLine("SetPrice: " + discount.SetPrice);
                        Console.WriteLine("PriceDropAmount: " + discount.PriceDropAmount);
                        Console.WriteLine("PriceDropPercent: " + discount.PriceDropPercent);
                    }
                }
                else if(command == "Get discounts")
                {
                    var discounts = Dm.GetDiscounts();
                    foreach(var discount in discounts)
                    {
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Id: " + discount.Id);
                        Console.WriteLine("IsAvailable: " + discount.IsAvailable);
                        Console.WriteLine("SetPrice: " + discount.SetPrice);
                        Console.WriteLine("PriceDropAmount: " + discount.PriceDropAmount);
                        Console.WriteLine("PriceDropPercent: " + discount.PriceDropPercent);
                        Console.WriteLine("------------------------------------");
                    }
                }
                else if(command == "Update discount")
                {
                    Console.WriteLine("Podaj ID zniżki:");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    Console.WriteLine("Podaj dostępność (true/false)");
                    bool input1 = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Podaj zmienioną cenę");
                    double input2 = Convert.ToDouble(Console.ReadLine());
                    
                    Console.WriteLine("Podaj obniżenie ceny");
                    double input3 = Convert.ToDouble(Console.ReadLine());
                    
                    Console.WriteLine("Podaj procentowe obniżenie ceny");
                    double input4 = Convert.ToDouble(Console.ReadLine());
                    
                    bool result = Dm.UpdateDiscount(id, input1, input2, input3, input4);
                    if (result == true)
                    {
                        Console.WriteLine("Poprawnie zaktualizowano zniżkę");
                    }
                }
                else if(command == "Delete discount")
                {
                    Console.WriteLine("Podaj ID zniżki:");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    bool result = Dm.DeleteDiscount(id);
                    if (result == true)
                    {
                        Console.WriteLine("Poprawnie usunięto zniżkę");
                    }
                }
                else if(command == "Add discountsetitem")
                {
                    Console.WriteLine("Podaj ID zniżki:");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    Console.WriteLine("Podaj ID produktu:");
                    string input2 = Console.ReadLine();
                    Guid id2 = new Guid(input2);
                    Console.WriteLine("Podaj ilość:");
                    int input3 = Convert.ToInt32(Console.ReadLine());
                    bool result = Dm.AddDiscountSetItem(id, id2, input3);
                    if (result == true)
                    {
                        Console.WriteLine("Operacja powiodła się");
                    }
                }
                else if(command == "Get discounsetitem")
                {
                    Console.WriteLine("Podaj ID zniżki:");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    Console.WriteLine("Podaj ID produktu:");
                    string input2 = Console.ReadLine();
                    Guid id2 = new Guid(input2);
                    var discountSetItem = Dm.GetDiscountSetItem(id, id2);
                    if(discountSetItem != null)
                    {
                        Console.WriteLine("DiscountId: " + discountSetItem.DiscountId);
                        Console.WriteLine("ProductId: " + discountSetItem.ProductId);
                        Console.WriteLine("Quantity: " + discountSetItem.Quantity);

                    }

                }
                else if (command == "Get discountsetitems")
                {
                    var discountsetitems = Dm.GetDiscountSetItems();
                    if (discountsetitems != null)
                    {
                        foreach (var discountsetitem in discountsetitems)
                        {
                            Console.WriteLine("DiscountId: " + discountsetitem.DiscountId);
                            Console.WriteLine("ProductId: " + discountsetitem.ProductId);
                            Console.WriteLine("Quantity: " + discountsetitem.Quantity);
                        }
                    }
                }
                else if (command == "Update discountsetitem")
                {
                    Console.WriteLine("Podaj ID zniżki:");
                    string input1 = Console.ReadLine();
                    Guid discountId = new Guid(input1);
                    Console.WriteLine("Podaj ID produktu:");
                    string input1 = Console.ReadLine();
                    Guid productId = new Guid(input1);
                    Console.WriteLine("Podaj ilość:");
                    string input2 = Console.ReadLine();
                    int status = Int32.parse(input2);
                    bool result = Dm.UpdateDiscountSetItems(discountId, productId, quantity);
                    if (result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if (command == "Delete discountsetitem")
                {
                    Console.WriteLine("Podaj ID zniżki:");
                    string input1 = Console.ReadLine();
                    Guid discountId = new Guid(input1);
                    Console.WriteLine("Podaj ID produktu:");
                    string input1 = Console.ReadLine();
                    Guid productId = new Guid(input1);
                    bool result = Dm.DeleteDiscountSetItems(discountId, productId);
                    if (result == true)
                    {
                        Console.WriteLine("Usuwanie zakończona powodzeniem");
                    }
                }


                else if (command == "Add order")
                {
                    Console.WriteLine("Podaj ID kasjera:");
                    string input1 = Console.ReadLine();
                    Guid cashierId = new Guid(input1);
                    Console.WriteLine("Podaj status zamówienia:");
                    string input2 = Console.ReadLine();
                    int status = Int32.parse(input2);
                    Console.WriteLine("Podaj cenę:");
                    string priceConsole = Console.ReadLine();
                    double price = Convert.ToDouble(priceConsole);
                    Console.WriteLine("Podaj numer zamówienia:");
                    string ticketNumberConsole = Console.ReadLine();
                    int ticketNumber = Int32.Parse(ticketNumberConsole);
                    bool result = Dm.AddOrder(cashierId, status, price, ticketNumber);
                    if (result == true)
                    {
                        Console.WriteLine("Dodawanie zakończone powodzeniem");
                    }
                }
                else if (command == "Get order")
                {
                    Console.WriteLine("Podaj ID zamówienia");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    var order = Dm.GetOrder(orderId);
                    if (orderitem != null)
                    {
                        Console.WriteLine("Id: " + order.Id);
                        Console.WriteLine("CashierId: " + order.CashierId);
                        Console.WriteLine("Status: " + order.Status);
                        Console.WriteLine("CreatedAt: " + order.CreatedAt);
                        Console.WriteLine("Price: " + order.Price);
                        Console.WriteLine("TicketNumber: " + order.TicketNumber);
                    }
                }
                else if (command == "Get orders")
                {
                    var orders = Dm.GetOrders();
                    if (orders != null)
                    {
                        foreach (var order in orders)
                        {
                            Console.WriteLine("Id: " + order.Id);
                            Console.WriteLine("CashierId: " + order.CashierId);
                            Console.WriteLine("Status: " + order.Status);
                            Console.WriteLine("CreatedAt: " + order.CreatedAt);
                            Console.WriteLine("Price: " + order.Price);
                            Console.WriteLine("TicketNumber: " + order.TicketNumber);
                        }
                    }
                }
                else if (command == "Update order")
                {
                    Console.WriteLine("Podaj ID zamówienia:");
                    string input1 = Console.ReadLine();
                    Guid guid = new Guid(input1);
                    Console.WriteLine("Podaj ID kasjera:");
                    string input1 = Console.ReadLine();
                    Guid cashierId = new Guid(input1);
                    Console.WriteLine("Podaj status zamówienia:");
                    string input2 = Console.ReadLine();
                    int status = Int32.parse(input2);
                    Console.WriteLine("Podaj cenę:");
                    string priceConsole = Console.ReadLine();
                    double price = Convert.ToDouble(priceConsole);
                    Console.WriteLine("Podaj numer zamówienia:");
                    string ticketNumberConsole = Console.ReadLine();
                    int ticketNumber = Int32.Parse(ticketNumberConsole);
                    bool result = Dm.UpdateOrder(guid, cashierId, status, price, ticketNumber);
                    if (result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if (command == "Delete order")
                {
                    Console.WriteLine("Podaj ID zamówienia");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    bool result = Dm.DeleteOrder(orderId);
                    if (result == true)
                    {
                        Console.WriteLine("Usuwanie zakończona powodzeniem");
                    }
                }

                else if (command == "Add orderdiscount")
                {
                    Console.WriteLine("Podaj ID zamówienia:");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID zniżki:");
                    string input2 = Console.ReadLine();
                    Guid discountId = new Guid(input2);
                    Console.WriteLine("Podaj ilość:");
                    string quantityConsole = Console.ReadLine();
                    int quantity = Int32.Parse(quantityConsole);
                    bool result = Dm.AddOrderItem(orderId, discountId, quantity);
                    if (result == true)
                    {
                        Console.WriteLine("Dodawanie zakończone powodzeniem");
                    }
                }
                else if (command == "Get orderdiscount")
                {
                    Console.WriteLine("Podaj ID zamówienia");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID zniżki");
                    string input2 = Console.ReadLine();
                    Guid discountId = new Guid(input2);
                    var orderdiscount = Dm.GetOrderDiscount(orderId, discountId);
                    if (orderitem != null)
                    {
                        Console.WriteLine("OrderId: " + orderdiscount.OrderId);
                        Console.WriteLine("ProductId: " + orderdiscount.DiscountId);
                        Console.WriteLine("Quantity: " + orderdiscount.Quantity);
                    }
                }
                else if (command == "Get orderdiscounts")
                {
                    var orderdiscounts = Dm.GetOrderDiscounts();
                    if (orderitems != null)
                    {
                        foreach (var orderdiscount in orderdiscounts)
                        {
                            Console.WriteLine("OrderId: " + orderdiscount.OrderId);
                            Console.WriteLine("ProductId: " + orderdiscount.DiscountId);
                            Console.WriteLine("Quantity: " + orderdiscount.Quantity);
                        }
                    }
                }
                else if (command == "Update orderdiscount")
                {
                    Console.WriteLine("Podaj ID zamówienia:");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID zniżki:");
                    string input2 = Console.ReadLine();
                    Guid discountId = new Guid(input2);
                    Console.WriteLine("Podaj ilość:");
                    string quantityConsole = Console.ReadLine();
                    int quantity = Int32.Parse(quantityConsole);
                    bool result = Dm.UpdateOrderDiscount(orderId, discountId, quantity);
                    if (result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if (command == "Delete orderdiscount")
                {
                    Console.WriteLine("Podaj ID zamówienia");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID zniżki");
                    string input2 = Console.ReadLine();
                    Guid discountId = new Guid(input2);
                    bool result = Dm.DeleteOrderDiscount(orderId, discountId);
                    if (result == true)
                    {
                        Console.WriteLine("Usuwanie zakończona powodzeniem");
                    }
                }

                else if (command == "Add orderitem")
                {
                    Console.WriteLine("Podaj ID zamówienia:");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID produktu:");
                    string input2 = Console.ReadLine();
                    Guid productId = new Guid(input2);
                    Console.WriteLine("Podaj ilość:");
                    string quantityConsole = Console.ReadLine();
                    int quantity = Int32.Parse(quantityConsole);
                    Console.WriteLine("Podaj cenę:");
                    string priceConsole = Console.ReadLine();
                    double price = Convert.ToDouble(priceConsole)
                    bool result = Dm.AddOrderItem(orderId, productId, quantity, price);
                    if (result == true)
                    {
                        Console.WriteLine("Dodawanie zakończone powodzeniem");
                    }
                }
                else if (command == "Get orderitem")
                {
                    Console.WriteLine("Podaj ID zamówienia");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID productu");
                    string input2 = Console.ReadLine();
                    Guid productId = new Guid(input2);
                    var orderitem = Dm.GetOrderItem(orderId, productId);
                    if (orderitem != null)
                    {
                        Console.WriteLine("OrderId: " + orderitem.OrderId);
                        Console.WriteLine("ProductId: " + orderitem.ProductId);
                        Console.WriteLine("Quantity: " + orderitem.Quantity);
                        Console.WriteLine("Price: " + orderitem.Price);
                    }
                }
                else if (command == "Get orderitems")
                {
                    var orderitems = Dm.GetOrderItems();
                    if (orderitems != null)
                    {
                        foreach (var orderitem in orderitems)
                        {
                            Console.WriteLine("OrderId: " + orderitem.OrderId);
                            Console.WriteLine("ProductId: " + orderitem.ProductId);
                            Console.WriteLine("Quantity: " + orderitem.Quantity);
                            Console.WriteLine("Price: " + orderitem.Price);
                        }
                    }
                }
                else if (command == "Update orderitem")
                {
                    Console.WriteLine("Podaj ID zamówienia:");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID produktu:");
                    string input2 = Console.ReadLine();
                    Guid productId = new Guid(input2);
                    Console.WriteLine("Podaj ilość:");
                    string quantityConsole = Console.ReadLine();
                    int quantity = Int32.Parse(quantityConsole);
                    Console.WriteLine("Podaj cenę:");
                    string priceConsole = Console.ReadLine();
                    double price = Convert.ToDouble(priceConsole)
                    bool result = Dm.UpdateOrderItem(orderId, productId, quantity, price);
                    if (result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if (command == "Delete orderitem")
                {
                    Console.WriteLine("Podaj ID zamówienia:");
                    string input1 = Console.ReadLine();
                    Guid orderId = new Guid(input1);
                    Console.WriteLine("Podaj ID produktu:");
                    string input2 = Console.ReadLine();
                    Guid productId = new Guid(input2);
                    bool result = Dm.DeleteOrderItem(orderId, productId);
                    if (result == true)
                    {
                        Console.WriteLine("Usuwanie zakończona powodzeniem");
                    }
                }

                else if (command == "Add product")
                {
                    Console.WriteLine("Podaj nazwę produktu:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Podaj cenę produktu:");
                    string priceConsole = Console.ReadLine();
                    double price = Convert.ToDouble(priceConsole)

                    Console.WriteLine("Podaj status produktu:");
                    string statusConsole = Console.ReadLine();
                    int status = Int32.Parse(statusConsole);
                    bool result = Dm.AddProduct(name, price, type);
                    if (result == true)
                    {
                        Console.WriteLine("Dodawanie zakończone powodzeniem");
                    }
                }
                else if (command == "Get product")
                {
                    Console.WriteLine("Podaj ID productu");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    var product = Dm.GetProduct(id);
                    if (product != null)
                    {
                        Console.WriteLine("Id: " + product.Id);
                        Console.WriteLine("Name: " + product.Name);
                        Console.WriteLine("Price: " + product.Price);
                        Console.WriteLine("Status: " + product.Status);
                    }
                }
                else if (command == "Get products")
                {
                    var products = Dm.GetProducts();
                    if (products != null)
                    {
                        foreach (var product in products)
                        {
                            Console.WriteLine("Id: " + product.Id);
                            Console.WriteLine("Name: " + product.Name);
                            Console.WriteLine("Price: " + product.Price);
                            Console.WriteLine("Status: " + product.Status);
                        }
                    }
                }
                else if (command == "Update product")
                {
                    Console.WriteLine("Podaj ID produktu");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                    Console.WriteLine("Podaj nazwę produktu:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Podaj cenę produktu:");
                    string priceConsole = Console.ReadLine();
                    double price = Convert.ToDouble(priceConsole)

                    Console.WriteLine("Podaj status produktu:");
                    string statusConsole = Console.ReadLine();
                    int status = Int32.Parse(statusConsole);
                    bool result = Dm.UpdateProduct(id, name, price, status);
                    if (result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if (command == "Delete product")
                {
                    Console.WriteLine("Podaj ID produktu");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                    bool result = Dm.DeleteProduct(id);
                    if (result == true)
                    {
                        Console.WriteLine("Usuwanie zakończona powodzeniem");
                    }
                }


                else if (command == "Add user")
                {
                    Console.WriteLine("Podaj nazwę użytkownika:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Podaj hasło użytkownika:");
                    string password = "";
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        password += key.KeyChar;
                    }
                    Console.WriteLine("Podaj typ użytkownika:");
                    string typeConsole = Console.ReadLine();
                    int type = Int32.Parse(typeConsole);
                    bool result = Dm.AddUser(name, password, type);
                    if (result == true)
                    {
                        Console.WriteLine("Dodawanie zakończone powodzeniem");
                    }
                }
                else if (command == "Get user")
                {
                    Console.WriteLine("Podaj ID użytkownika");
                    string input = Console.ReadLine();
                    Guid id = new Guid(input);
                    var user = Dm.GetUser(id);
                    if (user != null)
                    {
                        Console.WriteLine("Id: " + user.Id);
                        Console.WriteLine("Username: " + user.Username);
                        Console.WriteLine("PasswordHash: " + user.PasswordHash);
                        Console.WriteLine("Type: " + user.Type);
                    }
                }
                else if (command == "Get users")
                {
                    var users = Dm.GetUsers();
                    if (users != null)
                    {
                        foreach (var user in users)
                        {
                            Console.WriteLine("Id: " + user.Id);
                            Console.WriteLine("Username: " + user.Username);
                            Console.WriteLine("PasswordHash: " + user.PasswordHash);
                            Console.WriteLine("Type: " + user.Type);
                        }
                    }
                }
                else if (command == "Update user")
                {
                    Console.WriteLine("Podaj ID użytkownika");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                    Console.WriteLine("Podaj nazwę użytkownika:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Podaj hasło użytkownika:");
                    string password = "";
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        password += key.KeyChar;
                    }
                    Console.WriteLine("Podaj typ użytkownika:");
                    string typeConsole = Console.ReadLine();
                    int type = Int32.Parse(typeConsole);
                    bool result = Dm.UpdateUser(id, name, password, type);
                    if (result == true)
                    {
                        Console.WriteLine("Aktualizacja zakończona powodzeniem");
                    }
                }
                else if (command == "Delete user")
                {
                    Console.WriteLine("Podaj ID użytkownika");
                    string input1 = Console.ReadLine();
                    Guid id = new Guid(input1);
                    bool result = Dm.DeleteUser(id);
                    if (result == true)
                    {
                        Console.WriteLine("Usuwanie zakończona powodzeniem");
                    }
                }
            }

        }
    }
}
