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

namespace POSApp {


    public partial class Form1 : Form {
        NumberFormatInfo nfi = new NumberFormatInfo();

        public Form1() {
            InitializeComponent();

            this.nfi.NumberDecimalSeparator = ".";
            this.nfi.NumberDecimalDigits = 2;
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
            UpdateSumTextBox();
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
            Label textLabel = new Label() { Left = 25, Top = 20, Text = "Wybierz sposób płatności:" };
            Button cash = new Button() { Left = 25, Top = 50, Width = 75, Text = "Gotówka", DialogResult = DialogResult.Yes};
            Button card = new Button() { Left = 125, Top = 50, Width = 75, Text = "Karta", DialogResult = DialogResult.No};
            Button cancel = new Button() { Left = 225, Top = 50, Width = 75, Text = "Anuluj", DialogResult = DialogResult.Cancel};
            prompt.Controls.Add(cash);
            prompt.Controls.Add(card);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);

            var dialogResult = prompt.ShowDialog();

            if (dialogResult == DialogResult.Cancel) {
                prompt.Dispose();
                
            } else if (dialogResult == DialogResult.Yes || dialogResult == DialogResult.No) {
                prompt.Dispose();

                // add to DB

                string msg = String.Format("Zamówienie nr {0} zostało złożone","12");
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
                var amount = panel.GetControlFromPosition(1, i);
                var price = panel.GetControlFromPosition(2, i);
                sum += Convert.ToDouble(amount.Text, nfi) * Convert.ToDouble(price.Text, nfi);
            }
            SumTextBox.Text = string.Format(nfi, "{0:N}", sum)+" zł";
        }

        private void ProductsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (sender is TreeView) {
                if (((TreeView)sender).SelectedNode.LastNode == null) {
                    Button delButton = new DeleteButtonClass().DeleteButton;
                    delButton.Click += new System.EventHandler(this.DeleteButton_Click);
                    Button plusButton = new PlusButtonClass().PlusButton;
                    plusButton.Click += new System.EventHandler(this.PlusButton_Click);
                    Button minusButton = new MinusButtonClass().MinusButton;
                    minusButton.Click += new System.EventHandler(this.MinusButton_Click);

                    this.OrderTableLayoutPanel.RowCount += 1;
                    this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = ((TreeView)sender).SelectedNode.Text }, 0, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = "1" }, 1, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = "7" }, 2, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(minusButton, 3, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(plusButton, 4, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(delButton, 5, this.OrderTableLayoutPanel.RowCount - 1);

                    UpdateSumTextBox();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e) {
            var panel = OrderTableLayoutPanel;
            int rowIndex = OrderTableLayoutPanel.GetPositionFromControl((Button)sender).Row;

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
            if (value > 1) {
                panel.GetControlFromPosition(1, rowIndex).Text = (value - 1).ToString();
                UpdateSumTextBox();
            }
        }
        private void PlusButton_Click(object sender, EventArgs e) {
            var panel = OrderTableLayoutPanel;
            int rowIndex = OrderTableLayoutPanel.GetPositionFromControl((Button)sender).Row;
            var value = Int32.Parse(panel.GetControlFromPosition(1, rowIndex).Text);
            panel.GetControlFromPosition(1, rowIndex).Text = (value+1).ToString();
            UpdateSumTextBox();
        }
        
    }

    class DeleteButtonClass {
        public Button DeleteButton;
        public DeleteButtonClass() {
            this.DeleteButton = new Button();
            this.DeleteButton.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.TabIndex = 14;
            this.DeleteButton.Text = "d";
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
            this.MinusButton.Text = "-";
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
            this.PlusButton.UseVisualStyleBackColor = false;
        }
    }
}

// todo:

// fill up the left products panel

