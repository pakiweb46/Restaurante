namespace Restaurante
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnKundenDaten = new System.Windows.Forms.Button();
            this.btnSpeiseKarte = new System.Windows.Forms.Button();
            this.btnAuftragErfassen = new System.Windows.Forms.Button();
            this.btnTagesabrechnung = new System.Windows.Forms.Button();
            this.btnMitarbeiter = new System.Windows.Forms.Button();
            this.btnSontiges = new System.Windows.Forms.Button();
            this.btnBeenden = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKundenDaten
            // 
            this.btnKundenDaten.Location = new System.Drawing.Point(24, 12);
            this.btnKundenDaten.Name = "btnKundenDaten";
            this.btnKundenDaten.Size = new System.Drawing.Size(336, 68);
            this.btnKundenDaten.TabIndex = 0;
            this.btnKundenDaten.Text = "Kunden Daten (F1)";
            this.btnKundenDaten.UseVisualStyleBackColor = true;
            this.btnKundenDaten.Click += new System.EventHandler(this.btnKundenDaten_Click);
            // 
            // btnSpeiseKarte
            // 
            this.btnSpeiseKarte.Location = new System.Drawing.Point(24, 89);
            this.btnSpeiseKarte.Name = "btnSpeiseKarte";
            this.btnSpeiseKarte.Size = new System.Drawing.Size(336, 68);
            this.btnSpeiseKarte.TabIndex = 1;
            this.btnSpeiseKarte.Text = "Speise Karte (F2)";
            this.btnSpeiseKarte.UseVisualStyleBackColor = true;
            this.btnSpeiseKarte.Click += new System.EventHandler(this.btnSpeiseKarte_Click);
            // 
            // btnAuftragErfassen
            // 
            this.btnAuftragErfassen.Location = new System.Drawing.Point(24, 166);
            this.btnAuftragErfassen.Name = "btnAuftragErfassen";
            this.btnAuftragErfassen.Size = new System.Drawing.Size(336, 68);
            this.btnAuftragErfassen.TabIndex = 2;
            this.btnAuftragErfassen.Text = "Auftrag Erfassen(F3)";
            this.btnAuftragErfassen.UseVisualStyleBackColor = true;
            this.btnAuftragErfassen.Click += new System.EventHandler(this.btnAuftragErfassen_Click);
            // 
            // btnTagesabrechnung
            // 
            this.btnTagesabrechnung.Location = new System.Drawing.Point(24, 243);
            this.btnTagesabrechnung.Name = "btnTagesabrechnung";
            this.btnTagesabrechnung.Size = new System.Drawing.Size(336, 68);
            this.btnTagesabrechnung.TabIndex = 3;
            this.btnTagesabrechnung.Text = "Tagesabbrechnung (F4)";
            this.btnTagesabrechnung.UseVisualStyleBackColor = true;
            this.btnTagesabrechnung.Click += new System.EventHandler(this.btnTagesabrechnung_Click);
            // 
            // btnMitarbeiter
            // 
            this.btnMitarbeiter.Location = new System.Drawing.Point(24, 320);
            this.btnMitarbeiter.Name = "btnMitarbeiter";
            this.btnMitarbeiter.Size = new System.Drawing.Size(336, 68);
            this.btnMitarbeiter.TabIndex = 4;
            this.btnMitarbeiter.Text = "Mitarbeiter (F5)";
            this.btnMitarbeiter.UseVisualStyleBackColor = true;
            this.btnMitarbeiter.Click += new System.EventHandler(this.btnMitarbeiter_Click);
            // 
            // btnSontiges
            // 
            this.btnSontiges.Location = new System.Drawing.Point(24, 397);
            this.btnSontiges.Name = "btnSontiges";
            this.btnSontiges.Size = new System.Drawing.Size(336, 68);
            this.btnSontiges.TabIndex = 5;
            this.btnSontiges.Text = "Speisekarte Drücken (F6)";
            this.btnSontiges.UseVisualStyleBackColor = true;
            this.btnSontiges.Click += new System.EventHandler(this.btnSontiges_Click);
            // 
            // btnBeenden
            // 
            this.btnBeenden.Location = new System.Drawing.Point(24, 551);
            this.btnBeenden.Name = "btnBeenden";
            this.btnBeenden.Size = new System.Drawing.Size(336, 68);
            this.btnBeenden.TabIndex = 6;
            this.btnBeenden.Text = "Beenden (Esc)";
            this.btnBeenden.UseVisualStyleBackColor = true;
            this.btnBeenden.Click += new System.EventHandler(this.button6_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(336, 68);
            this.button1.TabIndex = 7;
            this.button1.Text = "Daten Sichern";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(367, 625);
            this.Controls.Add(this.btnBeenden);
            this.Controls.Add(this.btnSontiges);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMitarbeiter);
            this.Controls.Add(this.btnTagesabrechnung);
            this.Controls.Add(this.btnAuftragErfassen);
            this.Controls.Add(this.btnSpeiseKarte);
            this.Controls.Add(this.btnKundenDaten);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hauptmenü";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKundenDaten;
        private System.Windows.Forms.Button btnSpeiseKarte;
        private System.Windows.Forms.Button btnAuftragErfassen;
        private System.Windows.Forms.Button btnTagesabrechnung;
        private System.Windows.Forms.Button btnMitarbeiter;
        private System.Windows.Forms.Button btnSontiges;
        private System.Windows.Forms.Button btnBeenden;
        private System.Windows.Forms.Button button1;
    }
}

