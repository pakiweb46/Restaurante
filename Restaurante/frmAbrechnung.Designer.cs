namespace Restaurante
{
    partial class frmAbrechnung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbrechnung));
            this.btnStorno = new System.Windows.Forms.Button();
            this.btnJahresAbrechnung = new System.Windows.Forms.Button();
            this.btnDrucken = new System.Windows.Forms.Button();
            this.btnMonatAbrechnung = new System.Windows.Forms.Button();
            this.btnAbbruch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTagessicht = new System.Windows.Forms.Button();
            this.btnUbersicht = new System.Windows.Forms.Button();
            this.btnBerichte = new System.Windows.Forms.Button();
            this.btnFAbrechnung = new System.Windows.Forms.Button();
            this.listView1 = new Restaurante.PrintableListView2();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fahrer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Nr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.btnStorno.Location = new System.Drawing.Point(845, 344);
            this.btnStorno.Name = "button1";
            this.btnStorno.Size = new System.Drawing.Size(147, 51);
            this.btnStorno.TabIndex = 1;
            this.btnStorno.Text = "Storno(F7)";
            this.btnStorno.UseVisualStyleBackColor = true;
            this.btnStorno.Click += new System.EventHandler(this.btnStorno_Click);
            // 
            // button2
            // 
            this.btnJahresAbrechnung.Location = new System.Drawing.Point(845, 506);
            this.btnJahresAbrechnung.Name = "button2";
            this.btnJahresAbrechnung.Size = new System.Drawing.Size(147, 51);
            this.btnJahresAbrechnung.TabIndex = 2;
            this.btnJahresAbrechnung.Text = "Jahres Abbrechnung";
            this.btnJahresAbrechnung.UseVisualStyleBackColor = true;
            this.btnJahresAbrechnung.Click += new System.EventHandler(this.btnJahresAbrechnung_Click);
            // 
            // button3
            // 
            this.btnDrucken.Location = new System.Drawing.Point(849, 20);
            this.btnDrucken.Name = "button3";
            this.btnDrucken.Size = new System.Drawing.Size(147, 51);
            this.btnDrucken.TabIndex = 3;
            this.btnDrucken.Text = "Drucken (F3)";
            this.btnDrucken.UseVisualStyleBackColor = true;
            this.btnDrucken.Click += new System.EventHandler(this.btnDrucken_Click);
            // 
            // button4
            // 
            this.btnMonatAbrechnung.Location = new System.Drawing.Point(845, 425);
            this.btnMonatAbrechnung.Name = "button4";
            this.btnMonatAbrechnung.Size = new System.Drawing.Size(147, 51);
            this.btnMonatAbrechnung.TabIndex = 4;
            this.btnMonatAbrechnung.Text = "Monats Abbrechnung";
            this.btnMonatAbrechnung.UseVisualStyleBackColor = true;
            this.btnMonatAbrechnung.Click += new System.EventHandler(this.btnMonatAbrechnung_Click);
            // 
            // button5
            // 
            this.btnAbbruch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbbruch.Location = new System.Drawing.Point(845, 587);
            this.btnAbbruch.Name = "button5";
            this.btnAbbruch.Size = new System.Drawing.Size(147, 51);
            this.btnAbbruch.TabIndex = 5;
            this.btnAbbruch.Text = "Abbruch (F4)";
            this.btnAbbruch.UseVisualStyleBackColor = true;
            this.btnAbbruch.Click += new System.EventHandler(this.btnAbbruch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 642);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gesamt Umsatz :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(629, 636);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 26);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tages Abbrechnungs Datum :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(302, 15);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(260, 26);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.TextChanged += new System.EventHandler(this.dateTimePicker1_TextChanged);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(129, 700);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(410, 29);
            this.label3.TabIndex = 13;
            this.label3.Text = "Doppelt Klick um Details Bestellung details zu sehen.";
            this.label3.UseCompatibleTextRendering = true;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button6
            // 
            this.btnTagessicht.Location = new System.Drawing.Point(849, 182);
            this.btnTagessicht.Name = "button6";
            this.btnTagessicht.Size = new System.Drawing.Size(147, 51);
            this.btnTagessicht.TabIndex = 14;
            this.btnTagessicht.Text = "Tagesübersicht";
            this.btnTagessicht.UseVisualStyleBackColor = true;
            this.btnTagessicht.Click += new System.EventHandler(this.btnTagessicht_Click);
            // 
            // button7
            // 
            this.btnUbersicht.Location = new System.Drawing.Point(849, 101);
            this.btnUbersicht.Name = "button7";
            this.btnUbersicht.Size = new System.Drawing.Size(147, 51);
            this.btnUbersicht.TabIndex = 15;
            this.btnUbersicht.Text = "Übersicht";
            this.btnUbersicht.UseVisualStyleBackColor = true;
            this.btnUbersicht.Click += new System.EventHandler(this.btnUbersicht_Click);
            // 
            // button8
            // 
            this.btnBerichte.Location = new System.Drawing.Point(849, 263);
            this.btnBerichte.Name = "button8";
            this.btnBerichte.Size = new System.Drawing.Size(147, 51);
            this.btnBerichte.TabIndex = 16;
            this.btnBerichte.Text = "Berichte";
            this.btnBerichte.UseVisualStyleBackColor = true;
            this.btnBerichte.Click += new System.EventHandler(this.btnBerichte_Click);
            // 
            // button9
            // 
            this.btnFAbrechnung.Location = new System.Drawing.Point(849, 655);
            this.btnFAbrechnung.Name = "button9";
            this.btnFAbrechnung.Size = new System.Drawing.Size(147, 51);
            this.btnFAbrechnung.TabIndex = 17;
            this.btnFAbrechnung.Text = "Fahrer Abbrechnung";
            this.btnFAbrechnung.UseVisualStyleBackColor = true;
            this.btnFAbrechnung.Click += new System.EventHandler(this.btnFAbrechnung_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.Fahrer,
            this.Nr});
            this.listView1.FitToPage = false;
            this.listView1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(2, 70);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(824, 557);
            this.listView1.summesatz = "";
            this.listView1.TabIndex = 0;
            this.listView1.Title = "";
            this.listView1.Title2 = "";
            this.listView1.Title3 = "";
            this.listView1.TitleFont = new System.Drawing.Font("Arial", 12F);
            this.listView1.TitleFont2 = new System.Drawing.Font("Arial", 12F);
            this.listView1.TitleFont3 = new System.Drawing.Font("Arial", 12F);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged_1);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.LostFocus += new System.EventHandler(this.listView1_LostFocus);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 7;
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Telefon";
            this.columnHeader2.Width = 132;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "KNr - K.Name - Addresse";
            this.columnHeader3.Width = 267;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Datum";
            this.columnHeader4.Width = 112;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Zeit";
            this.columnHeader5.Width = 82;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Betrag";
            this.columnHeader6.Width = 86;
            // 
            // Fahrer
            // 
            this.Fahrer.Text = "Fahrer";
            this.Fahrer.Width = 108;
            // 
            // Nr
            // 
            this.Nr.DisplayIndex = 0;
            this.Nr.Text = "Nr";
            this.Nr.Width = 46;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 642);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Anzahl der Fahrten : ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(165, 639);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 19;
            // 
            // frmAbrechnung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFAbrechnung);
            this.Controls.Add(this.btnBerichte);
            this.Controls.Add(this.btnUbersicht);
            this.Controls.Add(this.btnTagessicht);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAbbruch);
            this.Controls.Add(this.btnMonatAbrechnung);
            this.Controls.Add(this.btnDrucken);
            this.Controls.Add(this.btnJahresAbrechnung);
            this.Controls.Add(this.btnStorno);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmAbrechnung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tages Abbrechnung";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAbrechnung_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PrintableListView2 listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnStorno;
        private System.Windows.Forms.Button btnJahresAbrechnung;
        private System.Windows.Forms.Button btnDrucken;
        private System.Windows.Forms.Button btnMonatAbrechnung;
        private System.Windows.Forms.Button btnAbbruch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        //private System.EventHandler listView1_SelectedIndexChanged;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader Fahrer;
        private System.Windows.Forms.Button btnTagessicht;
        private System.Windows.Forms.Button btnUbersicht;
        private System.Windows.Forms.Button btnBerichte;
        private System.Windows.Forms.Button btnFAbrechnung;
        private System.Windows.Forms.ColumnHeader Nr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
    }
}