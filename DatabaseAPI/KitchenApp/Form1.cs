using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using RestSharp;
using RestSharp.Authenticators;


namespace KitchenApp
{
    public class OrderItems
    {
        public string id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public List<OrderItems> products { get; set; }
        public int ticketNumber { get; set; }
    }

    public partial class Form1 : Form
    {
        String orderBox1_Id;
        String orderBox2_Id;
        String orderBox3_Id;

        public Form1()
        {
            InitializeComponent();

            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new ElapsedEventHandler(RefreshList);
            myTimer.Interval = 5000; // 1000 ms is one second
            myTimer.Start();

            var client = new RestClient("https://localhost:44328/");
            //client.Authenticator = new HttpBasicAuthenticator("postgres", "mysecretpassword");

            var request = new RestRequest("api/Orders/kitchen", DataFormat.Json);
            var response = client.Get(request);
            //Console.Write(response.Content);

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(response.Content);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            List<OrderItems> produkty1, produkty2, produkty3 = new List<OrderItems>();

            try
            {
                listBox1.Items.Add("Nr zamówienia: " + orders.ElementAt(0).ticketNumber + "\n\r");
                produkty1 = orders.ElementAt(0).products;
                for (int i = 0; i < produkty1.Count(); i++)
                {
                    listBox1.Items.Add(produkty1[i].name + " x" + produkty1[i].count);
                }
                orderBox1_Id = orders.ElementAt(0).id;
            }
            catch (ArgumentOutOfRangeException e)
            {
                listBox1.Items.Add("Brak zamówień");
                button1.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox2.Items.Add("Nr zamówienia: " + orders.ElementAt(1).ticketNumber + "\n\r");
                produkty2 = orders.ElementAt(1).products;
                for (int i = 0; i < produkty2.Count(); i++)
                {
                    listBox2.Items.Add(produkty2[i].name + " x" + produkty2[i].count);
                }
                orderBox2_Id = orders.ElementAt(1).id;
            }
            catch (ArgumentOutOfRangeException e)
            {
                listBox2.Items.Add("Brak zamówień");
                button2.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox3.Items.Add("Nr zamówienia: " + orders.ElementAt(2).ticketNumber + "\n\r");
                produkty3 = orders.ElementAt(2).products;
                for (int i = 0; i < produkty3.Count(); i++)
                {
                    listBox1.Items.Add(produkty3[i].name + " x" + produkty3[i].count);
                }
                orderBox3_Id = orders.ElementAt(2).id;
            } catch (ArgumentOutOfRangeException e)
            {
                listBox3.Items.Add("Brak zamówień");
                button3.Text = "Brak zamówienia";
            }
            

            //get daje full liste zamówień
            //brać liste i odświeżać co jakiś czas
            //ładować pierwsze 3 i potem oddawać wykonanie konkretnych
            //get api/Orders/kitchen

        }

        public void RefreshList(object source, ElapsedEventArgs e)
        {
            var client = new RestClient("https://localhost:44328/");
            var request = new RestRequest("api/Orders/kitchen", DataFormat.Json);
            var response = client.Get(request);
            //Console.Write(response.Content);

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(response.Content);

            List<OrderItems> produkty1, produkty2, produkty3 = new List<OrderItems>();

            try
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Nr zamówienia: " + orders.ElementAt(0).ticketNumber + "\n\r");
                produkty1 = orders.ElementAt(0).products;
                for (int i = 0; i < produkty1.Count(); i++)
                {
                    listBox1.Items.Add(produkty1[i].name + " x" + produkty1[i].count);
                }
                orderBox1_Id = orders.ElementAt(0).id;
                button1.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox1.Items.Add("Brak zamówień");
                button1.Text = "Brak zamówienia";
            }


            try
            {
                listBox2.Items.Clear();
                listBox2.Items.Add("Nr zamówienia: " + orders.ElementAt(1).ticketNumber + "\n\r");
                produkty2 = orders.ElementAt(1).products;
                for (int i = 0; i < produkty2.Count(); i++)
                {
                    listBox2.Items.Add(produkty2[i].name + " x" + produkty2[i].count);
                }
                orderBox2_Id = orders.ElementAt(1).id;
                button2.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox2.Items.Add("Brak zamówień");
                button2.Text = "Brak zamówienia";
            }


            try
            {
                listBox3.Items.Clear();
                listBox3.Items.Add("Nr zamówienia: " + orders.ElementAt(2).ticketNumber + "\n\r");
                produkty3 = orders.ElementAt(2).products;
                for (int i = 0; i < produkty3.Count(); i++)
                {
                    listBox1.Items.Add(produkty3[i].name + " x" + produkty3[i].count);
                }
                orderBox3_Id = orders.ElementAt(2).id;
                button3.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox3.Items.Add("Brak zamówień");
                button3.Text = "Brak zamówienia";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Wykonano zamówienie, wczytuję nowe");
            var client = new RestClient("https://localhost:44328/");
            var request_done = new RestRequest("api/Orders/kitchen/" + orderBox1_Id, DataFormat.Json);
            var response_done = client.Put(request_done);

            var request = new RestRequest("api/Orders/kitchen", DataFormat.Json);
            var response = client.Get(request);
            //Console.Write(response.Content);

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(response.Content);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            List<OrderItems> produkty1, produkty2, produkty3 = new List<OrderItems>();

            try
            {
                listBox1.Items.Add("Nr zamówienia: " + orders.ElementAt(0).ticketNumber + "\n\r");
                produkty1 = orders.ElementAt(0).products;
                for (int i = 0; i < produkty1.Count(); i++)
                {
                    listBox1.Items.Add(produkty1[i].name + " x" + produkty1[i].count);
                }
                orderBox1_Id = orders.ElementAt(0).id;
                button1.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox1.Items.Add("Brak zamówień");
                button1.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox2.Items.Add("Nr zamówienia: " + orders.ElementAt(1).ticketNumber + "\n\r");
                produkty2 = orders.ElementAt(1).products;
                for (int i = 0; i < produkty2.Count(); i++)
                {
                    listBox2.Items.Add(produkty2[i].name + " x" + produkty2[i].count);
                }
                orderBox2_Id = orders.ElementAt(1).id;
                button2.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox2.Items.Add("Brak zamówień");
                button2.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox3.Items.Add("Nr zamówienia: " + orders.ElementAt(2).ticketNumber + "\n\r");
                produkty3 = orders.ElementAt(2).products;
                for (int i = 0; i < produkty3.Count(); i++)
                {
                    listBox1.Items.Add(produkty3[i].name + " x" + produkty3[i].count);
                }
                orderBox3_Id = orders.ElementAt(2).id;
                button3.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox3.Items.Add("Brak zamówień");
                button3.Text = "Brak zamówienia";
            }
            
            //załadować zamówienie z listy
            //wysłać ukończenie danego order po id
            //put api/Orders/kitchen/{orderId}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("Wykonano zamówienie, wczytuję nowe");
            var client = new RestClient("https://localhost:44328/");
            var request_done = new RestRequest("api/Orders/kitchen/" + orderBox2_Id, DataFormat.Json);
            var response_done = client.Put(request_done);

            var request = new RestRequest("api/Orders/kitchen", DataFormat.Json);
            var response = client.Get(request);
            //Console.Write(response.Content);

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(response.Content);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            List<OrderItems> produkty1, produkty2, produkty3 = new List<OrderItems>();

            try
            {
                listBox1.Items.Add("Nr zamówienia: " + orders.ElementAt(0).ticketNumber + "\n\r");
                produkty1 = orders.ElementAt(0).products;
                for (int i = 0; i < produkty1.Count(); i++)
                {
                    listBox1.Items.Add(produkty1[i].name + " x" + produkty1[i].count);
                }
                orderBox1_Id = orders.ElementAt(0).id;
                button1.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox1.Items.Add("Brak zamówień");
                button1.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox2.Items.Add("Nr zamówienia: " + orders.ElementAt(1).ticketNumber + "\n\r");
                produkty2 = orders.ElementAt(1).products;
                for (int i = 0; i < produkty2.Count(); i++)
                {
                    listBox2.Items.Add(produkty2[i].name + " x" + produkty2[i].count);
                }
                orderBox2_Id = orders.ElementAt(1).id;
                button2.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox2.Items.Add("Brak zamówień");
                button2.Text = "Brak zamówienia";
            }
            
            
            try
            {
                listBox3.Items.Add("Nr zamówienia: " + orders.ElementAt(2).ticketNumber + "\n\r");
                produkty3 = orders.ElementAt(2).products;
                for (int i = 0; i < produkty3.Count(); i++)
                {
                    listBox1.Items.Add(produkty3[i].name + " x" + produkty3[i].count);
                }
                orderBox3_Id = orders.ElementAt(2).id;
                button3.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox3.Items.Add("Brak zamówień");
                button3.Text = "Brak zamówienia";
            }
            
            
            //załadować zamówienie z listy
            //wysłać ukończenie danego order po id
            //put api/Orders/kitchen/{orderId}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            listBox3.Items.Add("Wykonano zamówienie, wczytuję nowe");
            var client = new RestClient("https://localhost:44328/");
            var request_done = new RestRequest("api/Orders/kitchen/" + orderBox3_Id, DataFormat.Json);
            var response_done = client.Put(request_done);

            var request = new RestRequest("api/Orders/kitchen", DataFormat.Json);
            var response = client.Get(request);
            //Console.Write(response.Content);

            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(response.Content);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            List<OrderItems> produkty1, produkty2, produkty3 = new List<OrderItems>();

            try
            {
                listBox1.Items.Add("Nr zamówienia: " + orders.ElementAt(0).ticketNumber + "\n\r");
                produkty1 = orders.ElementAt(0).products;
                for (int i = 0; i < produkty1.Count(); i++)
                {
                    listBox1.Items.Add(produkty1[i].name + " x" + produkty1[i].count);
                }
                orderBox1_Id = orders.ElementAt(0).id;
                button1.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox1.Items.Add("Brak zamówień");
                button1.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox2.Items.Add("Nr zamówienia: " + orders.ElementAt(1).ticketNumber + "\n\r");
                produkty2 = orders.ElementAt(1).products;
                for (int i = 0; i < produkty2.Count(); i++)
                {
                    listBox2.Items.Add(produkty2[i].name + " x" + produkty2[i].count);
                }
                orderBox2_Id = orders.ElementAt(1).id;
                button2.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox2.Items.Add("Brak zamówień");
                button2.Text = "Brak zamówienia";
            }
            

            try
            {
                listBox3.Items.Add("Nr zamówienia: " + orders.ElementAt(2).ticketNumber + "\n\r");
                produkty3 = orders.ElementAt(2).products;
                for (int i = 0; i < produkty3.Count(); i++)
                {
                    listBox1.Items.Add(produkty3[i].name + " x" + produkty3[i].count);
                }
                orderBox3_Id = orders.ElementAt(2).id;
                button3.Text = "Wykonano";
            }
            catch (ArgumentOutOfRangeException f)
            {
                listBox3.Items.Add("Brak zamówień");
                button3.Text = "Brak zamówienia";
            }
            
            //załadować zamówienie z listy
            //wysłać ukończenie danego order po id
            //put api/Orders/kitchen/{orderId}
        }
    }
}
