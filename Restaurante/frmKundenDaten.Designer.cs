namespace Restaurante
{
    partial class frmKundenDaten
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKundenDaten));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbKundenNr = new System.Windows.Forms.TextBox();
            this.tbAnrede = new System.Windows.Forms.TextBox();
            this.tbKundenName = new System.Windows.Forms.TextBox();
            this.tbStraße = new System.Windows.Forms.TextBox();
            this.tbStrNo = new System.Windows.Forms.TextBox();
            this.tbZusatz = new System.Windows.Forms.TextBox();
            this.tbPLZ = new System.Windows.Forms.TextBox();
            this.tbOrt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbAnfahrt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbRabatt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Telefon:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLZ / Ort:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Zusatz :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Straße / Nr :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Kunden Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Anrede :";
            // 
            // tbKundenNr
            // 
            this.tbKundenNr.Location = new System.Drawing.Point(173, 23);
            this.tbKundenNr.Name = "tbKundenNr";
            this.tbKundenNr.Size = new System.Drawing.Size(161, 26);
            this.tbKundenNr.TabIndex = 6;
            this.tbKundenNr.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbKundenNr.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // tbAnrede
            // 
            this.tbAnrede.Location = new System.Drawing.Point(173, 81);
            this.tbAnrede.Name = "tbAnrede";
            this.tbAnrede.Size = new System.Drawing.Size(161, 26);
            this.tbAnrede.TabIndex = 7;
            this.tbAnrede.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbAnrede.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // tbKundenName
            // 
            this.tbKundenName.Location = new System.Drawing.Point(173, 139);
            this.tbKundenName.Name = "tbKundenName";
            this.tbKundenName.Size = new System.Drawing.Size(258, 26);
            this.tbKundenName.TabIndex = 8;
            this.tbKundenName.TextChanged += new System.EventHandler(this.tbKundenName_TextChanged);
            this.tbKundenName.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbKundenName.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // tbStraße
            // 
            this.tbStraße.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbStraße.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbStraße.Location = new System.Drawing.Point(173, 188);
            this.tbStraße.Name = "tbStraße";
            this.tbStraße.Size = new System.Drawing.Size(258, 26);
            this.tbStraße.TabIndex = 9;
            this.tbStraße.TextChanged += new System.EventHandler(this.tbStraße_TextChanged);
            this.tbStraße.GotFocus += new System.EventHandler(this.tbStraße_GotFocus);
            this.tbStraße.LostFocus += new System.EventHandler(this.tbStraße_LostFocus);
            // 
            // tbStrNo
            // 
            this.tbStrNo.Location = new System.Drawing.Point(447, 188);
            this.tbStrNo.Name = "tbStrNo";
            this.tbStrNo.Size = new System.Drawing.Size(74, 26);
            this.tbStrNo.TabIndex = 10;
            this.tbStrNo.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbStrNo.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // tbZusatz
            // 
            this.tbZusatz.Location = new System.Drawing.Point(173, 249);
            this.tbZusatz.Name = "tbZusatz";
            this.tbZusatz.Size = new System.Drawing.Size(258, 26);
            this.tbZusatz.TabIndex = 11;
            this.tbZusatz.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbZusatz.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // tbPLZ
            // 
            this.tbPLZ.Location = new System.Drawing.Point(173, 301);
            this.tbPLZ.Name = "tbPLZ";
            this.tbPLZ.Size = new System.Drawing.Size(100, 26);
            this.tbPLZ.TabIndex = 12;
            this.tbPLZ.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbPLZ.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // tbOrt
            // 
            this.tbOrt.Location = new System.Drawing.Point(279, 301);
            this.tbOrt.Name = "tbOrt";
            this.tbOrt.Size = new System.Drawing.Size(152, 26);
            this.tbOrt.TabIndex = 13;
            this.tbOrt.GotFocus += new System.EventHandler(this.text_GotFocus);
            this.tbOrt.LostFocus += new System.EventHandler(this.text_LostFocus);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 58);
            this.button1.TabIndex = 14;
            this.button1.Text = "Speichern (F4)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 402);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 58);
            this.button2.TabIndex = 15;
            this.button2.Text = "Suchen (F1)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(497, 402);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 58);
            this.button3.TabIndex = 16;
            this.button3.Text = "Löschen(F7)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button4.Location = new System.Drawing.Point(639, 402);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(156, 58);
            this.button4.TabIndex = 17;
            this.button4.Text = "Hauptmenü (Esc)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(639, 484);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 64);
            this.button8.TabIndex = 21;
            this.button8.Text = ">>";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(720, 484);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 64);
            this.button7.TabIndex = 20;
            this.button7.Text = ">>|";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(119, 484);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 64);
            this.button6.TabIndex = 19;
            this.button6.Text = "<<";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(38, 484);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 64);
            this.button5.TabIndex = 18;
            this.button5.Text = "|<<";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(527, 29);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(305, 261);
            this.listView1.TabIndex = 22;
            this.listView1.TabStop = false;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Visible = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "KundenNr";
            this.columnHeader1.Width = 153;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 159;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Id";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(323, 482);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(168, 66);
            this.button9.TabIndex = 23;
            this.button9.Text = "Felder Leeren (F5)";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(356, 402);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(135, 58);
            this.button10.TabIndex = 25;
            this.button10.Text = "Ändern (F6)";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 353);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "Anfahrtkosten:";
            // 
            // tbAnfahrt
            // 
            this.tbAnfahrt.Location = new System.Drawing.Point(173, 353);
            this.tbAnfahrt.Name = "tbAnfahrt";
            this.tbAnfahrt.Size = new System.Drawing.Size(100, 26);
            this.tbAnfahrt.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(291, 356);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 28;
            this.label8.Text = "Rabatt: ";
            // 
            // tbRabatt
            // 
            this.tbRabatt.Location = new System.Drawing.Point(371, 353);
            this.tbRabatt.Name = "tbRabatt";
            this.tbRabatt.Size = new System.Drawing.Size(100, 26);
            this.tbRabatt.TabIndex = 29;
            // 
            // frmKundenDaten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(838, 565);
            this.Controls.Add(this.tbRabatt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbAnfahrt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbOrt);
            this.Controls.Add(this.tbPLZ);
            this.Controls.Add(this.tbZusatz);
            this.Controls.Add(this.tbStrNo);
            this.Controls.Add(this.tbStraße);
            this.Controls.Add(this.tbKundenName);
            this.Controls.Add(this.tbAnrede);
            this.Controls.Add(this.tbKundenNr);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmKundenDaten";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kuden Daten Verwalten";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmKundenDaten_FormClosing);
            this.Load += new System.EventHandler(this.frmKundenDaten_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbKundenNr;
        private System.Windows.Forms.TextBox tbAnrede;
        private System.Windows.Forms.TextBox tbKundenName;
        private System.Windows.Forms.TextBox tbStraße;
        private System.Windows.Forms.TextBox tbStrNo;
        private System.Windows.Forms.TextBox tbZusatz;
        private System.Windows.Forms.TextBox tbPLZ;
        private System.Windows.Forms.TextBox tbOrt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbAnfahrt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbRabatt;
        
       
    }
}