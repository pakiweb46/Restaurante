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
            this.btnSpeichern = new System.Windows.Forms.Button();
            this.btnSuchen = new System.Windows.Forms.Button();
            this.btnLoeschen = new System.Windows.Forms.Button();
            this.btnHauptMenu = new System.Windows.Forms.Button();
            this.btnNextRecord = new System.Windows.Forms.Button();
            this.btnLastRecord = new System.Windows.Forms.Button();
            this.btnPreviousRecord = new System.Windows.Forms.Button();
            this.btnFirstRecord = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEmpty = new System.Windows.Forms.Button();
            this.btnAendern = new System.Windows.Forms.Button();
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
            this.btnSpeichern.Location = new System.Drawing.Point(200, 402);
            this.btnSpeichern.Name = "button1";
            this.btnSpeichern.Size = new System.Drawing.Size(148, 58);
            this.btnSpeichern.TabIndex = 14;
            this.btnSpeichern.Text = "Speichern (F4)";
            this.btnSpeichern.UseVisualStyleBackColor = true;
            this.btnSpeichern.Click += new System.EventHandler(this.btnSPeichern_Click);
            // 
            // button2
            // 
            this.btnSuchen.Location = new System.Drawing.Point(41, 402);
            this.btnSuchen.Name = "button2";
            this.btnSuchen.Size = new System.Drawing.Size(153, 58);
            this.btnSuchen.TabIndex = 15;
            this.btnSuchen.Text = "Suchen (F1)";
            this.btnSuchen.UseVisualStyleBackColor = true;
            this.btnSuchen.Click += new System.EventHandler(this.btnSuchen_Click);
            // 
            // button3
            // 
            this.btnLoeschen.Location = new System.Drawing.Point(497, 402);
            this.btnLoeschen.Name = "button3";
            this.btnLoeschen.Size = new System.Drawing.Size(136, 58);
            this.btnLoeschen.TabIndex = 16;
            this.btnLoeschen.Text = "Löschen(F7)";
            this.btnLoeschen.UseVisualStyleBackColor = true;
            this.btnLoeschen.Click += new System.EventHandler(this.btnLoeschen_Click);
            // 
            // button4
            // 
            this.btnHauptMenu.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnHauptMenu.Location = new System.Drawing.Point(639, 402);
            this.btnHauptMenu.Name = "button4";
            this.btnHauptMenu.Size = new System.Drawing.Size(156, 58);
            this.btnHauptMenu.TabIndex = 17;
            this.btnHauptMenu.Text = "Hauptmenü (Esc)";
            this.btnHauptMenu.UseVisualStyleBackColor = true;
            this.btnHauptMenu.Click += new System.EventHandler(this.btnHauptMenu_Click);
            // 
            // button8
            // 
            this.btnNextRecord.Location = new System.Drawing.Point(639, 484);
            this.btnNextRecord.Name = "button8";
            this.btnNextRecord.Size = new System.Drawing.Size(75, 64);
            this.btnNextRecord.TabIndex = 21;
            this.btnNextRecord.Text = ">>";
            this.btnNextRecord.UseVisualStyleBackColor = true;
            this.btnNextRecord.Click += new System.EventHandler(this.btnNextRecord_Click);
            // 
            // button7
            // 
            this.btnLastRecord.Location = new System.Drawing.Point(720, 484);
            this.btnLastRecord.Name = "button7";
            this.btnLastRecord.Size = new System.Drawing.Size(75, 64);
            this.btnLastRecord.TabIndex = 20;
            this.btnLastRecord.Text = ">>|";
            this.btnLastRecord.UseVisualStyleBackColor = true;
            this.btnLastRecord.Click += new System.EventHandler(this.btnLastRecord_Click);
            // 
            // button6
            // 
            this.btnPreviousRecord.Location = new System.Drawing.Point(119, 484);
            this.btnPreviousRecord.Name = "button6";
            this.btnPreviousRecord.Size = new System.Drawing.Size(75, 64);
            this.btnPreviousRecord.TabIndex = 19;
            this.btnPreviousRecord.Text = "<<";
            this.btnPreviousRecord.UseVisualStyleBackColor = true;
            this.btnPreviousRecord.Click += new System.EventHandler(this.btnPreviousRecord_Click);
            // 
            // button5
            // 
            this.btnFirstRecord.Location = new System.Drawing.Point(38, 484);
            this.btnFirstRecord.Name = "button5";
            this.btnFirstRecord.Size = new System.Drawing.Size(75, 64);
            this.btnFirstRecord.TabIndex = 18;
            this.btnFirstRecord.Text = "|<<";
            this.btnFirstRecord.UseVisualStyleBackColor = true;
            this.btnFirstRecord.Click += new System.EventHandler(this.btnFirstRecord_Click);
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
            this.btnEmpty.Location = new System.Drawing.Point(323, 482);
            this.btnEmpty.Name = "button9";
            this.btnEmpty.Size = new System.Drawing.Size(168, 66);
            this.btnEmpty.TabIndex = 23;
            this.btnEmpty.Text = "Felder Leeren (F5)";
            this.btnEmpty.UseVisualStyleBackColor = true;
            this.btnEmpty.Click += new System.EventHandler(this.btnEmpty_Click);
            // 
            // button10
            // 
            this.btnAendern.Location = new System.Drawing.Point(356, 402);
            this.btnAendern.Name = "button10";
            this.btnAendern.Size = new System.Drawing.Size(135, 58);
            this.btnAendern.TabIndex = 25;
            this.btnAendern.Text = "Ändern (F6)";
            this.btnAendern.UseVisualStyleBackColor = true;
            this.btnAendern.Click += new System.EventHandler(this.btnAendern_Click);
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
            this.Controls.Add(this.btnAendern);
            this.Controls.Add(this.btnEmpty);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnNextRecord);
            this.Controls.Add(this.btnLastRecord);
            this.Controls.Add(this.btnPreviousRecord);
            this.Controls.Add(this.btnFirstRecord);
            this.Controls.Add(this.btnHauptMenu);
            this.Controls.Add(this.btnLoeschen);
            this.Controls.Add(this.btnSuchen);
            this.Controls.Add(this.btnSpeichern);
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
        private System.Windows.Forms.Button btnSpeichern;
        private System.Windows.Forms.Button btnSuchen;
        private System.Windows.Forms.Button btnLoeschen;
        private System.Windows.Forms.Button btnHauptMenu;
        private System.Windows.Forms.Button btnNextRecord;
        private System.Windows.Forms.Button btnLastRecord;
        private System.Windows.Forms.Button btnPreviousRecord;
        private System.Windows.Forms.Button btnFirstRecord;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.Button btnAendern;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbAnfahrt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbRabatt;
        
       
    }
}