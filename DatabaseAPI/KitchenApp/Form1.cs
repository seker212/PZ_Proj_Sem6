﻿using System;
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
            //var client = new RestClient("https://api.twitter.com/1.1");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Wykonano zamówienie, wczytuję nowe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("Wykonano zamówienie, wczytuję nowe");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            listBox3.Items.Add("Wykonano zamówienie, wczytuję nowe");
        }
    }
}
