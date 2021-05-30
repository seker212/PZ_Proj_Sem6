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
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Produkt1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Produkt2");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Kategoria1", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Produkt1");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Produkt2");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Produkt3");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Kategoria2", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Produkt1");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Produkt2");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Kategoria3", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19});
            this.ProductsTreeView = new System.Windows.Forms.TreeView();
            this.CancelButton = new System.Windows.Forms.Button();
            this.PayButton = new System.Windows.Forms.Button();
            this.OrderTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Test = new System.Windows.Forms.Label();
            this.SumTextBox = new System.Windows.Forms.TextBox();
            this.OrderTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProductsTreeView
            // 
            this.ProductsTreeView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ProductsTreeView.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ProductsTreeView.Location = new System.Drawing.Point(12, 12);
            this.ProductsTreeView.Name = "ProductsTreeView";
            treeNode11.Name = "Węzeł3";
            treeNode11.Text = "Produkt1";
            treeNode12.Name = "Węzeł4";
            treeNode12.Text = "Produkt2";
            treeNode13.Name = "Węzeł0";
            treeNode13.Text = "Kategoria1";
            treeNode14.Name = "Węzeł5";
            treeNode14.Text = "Produkt1";
            treeNode15.Name = "Węzeł6";
            treeNode15.Text = "Produkt2";
            treeNode16.Name = "Węzeł7";
            treeNode16.Text = "Produkt3";
            treeNode17.Name = "Węzeł1";
            treeNode17.Text = "Kategoria2";
            treeNode18.Name = "Węzeł8";
            treeNode18.Text = "Produkt1";
            treeNode19.Name = "Węzeł9";
            treeNode19.Text = "Produkt2";
            treeNode20.Name = "Węzeł2";
            treeNode20.Text = "Kategoria3";
            this.ProductsTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode17,
            treeNode20});
            this.ProductsTreeView.Size = new System.Drawing.Size(220, 426);
            this.ProductsTreeView.TabIndex = 0;
            this.ProductsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProductsTreeView_NodeMouseDoubleClick);
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
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.OrderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
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
            this.label5.Location = new System.Drawing.Point(223, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 22);
            this.label5.TabIndex = 12;
            this.label5.Text = "Liczba";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 0);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SumTextBox);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.OrderTableLayoutPanel);
            this.Controls.Add(this.PayButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ProductsTreeView);
            this.Name = "Form1";
            this.Text = "POS";
            this.OrderTableLayoutPanel.ResumeLayout(false);
            this.OrderTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ProductsTreeView;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button PayButton;
        private System.Windows.Forms.TableLayoutPanel OrderTableLayoutPanel;
        private System.Windows.Forms.Label Test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SumTextBox;
    }
}

