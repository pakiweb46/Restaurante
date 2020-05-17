using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class frmShowList : Form
    {
        //ovveride initialzeComponent()
        private bool isBestellung = false;

        private void InitializeArtikel()
        {
            rData = new RestauranteData();
            this.listView1 = new PrintableListView2();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();

            this.SuspendLayout();
            //
            // listView1
            //
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(53, 75);
            this.listView1.Margin = new System.Windows.Forms.Padding(5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(804, 464);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;

            //
            // columnHeader1
            //
            this.columnHeader1.Text = "Artikel Nr";
            this.columnHeader1.Width = 149;
            //
            // columnHeader2
            //
            this.columnHeader2.Text = "Bezeichnung";
            this.columnHeader2.Width = 312;
            //
            // columnHeader3
            //
            this.columnHeader3.Text = "Zusatz";
            this.columnHeader3.Width = 118;
            //
            // columnHeader5
            //
            this.columnHeader4.Text = "Preis";
            this.columnHeader4.Width = 133;
            // button1
            //
            this.button1.Location = new System.Drawing.Point(53, 614);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "Drucken (F3)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // button2
            //
            this.button2.Location = new System.Drawing.Point(195, 614);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 45);
            this.button2.TabIndex = 5;
            this.button2.Text = "Zurück (Esc)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            //

            //
            // frmShowList
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 671);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);

            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmShowList";
            this.Text = "frmShowList";
            this.Load += new System.EventHandler(this.frmShowList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeBestellung()
        {
            rData = new RestauranteData();
            this.listView1 = new PrintableListView2();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();

            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();

            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(623, 553);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gesamt";
            //
            // textBox1
            //
            this.textBox1.Location = new System.Drawing.Point(729, 547);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(128, 26);
            this.textBox1.TabIndex = 3;

            //
            // listView1
            //
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,this.columnHeader5,this.columnHeader12});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(53, 75);

            this.listView1.Margin = new System.Windows.Forms.Padding(5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(804, 464);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            //
            // columnHeader1
            //
            this.columnHeader1.Text = "Artikel Nr";
            this.columnHeader1.Width = 100;
            //
            // columnHeader2
            //
            this.columnHeader2.Text = "Bezeichnung";
            this.columnHeader2.Width = 312;
            //
            // columnHeader3
            //
            this.columnHeader3.Text = "Preis";
            this.columnHeader3.Width = 118;
            //
            // columnHeader5
            //
            this.columnHeader4.Text = "Menge";
            this.columnHeader4.Width = 133;
            //
            // columnHeader5
            //
            this.columnHeader5.Text = "Total";
            this.columnHeader5.Width = 133;
            // columnHeader12
            //
            this.columnHeader12.Text = "MwSt";
            this.columnHeader12.Width = 67;

            // button1
            //
            this.button1.Location = new System.Drawing.Point(53, 614);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "Drucken (F3)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // button2
            //
            this.button2.Location = new System.Drawing.Point(195, 614);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 45);
            this.button2.TabIndex = 5;
            this.button2.Text = "Zurück (Esc)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //
            //
            // frmShowList
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 671);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);

            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmShowList";
            this.Text = "frmShowList";
            this.Load += new System.EventHandler(this.frmShowList_Load);
            this.ResumeLayout(false);
            isBestellung = true;
            this.PerformLayout();
        }

        private int index;
        private RestauranteData rData;

        public frmShowList()
        {
            InitializeComponent();
            rData = new RestauranteData();
        }

        public frmShowList(int reference, string title)
        {
            //TODO User ENUM Here decision on based on title BAD
            if (title == "Artikel Liste")
                InitializeArtikel();
            else if (title == "Bestellung Details")
                InitializeBestellung();
            index = reference;
            this.Text = title;
        }

        private void frmShowList_Load(object sender, EventArgs e)
        {
            if (this.Text == "Artikel Liste")
            {
                PerformListFill();
            }
            else if (this.Text == "Bestellung Details")
            {
                PerformListFill(true);
            }
        }

        private void PerformListFill()
        {
            listView1.Items.Clear();
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("Speisekarte");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(reader["ArtikelNr"].ToString());
                        item.SubItems.Add(reader["Bezeichnung"].ToString());
                        item.SubItems.Add(reader["zusatz"].ToString());
                        item.SubItems.Add(reader["Verkaufpreis"].ToString());
                        item.SubItems.Add(reader["MwSt"].ToString());

                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void PerformListFill(bool check)
        {
            listView1.Items.Clear();
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("Bestellung", "RechnungNr", index.ToString());
            double gesamt = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        double verkaufpreis = Convert.ToDouble(reader["Verkaufpreis"].ToString());
                        int menge = Convert.ToInt32(reader["Menge"].ToString());
                        gesamt = gesamt + (menge * verkaufpreis);
                        ListViewItem item = new ListViewItem(reader["ArtikelNr"].ToString());
                        item.SubItems.Add(reader["Bezeichnung"].ToString());
                        item.SubItems.Add(verkaufpreis.ToString());
                        item.SubItems.Add(menge.ToString());
                        item.SubItems.Add((verkaufpreis * menge).ToString());

                        item.SubItems.Add(reader["Mwst"].ToString());
                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                textBox1.Text = gesamt.ToString();
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private string KundenTelefone, KundenName, KundenAddresse, KundenPLZ, KundenOrt, KundenHinweis;
        private double AnfahrtKosten, Rabbatt, MwstAnfahrt;

        private int FindKunde(int rechnr)
        {
            int idKundenNr = 0;
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("abbrechnung", "idrechnung", rechnr.ToString(), "idkundendaten");

            if (reader.Read())
            {
                idKundenNr = Convert.ToInt32(reader["idkundendaten"].ToString());
            }
            reader.Close();
            rData.closeReadConnection();

            return idKundenNr;
        }

        private void loadKundendaten(int kundenid)
        {
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", kundenid.ToString());

            if (reader.Read())
            {
                KundenName = reader["KundenName"].ToString();
                KundenOrt = reader["ort"].ToString();
                KundenPLZ = reader["PLZ"].ToString();
                KundenAddresse = reader["strasse"].ToString() + " ." + reader["StrNo"].ToString();
                KundenTelefone = reader["kundennr"].ToString();
                KundenHinweis = reader["zusatz"].ToString();
                AnfahrtKosten = Convert.ToDouble(reader["AnfahrtKosten"].ToString());
                MwstAnfahrt = AnfahrtKosten * 0.19;
                // Mwst of Anfahrt in Mwst Total
                Rabbatt = Convert.ToDouble(reader["Rabatt"].ToString());
            }

            reader.Close();
            rData.closeReadConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isBestellung)
            {
                loadKundendaten(FindKunde(index));
                RecieptPrint obj = new RecieptPrint(listView1.Items.Count);
                string[] Artikel_Nummer = new string[listView1.Items.Count];
                string[] Artikel_Text = new string[listView1.Items.Count];
                double[] Artikel_Preis = new double[listView1.Items.Count];
                int[] Artikel_Anzahl = new int[listView1.Items.Count];
                for (int i = 0; i < listView1.Items.Count; i++)
                {///MMMMMMMM
                    if (listView1.Items[i].SubItems[0].Text == "+" || listView1.Items[i].SubItems[0].Text == "-")
                    {
                        Artikel_Nummer[i] = "";
                        Artikel_Text[i] = listView1.Items[i].SubItems[0].Text + " " + listView1.Items[i].SubItems[1].Text;
                        Artikel_Preis[i] = Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                        Artikel_Anzahl[i] = Convert.ToInt32(listView1.Items[i].SubItems[3].Text);
                    }
                    else
                    {
                        // add artikle to printReciept obj
                        Artikel_Nummer[i] = listView1.Items[i].SubItems[0].Text;
                        Artikel_Text[i] = listView1.Items[i].SubItems[1].Text;
                        Artikel_Preis[i] = Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                        Artikel_Anzahl[i] = Convert.ToInt32(listView1.Items[i].SubItems[3].Text);
                    }
                    obj.Artikel_Nummer = Artikel_Nummer;
                    obj.Artikel_Text = Artikel_Text;
                    obj.Artikel_Preis = Artikel_Preis;
                    obj.Artikel_Anzahl = Artikel_Anzahl;
                    if (Artikel_Nummer[0].Substring(0, 1) == "5" || Artikel_Nummer[0].Substring(0, 1) == "6" || Artikel_Nummer[0].Substring(0, 1) == "7" || Artikel_Nummer[0].Substring(0, 1) == "8" || Artikel_Nummer[0].Substring(0, 1) == "9")
                    {
                        obj.Title_Text = Globals.TITLE_NAME;
                        obj.Addresse_Text_Line1 = Globals.LINE1_ADDRESS;
                        obj.Addresse_Text_Line2 = Globals.LINE2_TELE;
                        obj.Addresse_Text_Line3 = Globals.LINE3_TELE2;
                        obj.Oeffenung_Text_Line1 = Globals.LINE4_OPENTIME;
                    }
                    else
                    {
                        obj.Title_Text = Globals.TITLE_NAME;
                        obj.Addresse_Text_Line1 = Globals.LINE1_ADDRESS;
                        obj.Addresse_Text_Line2 = Globals.LINE2_TELE;
                        obj.Addresse_Text_Line3 = Globals.LINE3_TELE2;
                        obj.Oeffenung_Text_Line1 = Globals.LINE4_OPENTIME;
                    }
                    obj.Bestellung_Text = "Bestellung " + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                    obj.KundenName_Text = KundenName;
                    obj.KundenNr_Text = KundenTelefone;
                    obj.KundenAddresse_Text = KundenAddresse + " " + KundenPLZ + " " + KundenOrt;
                    obj.Hinweise_Text = KundenHinweis;
                    obj.MwSt7 = 0; //TotalMwst7;
                    obj.MwSt19 = 0;// TotalMwst19;
                    obj.Rabatt = Rabbatt;
                    obj.Anfahrt_Kosten = AnfahrtKosten;
                    obj.Gesamt_Betrag = Convert.ToDouble(textBox1.Text);
                    obj.Print();
                }
            }
            else
                listView1.Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}