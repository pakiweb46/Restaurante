namespace Restaurante
{
    partial class dateSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dateSelection));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDrucken = new System.Windows.Forms.Button();
            this.btnzuruck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(80, 43);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(427, 42);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Von";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bis";
            // 
            // button2
            // 
            this.btnDrucken.Location = new System.Drawing.Point(259, 107);
            this.btnDrucken.Name = "button2";
            this.btnDrucken.Size = new System.Drawing.Size(114, 49);
            this.btnDrucken.TabIndex = 5;
            this.btnDrucken.Text = "Drucken";
            this.btnDrucken.UseVisualStyleBackColor = true;
            this.btnDrucken.Click += new System.EventHandler(this.btnDrucken_Click);
            // 
            // button1
            // 
            this.btnzuruck.Location = new System.Drawing.Point(379, 107);
            this.btnzuruck.Name = "button1";
            this.btnzuruck.Size = new System.Drawing.Size(106, 49);
            this.btnzuruck.TabIndex = 6;
            this.btnzuruck.Text = "Zurück";
            this.btnzuruck.UseVisualStyleBackColor = true;
            this.btnzuruck.Click += new System.EventHandler(this.btnZuruck_Click);
            // 
            // dateSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(694, 168);
            this.Controls.Add(this.btnzuruck);
            this.Controls.Add(this.btnDrucken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dateSelection";
            this.Text = "Bitte Wählen sie Anfang und End datum";
            this.Load += new System.EventHandler(this.dateSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDrucken;
        private System.Windows.Forms.Button btnzuruck;
    }
}