using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RestSharp;
using RestSharp.Authenticators;

namespace KitchenApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var client = new RestClient("https://localhost:44328/");
            //client.Authenticator = new HttpBasicAuthenticator("postgres", "mysecretpassword");

            //get daje full liste zamówień
            //brać liste i odświeżać co jakiś czas
            //ładować pierwsze 3 i potem oddawać wykonanie konkretnych
            //get api/Orders/kitchen

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Wykonano zamówienie, wczytuję nowe");
            //załadować zamówienie z listy
            //wysłać ukończenie danego order po id
            //put api/Orders/kitchen/{orderId}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("Wykonano zamówienie, wczytuję nowe");
            //załadować zamówienie z listy
            //wysłać ukończenie danego order po id
            //put api/Orders/kitchen/{orderId}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            listBox3.Items.Add("Wykonano zamówienie, wczytuję nowe");
            //załadować zamówienie z listy
            //wysłać ukończenie danego order po id
            //put api/Orders/kitchen/{orderId}
        }
    }
}
