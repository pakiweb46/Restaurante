namespace Restaurante
{
    partial class chooseView
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSchließen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(68, 18);
            this.listView1.Margin = new System.Windows.Forms.Padding(5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(279, 495);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 273;
            // 
            // button1
            // 
            this.btnSchließen.Location = new System.Drawing.Point(68, 534);
            this.btnSchließen.Margin = new System.Windows.Forms.Padding(5);
            this.btnSchließen.Name = "button1";
            this.btnSchließen.Size = new System.Drawing.Size(287, 57);
            this.btnSchließen.TabIndex = 1;
            this.btnSchließen.Text = "Schließen";
            this.btnSchließen.UseVisualStyleBackColor = true;
            this.btnSchließen.Click += new System.EventHandler(this.btnSchließen_Click);
            // 
            // chooseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 609);
            this.Controls.Add(this.btnSchließen);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "chooseView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitte von der Liste wählen";
            this.Load += new System.EventHandler(this.chooseView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnSchließen;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}