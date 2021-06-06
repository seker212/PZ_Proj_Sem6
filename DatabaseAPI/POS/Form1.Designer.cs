namespace POSApp {
    partial class Form1 {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent() {
            this.CancelButton = new System.Windows.Forms.Button();
            this.PayButton = new System.Windows.Forms.Button();
            this.OrderTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Test = new System.Windows.Forms.Label();
            this.SumTextBox = new System.Windows.Forms.TextBox();
            this.ProductsListView = new System.Windows.Forms.ListView();
            this.ProductsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProductsPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DiscountList = new System.Windows.Forms.CheckedListBox();
            this.OrderTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.IndianRed;
            this.CancelButton.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CancelButton.Location = new System.Drawing.Point(708, 358);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(80, 80);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Anuluj";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PayButton
            // 
            this.PayButton.BackColor = System.Drawing.Color.LightGreen;
            this.PayButton.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PayButton.Location = new System.Drawing.Point(575, 358);
            this.PayButton.Name = "PayButton";
            this.PayButton.Size = new System.Drawing.Size(127, 80);
            this.PayButton.TabIndex = 2;
            this.PayButton.Text = "Zapłać";
            this.PayButton.UseVisualStyleBackColor = false;
            this.PayButton.Click += new System.EventHandler(this.PayButton_Click);
            // 
            // OrderTableLayoutPanel
            // 
            this.OrderTableLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.OrderTableLayoutPanel.ColumnCount = 6;
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.81633F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.40816F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.40816F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.OrderTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.OrderTableLayoutPanel.Controls.Add(this.label5, 1, 0);
            this.OrderTableLayoutPanel.Controls.Add(this.label6, 2, 0);
            this.OrderTableLayoutPanel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OrderTableLayoutPanel.Location = new System.Drawing.Point(238, 12);
            this.OrderTableLayoutPanel.Name = "OrderTableLayoutPanel";
            this.OrderTableLayoutPanel.RowCount = 1;
            this.OrderTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.OrderTableLayoutPanel.Size = new System.Drawing.Size(550, 340);
            this.OrderTableLayoutPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Produkt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 22);
            this.label5.TabIndex = 12;
            this.label5.Text = "Liczba";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(339, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 22);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cena";
            // 
            // Test
            // 
            this.Test.AutoSize = true;
            this.Test.Location = new System.Drawing.Point(771, 428);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(28, 13);
            this.Test.TabIndex = 4;
            this.Test.Text = "Test";
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // SumTextBox
            // 
            this.SumTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SumTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SumTextBox.Location = new System.Drawing.Point(238, 358);
            this.SumTextBox.Name = "SumTextBox";
            this.SumTextBox.ReadOnly = true;
            this.SumTextBox.Size = new System.Drawing.Size(331, 80);
            this.SumTextBox.TabIndex = 5;
            this.SumTextBox.Text = "0.00 zł";
            this.SumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProductsListView
            // 
            this.ProductsListView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ProductsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProductsName,
            this.ProductsPrice});
            this.ProductsListView.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ProductsListView.FullRowSelect = true;
            this.ProductsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProductsListView.HideSelection = false;
            this.ProductsListView.Location = new System.Drawing.Point(12, 12);
            this.ProductsListView.MultiSelect = false;
            this.ProductsListView.Name = "ProductsListView";
            this.ProductsListView.Size = new System.Drawing.Size(220, 260);
            this.ProductsListView.TabIndex = 6;
            this.ProductsListView.UseCompatibleStateImageBehavior = false;
            this.ProductsListView.View = System.Windows.Forms.View.Details;
            this.ProductsListView.DoubleClick += new System.EventHandler(this.ProductsListView_DoubleClick);
            // 
            // ProductsName
            // 
            this.ProductsName.Text = "Produkt";
            this.ProductsName.Width = 145;
            // 
            // ProductsPrice
            // 
            this.ProductsPrice.Text = "Cena";
            this.ProductsPrice.Width = 70;
            // 
            // DiscountList
            // 
            this.DiscountList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DiscountList.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DiscountList.FormattingEnabled = true;
            this.DiscountList.Location = new System.Drawing.Point(12, 278);
            this.DiscountList.Name = "DiscountList";
            this.DiscountList.Size = new System.Drawing.Size(220, 158);
            this.DiscountList.TabIndex = 8;
            this.DiscountList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DiscountList_ItemCheck);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DiscountList);
            this.Controls.Add(this.ProductsListView);
            this.Controls.Add(this.SumTextBox);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.OrderTableLayoutPanel);
            this.Controls.Add(this.PayButton);
            this.Controls.Add(this.CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "POS";
            this.OrderTableLayoutPanel.ResumeLayout(false);
            this.OrderTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button PayButton;
        private System.Windows.Forms.TableLayoutPanel OrderTableLayoutPanel;
        private System.Windows.Forms.Label Test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SumTextBox;
        private System.Windows.Forms.ListView ProductsListView;
        private System.Windows.Forms.ColumnHeader ProductsName;
        private System.Windows.Forms.ColumnHeader ProductsPrice;
        private System.Windows.Forms.CheckedListBox DiscountList;
    }
}

