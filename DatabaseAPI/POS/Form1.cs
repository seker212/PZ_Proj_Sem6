using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using static System.Windows.Forms.ListViewItem;

namespace POSApp {
    public partial class Form1 : Form {
        NumberFormatInfo nfi = new NumberFormatInfo();
        RestClient client = new RestClient("https://localhost:44328/");
        List<ProductData> products;
        List<Product> orderProducts;
        List<Discount> discounts;
        List<Discount> discountsUsed;

        Dictionary<Tuple<Product, Discount>, double> productsDiscountAmount; // o ile obniżone

        double sum;

        public Form1() {
            InitializeComponent();

            this.nfi.NumberDecimalSeparator = ".";
            this.nfi.NumberDecimalDigits = 2;

            Login();
            Init();
        }

        private void Login() {
            Form login = new Form() {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                CausesValidation = true,
                MaximizeBox = false
            };
            Label loginLabel = new Label() { Left = 10, Top = 20, Text = "Login" };
            TextBox loginBox = new TextBox() { Left = 90, Top = 20, Width = 180 };
            Label nameLabel = new Label() { Left = 10, Top = 50, Text = "Nazwa kasjera" };
            TextBox nameBox = new TextBox() { Left = 90, Top = 50, Width = 180 };
            Label passwordLabel = new Label() { Left = 10, Top = 80, Text = "Hasło" };
            TextBox passwordBox = new TextBox() { Left = 90, Top = 80, Width = 180, PasswordChar='*' };

            Button confirmation = new Button() { Text = "Zaloguj", Left = 100, Width = 100, Top = 120, DialogResult = DialogResult.OK, CausesValidation = true };
            login.Controls.Add(loginBox);
            login.Controls.Add(nameBox);
            login.Controls.Add(passwordBox);
            login.Controls.Add(loginLabel);
            login.Controls.Add(nameLabel);
            login.Controls.Add(passwordLabel);
            login.Controls.Add(confirmation);
            login.AcceptButton = confirmation;

            var dialogResult = login.ShowDialog();
            while (true) {
                if (dialogResult == DialogResult.OK) {
                    if (loginBox.Text != "" && nameBox.Text != "" && passwordBox.Text != "") {
                        var request = new RestRequest("api/User/login/cashier");
                        request.AddParameter("username", loginBox.Text);
                        request.AddParameter("cashierName", nameBox.Text);
                        request.AddParameter("password", passwordBox.Text);
                        var response = client.Get(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            return;
                        else dialogResult = login.ShowDialog();
                    } else {
                        dialogResult = login.ShowDialog();
                    }
                }
            }
        }

        private void Init() {
            var request = new RestRequest("api/Product/available", DataFormat.Json);
            var response = client.Get(request);
            products = JsonConvert.DeserializeObject<List<ProductData>>(response.Content);
            foreach (var item in products) {
                this.ProductsListView.Items.Add(new ListViewItem(new string[] { item.Name, (item.Price).ToString(nfi) }));
            }

            request = new RestRequest("api/Discount/available", DataFormat.Json);
            response = client.Get(request);
            discounts = JsonConvert.DeserializeObject<List<Discount>>(response.Content);
            foreach (var item in discounts) {
                if (item.SetPrice != null) {
                    this.DiscountList.Items.Add(item, CheckState.Indeterminate);
                } else if (item.PriceDropAmmount != null) {
                    this.DiscountList.Items.Add(item, CheckState.Indeterminate);
                } else if (item.PriceDropPercent != null) {
                    this.DiscountList.Items.Add(item, CheckState.Indeterminate);
                }
            }

            //this.client.Authenticator = new HttpBasicAuthenticator("postgres", "mysecretpassword");

            orderProducts = new List<Product>();
            discountsUsed = new List<Discount>();
            productsDiscountAmount = new Dictionary<Tuple<Product, Discount>, double>();
            sum = 0;
        }

        private void ClearAll() {
            var panel = OrderTableLayoutPanel;

            for (int i = 1; i < panel.RowCount; i++) {
                for (int j = 0; j < panel.ColumnCount; j++) {
                    var control = panel.GetControlFromPosition(j, i);
                    panel.Controls.Remove(control);
                }
            }
            panel.RowCount = 1;

            for (int i = 0; i < DiscountList.Items.Count; i++) { 
                DiscountList.SetItemCheckState(i, CheckState.Indeterminate);
            }

            UpdateSumTextBox();
        }

        private void PutOrder() { //TODO cashier id
            var discountOrder = new List<DiscountBasic>();
            foreach (var disc in productsDiscountAmount) {
                if (discountOrder.Any(m => m.Id == disc.Key.Item2.Id)) {
                    discountOrder.SingleOrDefault(m => m.Id == disc.Key.Item2.Id).Count++;
                } else {
                    discountOrder.Add(new DiscountBasic(disc.Key.Item2.Id, 1));
                }
            }

            var order = new OrderPost(new Guid("00000000-0000-0000-0000-000000000007"), this.sum, orderProducts, discountOrder, DateTime.Now);
            string jsonToSend = JsonConvert.SerializeObject(order);
            var request = new RestRequest("api/Orders/kitchen", Method.POST);
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
        }

        private void PayButton_Click(object sender, EventArgs e) {
            if (SumTextBox.Text == "0.00 zł") {
                DialogResult info = MessageBox.Show("Nie możesz złożyć pustego zamówienia!", "Zapłać", MessageBoxButtons.OK);
                return;
            }

            Form prompt = new Form() {
                Width = 325,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Zapłać",
                StartPosition = FormStartPosition.CenterScreen,
                CausesValidation = true,
            };
            Label textLabel = new Label() { Left = 25, Top = 20, Width = 200, Text = "Wybierz sposób płatności:" };
            Button cash = new Button() { Left = 25, Top = 50, Width = 75, Text = "Gotówka", DialogResult = DialogResult.Yes };
            Button card = new Button() { Left = 125, Top = 50, Width = 75, Text = "Karta", DialogResult = DialogResult.No };
            Button cancel = new Button() { Left = 225, Top = 50, Width = 75, Text = "Anuluj", DialogResult = DialogResult.Cancel };
            prompt.Controls.Add(cash);
            prompt.Controls.Add(card);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);

            var dialogResult = prompt.ShowDialog();

            if (dialogResult == DialogResult.Cancel) {
                prompt.Dispose();

            } else if (dialogResult == DialogResult.Yes || dialogResult == DialogResult.No) {
                prompt.Dispose();

                this.PutOrder();

                //string msg = String.Format("Zamówienie nr {0} zostało złożone", "12");
                string msg = "Zamówienie nr zostało złożone";
                DialogResult confirmationresult = MessageBox.Show(msg, "Zapłać", MessageBoxButtons.OK);
                ClearAll();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("Czy chcesz anulować zamówienie?", "Anuluj", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                ClearAll();
            }
        }

        private void UpdateSumTextBox() {
            var panel = OrderTableLayoutPanel;
            double sum = 0;

            for (int i = 1; i < panel.RowCount; i++) {
                var price = panel.GetControlFromPosition(2, i);
                sum += Convert.ToDouble(price.Text, nfi);
            }
            this.sum = sum;
            SumTextBox.Text = string.Format(nfi, "{0:N}", sum) + " zł";
        }

        private bool OrderContains(string text) {
            var panel = OrderTableLayoutPanel;

            for (int i = 1; i < panel.RowCount; i++) {
                if (panel.GetControlFromPosition(0, i).Text == text)
                    return true;
            }
            return false;
        }

        private void ProductsListView_DoubleClick(object sender, EventArgs e) {
            var item = ProductsListView.SelectedItems[0];

            if (!OrderContains(item.Text)) {
                Button delButton = new DeleteButtonClass().DeleteButton;
                delButton.Click += new System.EventHandler(this.DeleteButton_Click);
                Button plusButton = new PlusButtonClass().PlusButton;
                plusButton.Click += new System.EventHandler(this.PlusButton_Click);
                Button minusButton = new MinusButtonClass().MinusButton;
                minusButton.Click += new System.EventHandler(this.MinusButton_Click);

                this.OrderTableLayoutPanel.RowCount += 1;
                this.OrderTableLayoutPanel.Controls.Add(new Label() { Width = 200, Font = new Font("Century Gothic", 14), Text = item.SubItems[0].Text }, 0, this.OrderTableLayoutPanel.RowCount - 1);
                this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = "1" }, 1, this.OrderTableLayoutPanel.RowCount - 1);
                this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = item.SubItems[1].Text }, 2, this.OrderTableLayoutPanel.RowCount - 1);
                this.OrderTableLayoutPanel.Controls.Add(minusButton, 3, this.OrderTableLayoutPanel.RowCount - 1);
                this.OrderTableLayoutPanel.Controls.Add(plusButton, 4, this.OrderTableLayoutPanel.RowCount - 1);
                this.OrderTableLayoutPanel.Controls.Add(delButton, 5, this.OrderTableLayoutPanel.RowCount - 1);

                var product = products.Find(x => x.Name.Equals(item.Text));
                orderProducts.Add(new Product(product.Id, product.Name, 1));

                foreach (var discount in discounts) {
                    foreach (var p in discount.Products) {
                        if (p.Id == product.Id) {
                            int index = DiscountList.Items.IndexOf(discount);
                            DiscountList.SetItemCheckState(index, CheckState.Unchecked);
                        }
                    }
                }

            } else {
                for (int rowIndex = 1; rowIndex < OrderTableLayoutPanel.RowCount; rowIndex++) {
                    if (OrderTableLayoutPanel.GetControlFromPosition(0, rowIndex).Text == item.Text) {
                        var product = products.Find(x => x.Name.Equals(item.Text));
                        var price = product.Price;

                        if (productsDiscountAmount.Keys.Any(m => m.Item1.Id == product.Id)) {
                            for (int i = 0; i < productsDiscountAmount.Count; i++) {
                                var discount = productsDiscountAmount.ElementAt(i);
                                if (discount.Key.Item1.Id == product.Id) {
                                    var disc = discount.Value / (Int32.Parse(OrderTableLayoutPanel.GetControlFromPosition(1, rowIndex).Text));
                                    OrderTableLayoutPanel.GetControlFromPosition(2, rowIndex).Text = (Convert.ToDouble(OrderTableLayoutPanel.GetControlFromPosition(2, rowIndex).Text, nfi) + price - disc).ToString(nfi);
                                    productsDiscountAmount[discount.Key] += disc;
                                    OrderTableLayoutPanel.GetControlFromPosition(1, rowIndex).Text = (Int32.Parse(OrderTableLayoutPanel.GetControlFromPosition(1, rowIndex).Text) + 1).ToString();
                                }
                            }
                        } else {
                            OrderTableLayoutPanel.GetControlFromPosition(1, rowIndex).Text = (Int32.Parse(OrderTableLayoutPanel.GetControlFromPosition(1, rowIndex).Text) + 1).ToString();
                            OrderTableLayoutPanel.GetControlFromPosition(2, rowIndex).Text = (Double.Parse(OrderTableLayoutPanel.GetControlFromPosition(1, rowIndex).Text, nfi) * product.Price).ToString(nfi);
                        }

                        orderProducts.Find(x => x.Name.Equals(item.Text)).Count++;
                    }
                }
            }
            UpdateSumTextBox();
        }

        private void DiscountList_ItemCheck(object sender, ItemCheckEventArgs e) {
            Discount discount = (Discount)DiscountList.SelectedItem;
            if (discount == null)
                return;

            if (e.NewValue == CheckState.Checked && e.CurrentValue == CheckState.Unchecked) {
                discountsUsed.Add(discount);

                if (discount.SetPrice != null) {
                    foreach (Product product in discount.Products) {
                        ApplyDiscount(product, discount, discount.SetPrice);
                    }
                } else if (discount.PriceDropAmmount != null) {
                    foreach (Product product in discount.Products) {
                        ProductData productData = products.Find(x => x.Name.Equals(product.Name));
                        ApplyDiscount(product, discount, productData.Price - discount.PriceDropAmmount);
                    }
                } else if (discount.PriceDropPercent != null) {
                    foreach (Product product in discount.Products) {
                        ProductData productData = products.Find(x => x.Name.Equals(product.Name));
                        ApplyDiscount(product, discount, productData.Price * (discount.PriceDropPercent / 100));
                    }
                }
            } else if (e.NewValue == CheckState.Unchecked && e.CurrentValue == CheckState.Checked) {
                discountsUsed.Remove(discount);
                foreach (Product product in discount.Products) {
                    RemoveDiscount(product, discount);
                }
            }
            UpdateSumTextBox();
        }

        private void RemoveDiscount(Product product, Discount discount) {
            var panel = OrderTableLayoutPanel;
            double value;
            Tuple<Product, Discount> tuple = new Tuple<Product, Discount>(product, discount);
            if (productsDiscountAmount.Keys.Contains(tuple)) {
                value = productsDiscountAmount[tuple];

                for (int i = 1; i < panel.RowCount; i++) {
                    if (OrderTableLayoutPanel.GetControlFromPosition(0, i).Text == product.Name) {
                        double newValue = Convert.ToDouble(OrderTableLayoutPanel.GetControlFromPosition(2, i).Text, nfi) + value;
                        OrderTableLayoutPanel.GetControlFromPosition(2, i).Text = Convert.ToDouble(newValue).ToString(nfi);
                        productsDiscountAmount.Remove(tuple);
                    }
                }
            }
        }

        private void ApplyDiscount(Product product, Discount discount, double? discountAmount) {
            double? newValue;
            var panel = OrderTableLayoutPanel;


            for (int i = 1; i < panel.RowCount; i++) {
                if (OrderTableLayoutPanel.GetControlFromPosition(0, i).Text == product.Name) {
                    var oldPrice = Convert.ToDouble(OrderTableLayoutPanel.GetControlFromPosition(2, i).Text, nfi);
                    int amount = Int32.Parse(OrderTableLayoutPanel.GetControlFromPosition(1, i).Text);
                    if (discountAmount < 0) {
                        newValue = 0;
                        discountAmount = Convert.ToDouble(OrderTableLayoutPanel.GetControlFromPosition(2, i).Text, nfi);
                        productsDiscountAmount.Add(new Tuple<Product, Discount>(product, discount), Convert.ToDouble(discountAmount, nfi));
                    } else {
                        newValue = amount * discountAmount;
                        productsDiscountAmount.Add(new Tuple<Product, Discount>(product, discount), Convert.ToDouble(oldPrice - newValue, nfi));
                    }

                    OrderTableLayoutPanel.GetControlFromPosition(2, i).Text = Convert.ToDouble(newValue).ToString(nfi);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e) {
            var panel = OrderTableLayoutPanel;
            int rowIndex = OrderTableLayoutPanel.GetPositionFromControl((Button)sender).Row;
            Product product = orderProducts.Find(x => x.Name == panel.GetControlFromPosition(0, rowIndex).Text);
            orderProducts.Remove(product);

            for (int i = 0; i < DiscountList.Items.Count; i++) {
                Discount discount = (Discount)DiscountList.Items[i];
                bool containsFlag = false;
                if (discount.Products.Any(m => m.Id == product.Id)) {
                    foreach (var pr in discount.Products) {
                        if (orderProducts.Any(m => m.Id == pr.Id)) {
                            containsFlag = true;
                            break;
                        }
                    }

                    if (containsFlag) {
                        var tuple = productsDiscountAmount.FirstOrDefault(x => x.Key.Item1.Id == product.Id && x.Key.Item2.Id == discount.Id);
                        if (tuple.Key != null)
                            productsDiscountAmount.Remove(tuple.Key);

                    } else {
                        DiscountList.SetItemCheckState(i, CheckState.Indeterminate);
                        var tuple = productsDiscountAmount.FirstOrDefault(x => x.Key.Item1.Id == product.Id && x.Key.Item2.Id == discount.Id);
                        if (tuple.Key != null)
                            productsDiscountAmount.Remove(tuple.Key);
                    }
                }
            }

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++) {
                var control = panel.GetControlFromPosition(i, rowIndex);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = rowIndex + 1; i < panel.RowCount; i++) {
                for (int j = 0; j < panel.ColumnCount; j++) {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null) {
                        panel.SetRow(control, i - 1);
                    }
                }
            }

            var removeStyle = panel.RowCount - 1;

            if (panel.RowStyles.Count > removeStyle)
                panel.RowStyles.RemoveAt(removeStyle);

            panel.RowCount--;
            UpdateSumTextBox();
        }

        private void MinusButton_Click(object sender, EventArgs e) {
            var panel = OrderTableLayoutPanel;
            int rowIndex = OrderTableLayoutPanel.GetPositionFromControl((Button)sender).Row;
            var value = Int32.Parse(panel.GetControlFromPosition(1, rowIndex).Text);
            var product = products.Find(x => x.Name.Equals(panel.GetControlFromPosition(0, rowIndex).Text));
            var price = product.Price;
            if (value > 1) {
                if (Convert.ToDouble(panel.GetControlFromPosition(2, rowIndex).Text, nfi) < price)
                    panel.GetControlFromPosition(2, rowIndex).Text = "0.00";
                else {
                    if (productsDiscountAmount.Keys.Any(m => m.Item1.Id == product.Id)) {
                        for (int i = 0; i < productsDiscountAmount.Count; i++) {
                            var discount = productsDiscountAmount.ElementAt(i);
                            if (discount.Key.Item1.Id == product.Id) {
                                var disc = discount.Value / (Int32.Parse(panel.GetControlFromPosition(1, rowIndex).Text));
                                panel.GetControlFromPosition(2, rowIndex).Text = ((Convert.ToDouble(panel.GetControlFromPosition(2, rowIndex).Text, nfi)) - (price - disc)).ToString(nfi);
                                productsDiscountAmount[discount.Key] -= disc;
                            }
                        }
                    } else {
                        panel.GetControlFromPosition(2, rowIndex).Text = (Convert.ToDouble(panel.GetControlFromPosition(2, rowIndex).Text, nfi) - price).ToString(nfi);
                    }
                }
                panel.GetControlFromPosition(1, rowIndex).Text = (value - 1).ToString();
                orderProducts.Find(x => x.Name.Equals(product.Name)).Count--;
                UpdateSumTextBox();
            }
        }

        private void PlusButton_Click(object sender, EventArgs e) {
            var panel = OrderTableLayoutPanel;
            int rowIndex = OrderTableLayoutPanel.GetPositionFromControl((Button)sender).Row;
            var value = Int32.Parse(panel.GetControlFromPosition(1, rowIndex).Text);
            var product = products.Find(x => x.Name.Equals(panel.GetControlFromPosition(0, rowIndex).Text));
            var price = product.Price;

            if (productsDiscountAmount.Keys.Any(m => m.Item1.Id == product.Id)) {
                for (int i = 0; i < productsDiscountAmount.Count; i++) {
                    var discount = productsDiscountAmount.ElementAt(i);
                    if (discount.Key.Item1.Id == product.Id) {
                        var disc = discount.Value / (Int32.Parse(panel.GetControlFromPosition(1, rowIndex).Text));
                        panel.GetControlFromPosition(2, rowIndex).Text = (Convert.ToDouble(panel.GetControlFromPosition(2, rowIndex).Text, nfi) + price - disc).ToString(nfi);
                        productsDiscountAmount[discount.Key] += disc;
                    }
                }
            } else {
                panel.GetControlFromPosition(2, rowIndex).Text = (Convert.ToDouble(panel.GetControlFromPosition(2, rowIndex).Text, nfi) + price).ToString(nfi);
            }
            panel.GetControlFromPosition(1, rowIndex).Text = (value + 1).ToString();
            orderProducts.Find(x => x.Name.Equals(product.Name)).Count++;
            UpdateSumTextBox();
        }

        private void Test_Click(object sender, EventArgs e) {

        }
    }

    class DeleteButtonClass {
        public Button DeleteButton;
        public DeleteButtonClass() {
            this.DeleteButton = new Button();
            this.DeleteButton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.TabIndex = 14;
            this.DeleteButton.Text = "✕";
            this.DeleteButton.TextAlign = ContentAlignment.MiddleCenter;
            this.DeleteButton.UseVisualStyleBackColor = false;
        }
    }
    class MinusButtonClass {
        public Button MinusButton;
        public MinusButtonClass() {
            this.MinusButton = new Button();
            this.MinusButton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MinusButton.Name = "PlusButton";
            this.MinusButton.TabIndex = 14;
            this.MinusButton.Text = "−";
            this.MinusButton.TextAlign = ContentAlignment.MiddleCenter;
            this.MinusButton.UseVisualStyleBackColor = false;
        }
    }
    class PlusButtonClass {
        public Button PlusButton;
        public PlusButtonClass() {
            this.PlusButton = new Button();
            this.PlusButton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlusButton.Name = "PlusButton";
            this.PlusButton.TabIndex = 14;
            this.PlusButton.Text = "+";
            this.PlusButton.TextAlign = ContentAlignment.MiddleCenter;
            this.PlusButton.UseVisualStyleBackColor = false;
        }
    }
}