namespace Restaurante
{
    partial class frmSpeiseKarte
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbArtikel = new System.Windows.Forms.TextBox();
            this.tbBezeichnung = new System.Windows.Forms.TextBox();
            this.tbZusatz = new System.Windows.Forms.TextBox();
            this.tbVerkaufPreis = new System.Windows.Forms.TextBox();
            this.tbMwSt = new System.Windows.Forms.TextBox();
            this.btnSpeichern = new System.Windows.Forms.Button();
            this.btnÄndern = new System.Windows.Forms.Button();
            this.btnLöschen = new System.Windows.Forms.Button();
            this.btnZuruck = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.lvArtikel = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button9 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pfandBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Artikel Nummer: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Artikelbezeichnung :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Zusatz :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Verkaufpreis  :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "MwSt % :";
            // 
            // tbArtikel
            // 
            this.tbArtikel.Location = new System.Drawing.Point(210, 69);
            this.tbArtikel.Name = "tbArtikel";
            this.tbArtikel.Size = new System.Drawing.Size(85, 30);
            this.tbArtikel.TabIndex = 5;
            // 
            // tbBezeichnung
            // 
            this.tbBezeichnung.Location = new System.Drawing.Point(210, 127);
            this.tbBezeichnung.Name = "tbBezeichnung";
            this.tbBezeichnung.Size = new System.Drawing.Size(239, 30);
            this.tbBezeichnung.TabIndex = 6;
            // 
            // tbZusatz
            // 
            this.tbZusatz.Location = new System.Drawing.Point(210, 188);
            this.tbZusatz.Name = "tbZusatz";
            this.tbZusatz.Size = new System.Drawing.Size(239, 30);
            this.tbZusatz.TabIndex = 7;
            // 
            // tbVerkaufPreis
            // 
            this.tbVerkaufPreis.Location = new System.Drawing.Point(210, 243);
            this.tbVerkaufPreis.Name = "tbVerkaufPreis";
            this.tbVerkaufPreis.Size = new System.Drawing.Size(62, 30);
            this.tbVerkaufPreis.TabIndex = 8;
            // 
            // tbMwSt
            // 
            this.tbMwSt.Location = new System.Drawing.Point(210, 304);
            this.tbMwSt.Name = "tbMwSt";
            this.tbMwSt.Size = new System.Drawing.Size(62, 30);
            this.tbMwSt.TabIndex = 9;
            // 
            // btnSpeichern
            // 
            this.btnSpeichern.Location = new System.Drawing.Point(39, 405);
            this.btnSpeichern.Name = "btnSpeichern";
            this.btnSpeichern.Size = new System.Drawing.Size(137, 50);
            this.btnSpeichern.TabIndex = 10;
            this.btnSpeichern.Text = "Speichern (F4)";
            this.btnSpeichern.UseVisualStyleBackColor = true;
            this.btnSpeichern.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnÄndern
            // 
            this.btnÄndern.Location = new System.Drawing.Point(182, 405);
            this.btnÄndern.Name = "btnÄndern";
            this.btnÄndern.Size = new System.Drawing.Size(136, 50);
            this.btnÄndern.TabIndex = 11;
            this.btnÄndern.Text = "Ändern (F6)";
            this.btnÄndern.UseVisualStyleBackColor = true;
            this.btnÄndern.Click += new System.EventHandler(this.btnÄndern_Click);
            // 
            // btnLöschen
            // 
            this.btnLöschen.Location = new System.Drawing.Point(324, 405);
            this.btnLöschen.Name = "btnLöschen";
            this.btnLöschen.Size = new System.Drawing.Size(125, 50);
            this.btnLöschen.TabIndex = 12;
            this.btnLöschen.Text = "Löschen (F7)";
            this.btnLöschen.UseVisualStyleBackColor = true;
            this.btnLöschen.Click += new System.EventHandler(this.btnLöschen_Click);
            // 
            // btnZuruck
            // 
            this.btnZuruck.Location = new System.Drawing.Point(455, 405);
            this.btnZuruck.Name = "btnZuruck";
            this.btnZuruck.Size = new System.Drawing.Size(130, 50);
            this.btnZuruck.TabIndex = 13;
            this.btnZuruck.Text = "Hauptmenü (Esc)";
            this.btnZuruck.UseVisualStyleBackColor = true;
            this.btnZuruck.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(39, 479);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 64);
            this.button5.TabIndex = 14;
            this.button5.Text = "|<<";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(120, 479);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 64);
            this.button6.TabIndex = 15;
            this.button6.Text = "<<";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(510, 479);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 64);
            this.button7.TabIndex = 16;
            this.button7.Text = ">>|";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(429, 479);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 64);
            this.button8.TabIndex = 17;
            this.button8.Text = ">>";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // lvArtikel
            // 
            this.lvArtikel.BackColor = System.Drawing.Color.White;
            this.lvArtikel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvArtikel.FullRowSelect = true;
            this.lvArtikel.GridLines = true;
            this.lvArtikel.Location = new System.Drawing.Point(480, 30);
            this.lvArtikel.Name = "lvArtikel";
            this.lvArtikel.Size = new System.Drawing.Size(396, 341);
            this.lvArtikel.TabIndex = 18;
            this.lvArtikel.UseCompatibleStateImageBehavior = false;
            this.lvArtikel.View = System.Windows.Forms.View.Details;
            this.lvArtikel.SelectedIndexChanged += new System.EventHandler(this.lvArtikel_SelectedIndexChanged);
            this.lvArtikel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvArtikel_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Artikel Nr";
            this.columnHeader1.Width = 97;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 207;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Preis";
            this.columnHeader3.Width = 88;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(237, 479);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(163, 64);
            this.button9.TabIndex = 19;
            this.button9.Text = "Felder leeren (F5)";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(681, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 70);
            this.button1.TabIndex = 20;
            this.button1.Text = "Zutaten";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 353);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "Pfand:";
            // 
            // pfandBox
            // 
            this.pfandBox.FormattingEnabled = true;
            this.pfandBox.Location = new System.Drawing.Point(210, 352);
            this.pfandBox.Name = "pfandBox";
            this.pfandBox.Size = new System.Drawing.Size(121, 33);
            this.pfandBox.TabIndex = 23;
            // 
            // frmSpeiseKarte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(888, 559);
            this.Controls.Add(this.pfandBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.lvArtikel);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnZuruck);
            this.Controls.Add(this.btnLöschen);
            this.Controls.Add(this.btnÄndern);
            this.Controls.Add(this.btnSpeichern);
            this.Controls.Add(this.tbMwSt);
            this.Controls.Add(this.tbVerkaufPreis);
            this.Controls.Add(this.tbZusatz);
            this.Controls.Add(this.tbBezeichnung);
            this.Controls.Add(this.tbArtikel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmSpeiseKarte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Speise Karte Verwalten";
            this.Load += new System.EventHandler(this.frmSpeiseKarte_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbArtikel;
        private System.Windows.Forms.TextBox tbBezeichnung;
        private System.Windows.Forms.TextBox tbZusatz;
        private System.Windows.Forms.TextBox tbVerkaufPreis;
        private System.Windows.Forms.TextBox tbMwSt;
        private System.Windows.Forms.Button btnSpeichern;
        private System.Windows.Forms.Button btnÄndern;
        private System.Windows.Forms.Button btnLöschen;
        private System.Windows.Forms.Button btnZuruck;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListView lvArtikel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox pfandBox;
    }
}