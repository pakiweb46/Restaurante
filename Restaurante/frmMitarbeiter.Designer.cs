namespace Restaurante
{
    partial class frmMitarbeiter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMitarbeiter));
            this.btnHauptmenu = new System.Windows.Forms.Button();
            this.btnLoeschen = new System.Windows.Forms.Button();
            this.btnSpeichern = new System.Windows.Forms.Button();
            this.txtOrt = new System.Windows.Forms.TextBox();
            this.txtPLZ = new System.Windows.Forms.TextBox();
            this.txtzusatz = new System.Windows.Forms.TextBox();
            this.txtStrNo = new System.Windows.Forms.TextBox();
            this.tbStraße = new System.Windows.Forms.TextBox();
            this.txtMitarbeiterName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTatigkeit = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNextRecord = new System.Windows.Forms.Button();
            this.btnLastRecord = new System.Windows.Forms.Button();
            this.btnPreviousRecord = new System.Windows.Forms.Button();
            this.btnFirstRecord = new System.Windows.Forms.Button();
            this.chkAdminRight = new System.Windows.Forms.CheckBox();
            this.btnSuchen = new System.Windows.Forms.Button();
            this.btnEmptyField = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.btnHauptmenu.Location = new System.Drawing.Point(616, 407);
            this.btnHauptmenu.Name = "button4";
            this.btnHauptmenu.Size = new System.Drawing.Size(184, 58);
            this.btnHauptmenu.TabIndex = 31;
            this.btnHauptmenu.Text = "Hauptmenü (Esc)";
            this.btnHauptmenu.UseVisualStyleBackColor = true;
            this.btnHauptmenu.Click += new System.EventHandler(this.btnHauptmenu_Click);
            // 
            // button3
            // 
            this.btnLoeschen.Location = new System.Drawing.Point(426, 407);
            this.btnLoeschen.Name = "button3";
            this.btnLoeschen.Size = new System.Drawing.Size(184, 58);
            this.btnLoeschen.TabIndex = 30;
            this.btnLoeschen.Text = "Löschen (F7)";
            this.btnLoeschen.UseVisualStyleBackColor = true;
            this.btnLoeschen.Click += new System.EventHandler(this.btnLoeschen_Click);
            // 
            // button1
            // 
            this.btnSpeichern.Location = new System.Drawing.Point(43, 407);
            this.btnSpeichern.Name = "button1";
            this.btnSpeichern.Size = new System.Drawing.Size(184, 58);
            this.btnSpeichern.TabIndex = 28;
            this.btnSpeichern.Text = "Speichern (F4)";
            this.btnSpeichern.UseVisualStyleBackColor = true;
            this.btnSpeichern.Click += new System.EventHandler(this.btnSpeichern_Click);
            // 
            // textBox8
            // 
            this.txtOrt.Location = new System.Drawing.Point(268, 200);
            this.txtOrt.Name = "textBox8";
            this.txtOrt.Size = new System.Drawing.Size(152, 26);
            this.txtOrt.TabIndex = 27;
            // 
            // textBox7
            // 
            this.txtPLZ.Location = new System.Drawing.Point(162, 200);
            this.txtPLZ.Name = "textBox7";
            this.txtPLZ.Size = new System.Drawing.Size(100, 26);
            this.txtPLZ.TabIndex = 26;
            // 
            // textBox6
            // 
            this.txtzusatz.Location = new System.Drawing.Point(162, 148);
            this.txtzusatz.Name = "textBox6";
            this.txtzusatz.Size = new System.Drawing.Size(258, 26);
            this.txtzusatz.TabIndex = 25;
            // 
            // textBox5
            // 
            this.txtStrNo.Location = new System.Drawing.Point(436, 87);
            this.txtStrNo.Name = "textBox5";
            this.txtStrNo.Size = new System.Drawing.Size(74, 26);
            this.txtStrNo.TabIndex = 24;
            // 
            // tbStraße
            // 
            this.tbStraße.Location = new System.Drawing.Point(162, 87);
            this.tbStraße.Name = "tbStraße";
            this.tbStraße.Size = new System.Drawing.Size(258, 26);
            this.tbStraße.TabIndex = 23;
            // 
            // textBox3
            // 
            this.txtMitarbeiterName.BackColor = System.Drawing.SystemColors.Window;
            this.txtMitarbeiterName.Location = new System.Drawing.Point(162, 38);
            this.txtMitarbeiterName.Name = "textBox3";
            this.txtMitarbeiterName.Size = new System.Drawing.Size(258, 26);
            this.txtMitarbeiterName.TabIndex = 22;
            this.txtMitarbeiterName.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Name :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Straße / Nr :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Zusatz :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "PLZ / Ort:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Tätigkeit :";
            // 
            // comboBox1
            // 
            this.cmbTatigkeit.FormattingEnabled = true;
            this.cmbTatigkeit.Location = new System.Drawing.Point(162, 244);
            this.cmbTatigkeit.Name = "comboBox1";
            this.cmbTatigkeit.Size = new System.Drawing.Size(203, 28);
            this.cmbTatigkeit.TabIndex = 33;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(616, 21);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(304, 272);
            this.listView1.TabIndex = 34;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 235;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Id";
            this.columnHeader2.Width = 65;
            // 
            // button8
            // 
            this.btnNextRecord.Location = new System.Drawing.Point(644, 471);
            this.btnNextRecord.Name = "button8";
            this.btnNextRecord.Size = new System.Drawing.Size(75, 64);
            this.btnNextRecord.TabIndex = 38;
            this.btnNextRecord.Text = ">>";
            this.btnNextRecord.UseVisualStyleBackColor = true;
            this.btnNextRecord.Click += new System.EventHandler(this.btnNextRecord_Click);
            // 
            // button7
            // 
            this.btnLastRecord.Location = new System.Drawing.Point(725, 471);
            this.btnLastRecord.Name = "button7";
            this.btnLastRecord.Size = new System.Drawing.Size(75, 64);
            this.btnLastRecord.TabIndex = 37;
            this.btnLastRecord.Text = ">>|";
            this.btnLastRecord.UseVisualStyleBackColor = true;
            this.btnLastRecord.Click += new System.EventHandler(this.btnLastRecord_Click);
            // 
            // button6
            // 
            this.btnPreviousRecord.Location = new System.Drawing.Point(124, 471);
            this.btnPreviousRecord.Name = "button6";
            this.btnPreviousRecord.Size = new System.Drawing.Size(75, 64);
            this.btnPreviousRecord.TabIndex = 36;
            this.btnPreviousRecord.Text = "<<";
            this.btnPreviousRecord.UseVisualStyleBackColor = true;
            this.btnPreviousRecord.Click += new System.EventHandler(this.btnPreviousRecord_Click);
            // 
            // button5
            // 
            this.btnFirstRecord.Location = new System.Drawing.Point(43, 471);
            this.btnFirstRecord.Name = "button5";
            this.btnFirstRecord.Size = new System.Drawing.Size(75, 64);
            this.btnFirstRecord.TabIndex = 35;
            this.btnFirstRecord.Text = "|<<";
            this.btnFirstRecord.UseVisualStyleBackColor = true;
            this.btnFirstRecord.Click += new System.EventHandler(this.btnFirstRecord_Click);
            // 
            // checkBox1
            // 
            this.chkAdminRight.AutoSize = true;
            this.chkAdminRight.Location = new System.Drawing.Point(27, 301);
            this.chkAdminRight.Name = "checkBox1";
            this.chkAdminRight.Size = new System.Drawing.Size(199, 24);
            this.chkAdminRight.TabIndex = 39;
            this.chkAdminRight.Text = " Aministrative Rechte";
            this.chkAdminRight.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.btnSuchen.Location = new System.Drawing.Point(236, 407);
            this.btnSuchen.Name = "button2";
            this.btnSuchen.Size = new System.Drawing.Size(184, 58);
            this.btnSuchen.TabIndex = 29;
            this.btnSuchen.Text = "Suchen (F1)";
            this.btnSuchen.UseVisualStyleBackColor = true;
            this.btnSuchen.Click += new System.EventHandler(this.btnSuchen_Click);
            // 
            // button9
            // 
            this.btnEmptyField.Location = new System.Drawing.Point(302, 471);
            this.btnEmptyField.Name = "button9";
            this.btnEmptyField.Size = new System.Drawing.Size(179, 59);
            this.btnEmptyField.TabIndex = 40;
            this.btnEmptyField.Text = "Felder leeren (F5)";
            this.btnEmptyField.UseVisualStyleBackColor = true;
            this.btnEmptyField.Click += new System.EventHandler(this.btnEmptyField_Click);
            // 
            // frmMitarbeiter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(941, 549);
            this.Controls.Add(this.btnEmptyField);
            this.Controls.Add(this.chkAdminRight);
            this.Controls.Add(this.btnNextRecord);
            this.Controls.Add(this.btnLastRecord);
            this.Controls.Add(this.btnPreviousRecord);
            this.Controls.Add(this.btnFirstRecord);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cmbTatigkeit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHauptmenu);
            this.Controls.Add(this.btnLoeschen);
            this.Controls.Add(this.btnSuchen);
            this.Controls.Add(this.btnSpeichern);
            this.Controls.Add(this.txtOrt);
            this.Controls.Add(this.txtPLZ);
            this.Controls.Add(this.txtzusatz);
            this.Controls.Add(this.txtStrNo);
            this.Controls.Add(this.tbStraße);
            this.Controls.Add(this.txtMitarbeiterName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmMitarbeiter";
            this.Text = "Mitarbeiter Verwaltung";
            this.Load += new System.EventHandler(this.frmMitarbeiter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHauptmenu;
        private System.Windows.Forms.Button btnLoeschen;
        private System.Windows.Forms.Button btnSpeichern;
        private System.Windows.Forms.TextBox txtOrt;
        private System.Windows.Forms.TextBox txtPLZ;
        private System.Windows.Forms.TextBox txtzusatz;
        private System.Windows.Forms.TextBox txtStrNo;
        private System.Windows.Forms.TextBox tbStraße;
        private System.Windows.Forms.TextBox txtMitarbeiterName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTatigkeit;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnNextRecord;
        private System.Windows.Forms.Button btnLastRecord;
        private System.Windows.Forms.Button btnPreviousRecord;
        private System.Windows.Forms.Button btnFirstRecord;
        private System.Windows.Forms.CheckBox chkAdminRight;
        private System.Windows.Forms.Button btnSuchen;
        private System.Windows.Forms.Button btnEmptyField;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}