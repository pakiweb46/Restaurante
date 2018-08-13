using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LieferDienst;
namespace Restaurante
{
    public partial class frmBestellungDetails : Form
    {
        public frmBestellungDetails(int reference,int bestnr)
        {
            InitializeComponent();
            index = reference;
            this.BestellNr = bestnr;
        }
        private int index;
               static string connStr = Class1.connString;
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlCommand cmd;
        MySqlDataReader rdr;

        private string KundenTelefone, KundenName, KundenAddresse, KundenPLZ, KundenOrt, KundenHinweis;
        private double AnfahrtKosten, Rabbatt, MwstAnfahrt,TotalMwst7,TotalMwst19;
        private int BestellNr, kundenreference;
        private void PerformListFill()
        {

            conn.Open();
            listView1.Items.Clear();
            string sql = "SELECT * From dbbari.Bestellung where RechnungNr=" + index + ";";
            cmd = new MySqlCommand(sql, conn);
            try
            {

                rdr = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            double gesamt = 0;
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    try
                    {
                        double verkaufpreis = Convert.ToDouble(rdr["Verkaufpreis"].ToString());
                        int menge = Convert.ToInt32(rdr["Menge"].ToString());
                        if(rdr["ArtikelNr"].ToString()=="-")
                            gesamt -= (menge * verkaufpreis);
                        else
                        gesamt = gesamt + (menge * verkaufpreis);
                        ListViewItem item = new ListViewItem(rdr["ArtikelNr"].ToString());
                        item.SubItems.Add(rdr["Bezeichnung"].ToString());
                        item.SubItems.Add(menge.ToString());
                        item.SubItems.Add(verkaufpreis.ToString());
                        item.SubItems.Add((verkaufpreis * menge).ToString());
                        item.SubItems.Add(rdr["Mwst"].ToString());
                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                tbAnfahrtkosten.Text = AnfahrtKosten.ToString();
                tbTotalMwst.Text = (TotalMwst19 + TotalMwst7).ToString();
                tbGesamt.Text = (gesamt+AnfahrtKosten).ToString();

            }
            try
            {
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void frmBestellungDetails_Load(object sender, EventArgs e)
        {
            loadKundendaten(FindKunde(index));
            PerformListFill();
        }
        private int FindKunde(int rechnr)
        {
            int idKundenNr = 0;
            if (conn.State.ToString() == "Closed")
                conn.Open();
            string sql = "SELECT idkundendaten,Mwst7,Mwst19 FROM dbbari.abbrechnung Where idrechnung=" + rechnr + ";";
            cmd = new MySqlCommand(sql, conn);
            try
            {
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (rdr.Read())
            {
                idKundenNr = Convert.ToInt32(rdr["idkundendaten"].ToString());
                TotalMwst19 = Convert.ToDouble(rdr["Mwst19"].ToString());
                TotalMwst7 = Convert.ToDouble(rdr["Mwst7"].ToString());
                
            }

            try
            {
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            return idKundenNr;
        }
        private void loadKundendaten(int kundenid)
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();
            string sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" + kundenid + ";";
            cmd = new MySqlCommand(sql, conn);
            try
            {
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (rdr.Read())
            {
                kundenreference = Convert.ToInt32(rdr["idKundendaten"].ToString());
                KundenName = rdr["KundenName"].ToString();
                KundenOrt = rdr["ort"].ToString();
                KundenPLZ = rdr["PLZ"].ToString();
                KundenAddresse = rdr["strasse"].ToString() + " ." + rdr["StrNo"].ToString();
                KundenTelefone = rdr["kundennr"].ToString();
                KundenHinweis = rdr["zusatz"].ToString();
                AnfahrtKosten = Convert.ToDouble(rdr["AnfahrtKosten"].ToString());
                tbAnfahrtkosten.Text = AnfahrtKosten.ToString();
                MwstAnfahrt = AnfahrtKosten * 0.19;
                // Mwst of Anfahrt in Mwst Total
                Rabbatt = Convert.ToDouble(rdr["Rabatt"].ToString());
            }

            try
            {
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private bool checkArtikel(string artikelText)
        {
            bool retValue;
            if ((artikelText.Substring(artikelText.Length - 1, 1) == "a") || (artikelText.Substring(artikelText.Length - 1, 1) == "k") || (artikelText.Substring(artikelText.Length - 1, 1) == "b") || (artikelText.Substring(artikelText.Length - 1, 1) == "c") || (artikelText.Substring(artikelText.Length - 1, 1) == "d") || (artikelText.Substring(artikelText.Length - 1, 1) == "e") || (artikelText.Substring(artikelText.Length - 1, 1) == "f") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "r") || (artikelText.Substring(artikelText.Length - 1, 1) == "s") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "l") || (artikelText.Substring(artikelText.Length - 1, 1) == "X") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "i") || (artikelText.Substring(artikelText.Length - 1, 1) == "K") || (artikelText.Substring(artikelText.Length - 1, 1) == "L") || (artikelText.Substring(artikelText.Length - 1, 1) == "z") || (artikelText.Substring(0, 1) == "a"))
                artikelText = artikelText.Substring(0, artikelText.Length - 1);
            if ((artikelText.Substring(artikelText.Length - 1, 1) == "a") || (artikelText.Substring(artikelText.Length - 1, 1) == "k") || (artikelText.Substring(artikelText.Length - 1, 1) == "b") || (artikelText.Substring(artikelText.Length - 1, 1) == "c") || (artikelText.Substring(artikelText.Length - 1, 1) == "d") || (artikelText.Substring(artikelText.Length - 1, 1) == "e") || (artikelText.Substring(artikelText.Length - 1, 1) == "f") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "r") || (artikelText.Substring(artikelText.Length - 1, 1) == "s") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "l") || (artikelText.Substring(artikelText.Length - 1, 1) == "X") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "i") || (artikelText.Substring(artikelText.Length - 1, 1) == "K") || (artikelText.Substring(artikelText.Length - 1, 1) == "L") || (artikelText.Substring(artikelText.Length - 1, 1) == "z") || (artikelText.Substring(0, 1) == "a"))
                artikelText = artikelText.Substring(0, artikelText.Length - 1);
            try
            {
                if (Convert.ToInt64(artikelText) > 500)
                    retValue = true;
                else

                    retValue = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Title Zur Grillpfane Festgellegt");
                retValue = true;

            }
            /*  if ((artikelText.Substring(0, 1) == "5" || artikelText.Substring(0, 1) == "6" || artikelText.Substring(0, 1) == "7" || artikelText.Substring(0, 1) == "8" || artikelText.Substring(0, 1) == "9") & (length > 2))
                retValue = true;
            else
                retValue = false;*/
            return retValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            RecieptPrint obj = new RecieptPrint(listView1.Items.Count);
            string[] Artikel_Nummer = new string[listView1.Items.Count];
            string[] Artikel_Text = new string[listView1.Items.Count];
            double[] Artikel_Preis = new double[listView1.Items.Count];
            int[] Artikel_Anzahl = new int[listView1.Items.Count];
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems[0].Text == "+" || listView1.Items[i].SubItems[0].Text == "-")
                {
                    Artikel_Nummer[i] = "";
                    Artikel_Text[i] = listView1.Items[i].SubItems[0].Text + " " + listView1.Items[i].SubItems[1].Text;
                    Artikel_Preis[i] = Convert.ToDouble(listView1.Items[i].SubItems[3].Text);
                    Artikel_Anzahl[i] = Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                }
                else
                {
                    // add artikle to printReciept obj
                    Artikel_Nummer[i] = listView1.Items[i].SubItems[0].Text;
                    Artikel_Text[i] = listView1.Items[i].SubItems[1].Text;
                    Artikel_Preis[i] = Convert.ToDouble(listView1.Items[i].SubItems[3].Text);
                    Artikel_Anzahl[i] = Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                }
            }
                obj.Artikel_Nummer = Artikel_Nummer;
                obj.Artikel_Text = Artikel_Text;
                obj.Artikel_Preis = Artikel_Preis;
                obj.Artikel_Anzahl = Artikel_Anzahl;
                bool isGrillPfane = checkArtikel(Artikel_Nummer[0]);
                if (isGrillPfane)
                {
                    obj.Title_Text = "Tamarinde";
                    obj.Addresse_Text_Line1 = "Grubenstr. 7 - 18055 Rostock";
                    obj.Addresse_Text_Line2 = " Tel. : 0381 21055633";
                    obj.Addresse_Text_Line3 = "Tel2. ";
                    obj.Oeffenung_Text_Line1 = "Fax : ";
                }
                else
                {
                    obj.Title_Text = "Tamarinde";
                    obj.Addresse_Text_Line1 = "Grubenstr. 7 - 18055 Rostock";
                    obj.Addresse_Text_Line2 = "Tel. 0381 21055633";
                    obj.Addresse_Text_Line3 = "Tel2.";
                    obj.Oeffenung_Text_Line1 = "";
                }
       
                obj.Bestellung_Text = "Bestellung - " + BestellNr + " - " + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                obj.KundenName_Text = KundenName;
                obj.idkunde = kundenreference.ToString();
                obj.KundenNr_Text = KundenTelefone;
                obj.KundenAddresse_Text = KundenAddresse + " " + KundenPLZ + " " + KundenOrt;
                obj.Hinweise_Text = KundenHinweis;
                obj.MwSt7 = TotalMwst7;
                obj.MwSt19 = TotalMwst19;
                obj.Rabatt = Rabbatt;
                obj.Anfahrt_Kosten = AnfahrtKosten;
                obj.Gesamt_Betrag = Convert.ToDouble(tbGesamt.Text);
                // paper sizes
                int a5index = 0;
                System.Drawing.Printing.PaperSize pkSize;
                for (int k = 0; k < obj.PrinterSettings.PaperSizes.Count; k++)
                {
                    pkSize = obj.PrinterSettings.PaperSizes[k];
                    if (pkSize.PaperName.ToString() == "A5")
                    {
                        a5index = k;
                    }

                }
                if (obj.PrinterSettings.PaperSizes[a5index].PaperName == "A5")
                    obj.DefaultPageSettings.PaperSize = obj.PrinterSettings.PaperSizes[a5index];
                try
                {
                    obj.Print();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Es ist ein Fehler beim Drücken Aufgetreten \n"+ex.Message);
                }
            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}