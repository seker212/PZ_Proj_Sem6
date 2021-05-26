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

        private void PayButton_Click(object sender, EventArgs e) {
            // todo
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            // todo
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

                    this.OrderTableLayoutPanel.RowCount += 1;
                    this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = ((TreeView)sender).SelectedNode.Text }, 0, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = "5" }, 1, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(new Label() { Font = new Font("Century Gothic", 14), Text = "7" }, 2, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(new Button() { Font = new Font("Century Gothic", 9), Text = "e" }, 3, this.OrderTableLayoutPanel.RowCount - 1);
                    this.OrderTableLayoutPanel.Controls.Add(delButton, 4, this.OrderTableLayoutPanel.RowCount - 1);

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
}

// todo:
// quantity of products
// fill up the left products panel
// is edit button necessary?
