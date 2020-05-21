namespace Restaurante
{
    partial class frmBestellungDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBestellungDetails));
            this.tbAnfahrtkosten = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTotalMwst = new System.Windows.Forms.TextBox();
            this.tbGesamt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnDrucken = new System.Windows.Forms.Button();
            this.btnZuruck = new System.Windows.Forms.Button();
            this.lvBestellDetail = new Restaurante.PrintableListView2();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // tbAnfahrtkosten
            // 
            this.tbAnfahrtkosten.Location = new System.Drawing.Point(712, 471);
            this.tbAnfahrtkosten.Name = "tbAnfahrtkosten";
            this.tbAnfahrtkosten.Size = new System.Drawing.Size(128, 20);
            this.tbAnfahrtkosten.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(595, 469);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Anfahrtkosten:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(595, 506);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Mwst:";
            // 
            // tbTotalMwst
            // 
            this.tbTotalMwst.Location = new System.Drawing.Point(713, 500);
            this.tbTotalMwst.Name = "tbTotalMwst";
            this.tbTotalMwst.Size = new System.Drawing.Size(127, 20);
            this.tbTotalMwst.TabIndex = 31;
            // 
            // tbGesamt
            // 
            this.tbGesamt.Location = new System.Drawing.Point(713, 529);
            this.tbGesamt.Margin = new System.Windows.Forms.Padding(4);
            this.tbGesamt.Name = "tbGesamt";
            this.tbGesamt.Size = new System.Drawing.Size(127, 20);
            this.tbGesamt.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(595, 532);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Gesamt Betrag:";
            // 
            // btnDrucken
            // 
            this.btnDrucken.Location = new System.Drawing.Point(103, 492);
            this.btnDrucken.Margin = new System.Windows.Forms.Padding(4);
            this.btnDrucken.Name = "btnDrucken";
            this.btnDrucken.Size = new System.Drawing.Size(176, 34);
            this.btnDrucken.TabIndex = 35;
            this.btnDrucken.Text = "Drucken ";
            this.btnDrucken.UseVisualStyleBackColor = true;
            this.btnDrucken.Click += new System.EventHandler(this.btnDrucken_Click);
            // 
            // btnZuruck
            // 
            this.btnZuruck.Location = new System.Drawing.Point(298, 492);
            this.btnZuruck.Margin = new System.Windows.Forms.Padding(4);
            this.btnZuruck.Name = "btnZuruck";
            this.btnZuruck.Size = new System.Drawing.Size(176, 34);
            this.btnZuruck.TabIndex = 36;
            this.btnZuruck.Text = "Zurück ";
            this.btnZuruck.UseVisualStyleBackColor = true;
            this.btnZuruck.Click += new System.EventHandler(this.btnZuruck_Click);
            // 
            // lvBestellDetail
            // 
            this.lvBestellDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader12});
            this.lvBestellDetail.FitToPage = false;
            this.lvBestellDetail.FullRowSelect = true;
            this.lvBestellDetail.GridLines = true;
            this.lvBestellDetail.HideSelection = false;
            this.lvBestellDetail.Location = new System.Drawing.Point(21, 25);
            this.lvBestellDetail.Margin = new System.Windows.Forms.Padding(4);
            this.lvBestellDetail.Name = "lvBestellDetail";
            this.lvBestellDetail.Size = new System.Drawing.Size(890, 430);
            this.lvBestellDetail.summesatz = "";
            this.lvBestellDetail.TabIndex = 9;
            this.lvBestellDetail.TabStop = false;
            this.lvBestellDetail.Title = "";
            this.lvBestellDetail.Title2 = "";
            this.lvBestellDetail.Title3 = "";
            this.lvBestellDetail.TitleFont = new System.Drawing.Font("Arial", 12F);
            this.lvBestellDetail.TitleFont2 = new System.Drawing.Font("Arial", 12F);
            this.lvBestellDetail.TitleFont3 = new System.Drawing.Font("Arial", 12F);
            this.lvBestellDetail.UseCompatibleStateImageBehavior = false;
            this.lvBestellDetail.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Artikel Nr";
            this.columnHeader1.Width = 123;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Bezeichnung";
            this.columnHeader2.Width = 384;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Menge";
            this.columnHeader3.Width = 86;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Einzel Preis";
            this.columnHeader4.Width = 98;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Preis";
            this.columnHeader5.Width = 121;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "MwSt";
            this.columnHeader12.Width = 67;
            // 
            // frmBestellungDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1044, 630);
            this.Controls.Add(this.btnZuruck);
            this.Controls.Add(this.btnDrucken);
            this.Controls.Add(this.tbAnfahrtkosten);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTotalMwst);
            this.Controls.Add(this.tbGesamt);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lvBestellDetail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBestellungDetails";
            this.Text = "Bestellung Detailiert";
            this.Load += new System.EventHandler(this.frmBestellungDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PrintableListView2 lvBestellDetail;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.TextBox tbAnfahrtkosten;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTotalMwst;
        private System.Windows.Forms.TextBox tbGesamt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnDrucken;
        private System.Windows.Forms.Button btnZuruck;
    }
}