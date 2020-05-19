namespace Restaurante
{
    partial class frmZutaten
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
            this.lvArtikel = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEmpty = new System.Windows.Forms.Button();
            this.btnNextRecord = new System.Windows.Forms.Button();
            this.btnLastRecord = new System.Windows.Forms.Button();
            this.btnPreviousRecord = new System.Windows.Forms.Button();
            this.btnFirstRecord = new System.Windows.Forms.Button();
            this.btnZuruck = new System.Windows.Forms.Button();
            this.btnLöschen = new System.Windows.Forms.Button();
            this.btnÄndern = new System.Windows.Forms.Button();
            this.btnSpeichern = new System.Windows.Forms.Button();
            this.tbMwSt = new System.Windows.Forms.TextBox();
            this.tbVerkaufPreis = new System.Windows.Forms.TextBox();
            this.tbBezeichnung = new System.Windows.Forms.TextBox();
            this.tbArtikel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.lvArtikel.Location = new System.Drawing.Point(564, 14);
            this.lvArtikel.Margin = new System.Windows.Forms.Padding(5);
            this.lvArtikel.Name = "lvArtikel";
            this.lvArtikel.Size = new System.Drawing.Size(396, 432);
            this.lvArtikel.TabIndex = 19;
            this.lvArtikel.UseCompatibleStateImageBehavior = false;
            this.lvArtikel.View = System.Windows.Forms.View.Details;
            this.lvArtikel.SelectedIndexChanged += new System.EventHandler(this.lvArtikel_SelectedIndexChanged);
            this.lvArtikel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvArtikel_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
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
            this.btnEmpty.Location = new System.Drawing.Point(224, 541);
            this.btnEmpty.Name = "button9";
            this.btnEmpty.Size = new System.Drawing.Size(163, 64);
            this.btnEmpty.TabIndex = 28;
            this.btnEmpty.Text = "Felder leeren (F5)";
            this.btnEmpty.UseVisualStyleBackColor = true;
            this.btnEmpty.Click += new System.EventHandler(this.btnEmpty_Click);
            // 
            // button8
            // 
            this.btnNextRecord.Location = new System.Drawing.Point(416, 541);
            this.btnNextRecord.Name = "button8";
            this.btnNextRecord.Size = new System.Drawing.Size(75, 64);
            this.btnNextRecord.TabIndex = 27;
            this.btnNextRecord.Text = ">>";
            this.btnNextRecord.UseVisualStyleBackColor = true;
            this.btnNextRecord.Click += new System.EventHandler(this.btnNextRecord_Click);
            // 
            // button7
            // 
            this.btnLastRecord.Location = new System.Drawing.Point(497, 541);
            this.btnLastRecord.Name = "button7";
            this.btnLastRecord.Size = new System.Drawing.Size(75, 64);
            this.btnLastRecord.TabIndex = 26;
            this.btnLastRecord.Text = ">>|";
            this.btnLastRecord.UseVisualStyleBackColor = true;
            this.btnLastRecord.Click += new System.EventHandler(this.btnLastRecord_Click);
            // 
            // button6
            // 
            this.btnPreviousRecord.Location = new System.Drawing.Point(107, 541);
            this.btnPreviousRecord.Name = "button6";
            this.btnPreviousRecord.Size = new System.Drawing.Size(75, 64);
            this.btnPreviousRecord.TabIndex = 25;
            this.btnPreviousRecord.Text = "<<";
            this.btnPreviousRecord.UseVisualStyleBackColor = true;
            this.btnPreviousRecord.Click += new System.EventHandler(this.btnPreviousRecord_Click);
            // 
            // button5
            // 
            this.btnFirstRecord.Location = new System.Drawing.Point(26, 541);
            this.btnFirstRecord.Name = "button5";
            this.btnFirstRecord.Size = new System.Drawing.Size(75, 64);
            this.btnFirstRecord.TabIndex = 24;
            this.btnFirstRecord.Text = "|<<";
            this.btnFirstRecord.UseVisualStyleBackColor = true;
            this.btnFirstRecord.Click += new System.EventHandler(this.btnFirstRecord_Click);
            // 
            // btnZuruck
            // 
            this.btnZuruck.Location = new System.Drawing.Point(442, 467);
            this.btnZuruck.Name = "btnZuruck";
            this.btnZuruck.Size = new System.Drawing.Size(130, 50);
            this.btnZuruck.TabIndex = 23;
            this.btnZuruck.Text = "Hauptmenü (Esc)";
            this.btnZuruck.UseVisualStyleBackColor = true;
            this.btnZuruck.Click += new System.EventHandler(this.btnZuruck_Click);
            // 
            // btnLöschen
            // 
            this.btnLöschen.Location = new System.Drawing.Point(311, 467);
            this.btnLöschen.Name = "btnLöschen";
            this.btnLöschen.Size = new System.Drawing.Size(125, 50);
            this.btnLöschen.TabIndex = 22;
            this.btnLöschen.Text = "Löschen (F7)";
            this.btnLöschen.UseVisualStyleBackColor = true;
            this.btnLöschen.Click += new System.EventHandler(this.btnLöschen_Click);
            // 
            // btnÄndern
            // 
            this.btnÄndern.Location = new System.Drawing.Point(169, 467);
            this.btnÄndern.Name = "btnÄndern";
            this.btnÄndern.Size = new System.Drawing.Size(136, 50);
            this.btnÄndern.TabIndex = 21;
            this.btnÄndern.Text = "Ändern (F6)";
            this.btnÄndern.UseVisualStyleBackColor = true;
            this.btnÄndern.Click += new System.EventHandler(this.btnÄndern_Click);
            // 
            // btnSpeichern
            // 
            this.btnSpeichern.Location = new System.Drawing.Point(26, 467);
            this.btnSpeichern.Name = "btnSpeichern";
            this.btnSpeichern.Size = new System.Drawing.Size(137, 50);
            this.btnSpeichern.TabIndex = 20;
            this.btnSpeichern.Text = "Speichern (F4)";
            this.btnSpeichern.UseVisualStyleBackColor = true;
            this.btnSpeichern.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbMwSt
            // 
            this.tbMwSt.Location = new System.Drawing.Point(224, 275);
            this.tbMwSt.Name = "tbMwSt";
            this.tbMwSt.Size = new System.Drawing.Size(62, 26);
            this.tbMwSt.TabIndex = 29;
            // 
            // tbVerkaufPreis
            // 
            this.tbVerkaufPreis.Location = new System.Drawing.Point(224, 198);
            this.tbVerkaufPreis.Name = "tbVerkaufPreis";
            this.tbVerkaufPreis.Size = new System.Drawing.Size(62, 26);
            this.tbVerkaufPreis.TabIndex = 32;
            // 
            // tbBezeichnung
            // 
            this.tbBezeichnung.Location = new System.Drawing.Point(224, 138);
            this.tbBezeichnung.Name = "tbBezeichnung";
            this.tbBezeichnung.Size = new System.Drawing.Size(239, 26);
            this.tbBezeichnung.TabIndex = 31;
            // 
            // tbArtikel
            // 
            this.tbArtikel.Location = new System.Drawing.Point(224, 77);
            this.tbArtikel.Name = "tbArtikel";
            this.tbArtikel.Size = new System.Drawing.Size(85, 26);
            this.tbArtikel.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "MwSt % :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 20);
            this.label4.TabIndex = 35;
            this.label4.Text = "Verkaufpreis  :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "Zutatbezeichnung:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "ID Nummer: ";
            // 
            // frmZutaten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(974, 623);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbVerkaufPreis);
            this.Controls.Add(this.tbBezeichnung);
            this.Controls.Add(this.tbArtikel);
            this.Controls.Add(this.tbMwSt);
            this.Controls.Add(this.btnEmpty);
            this.Controls.Add(this.btnNextRecord);
            this.Controls.Add(this.btnLastRecord);
            this.Controls.Add(this.btnPreviousRecord);
            this.Controls.Add(this.btnFirstRecord);
            this.Controls.Add(this.btnZuruck);
            this.Controls.Add(this.btnLöschen);
            this.Controls.Add(this.btnÄndern);
            this.Controls.Add(this.btnSpeichern);
            this.Controls.Add(this.lvArtikel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmZutaten";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmZutaten";
            this.Load += new System.EventHandler(this.frmZutaten_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvArtikel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.Button btnNextRecord;
        private System.Windows.Forms.Button btnLastRecord;
        private System.Windows.Forms.Button btnPreviousRecord;
        private System.Windows.Forms.Button btnFirstRecord;
        private System.Windows.Forms.Button btnZuruck;
        private System.Windows.Forms.Button btnLöschen;
        private System.Windows.Forms.Button btnÄndern;
        private System.Windows.Forms.Button btnSpeichern;
        private System.Windows.Forms.TextBox tbMwSt;
        private System.Windows.Forms.TextBox tbVerkaufPreis;
        private System.Windows.Forms.TextBox tbBezeichnung;
        private System.Windows.Forms.TextBox tbArtikel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}