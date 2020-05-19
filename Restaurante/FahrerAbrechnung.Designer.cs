namespace Restaurante
{
    partial class FahrerAbrechnung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FahrerAbrechnung));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDrucken = new System.Windows.Forms.Button();
            this.btnzuruck = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Anfangzeit :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(95, 14);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(182, 24);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(467, 13);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(170, 24);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "@";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(316, 13);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(78, 24);
            this.textBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Endzeit";
            // 
            // button1
            // 
            this.btnDrucken.Location = new System.Drawing.Point(658, 10);
            this.btnDrucken.Margin = new System.Windows.Forms.Padding(4);
            this.btnDrucken.Name = "button1";
            this.btnDrucken.Size = new System.Drawing.Size(152, 32);
            this.btnDrucken.TabIndex = 8;
            this.btnDrucken.Text = "Übersicht Drucken";
            this.btnDrucken.UseVisualStyleBackColor = true;
            this.btnDrucken.Click += new System.EventHandler(this.btnDrucken_Click);
            // 
            // button2
            // 
            this.btnzuruck.Location = new System.Drawing.Point(836, 10);
            this.btnzuruck.Margin = new System.Windows.Forms.Padding(4);
            this.btnzuruck.Name = "button2";
            this.btnzuruck.Size = new System.Drawing.Size(106, 32);
            this.btnzuruck.TabIndex = 9;
            this.btnzuruck.Text = "zurück";
            this.btnzuruck.UseVisualStyleBackColor = true;
            this.btnzuruck.Click += new System.EventHandler(this.btnzuruck_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Anzahl Der Fahrten :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(151, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 24);
            this.textBox2.TabIndex = 11;
            // 
            // FahrerAbrechnung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(956, 121);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnzuruck);
            this.Controls.Add(this.btnDrucken);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FahrerAbrechnung";
            this.Text = "FahrerAbrechnung";
            this.Load += new System.EventHandler(this.FahrerAbrechnung_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDrucken;
        private System.Windows.Forms.Button btnzuruck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
    }
}