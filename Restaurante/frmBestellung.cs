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

    // TODO::IMprovements 1. button names  rename to some thing context relavent not button1 etc
    // TODO::IMprovements 2. Function Names to either Englisch or German
    public partial class frmBestellung : Form
    {
        static string connStr = Class1.connString;
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlCommand cmd;
        public int a5index = 0;
        MySqlDataReader rdr,rdr1;
        private Zutaten obj;
        private double TotalMwst7, TotalMwst19,restmwst;
        public string kundenreference;
        public bool two_print;
        public int BestellNr = 0,currentIndex=-1;
        public double valueadded=0,valaddedMwst=0,valueremoved=0,valremMwst=0;
        public double grundpreis = 0,grundmwst=0;
        public double gesamt_pfand=0; // speichert die gesamt pfand.
     //s   private Speisekarte speise = new Speisekarte();
        public frmBestellung(string kno)
        {
            InitializeComponent();
            this.kundenreference = kno;
            try
            {
                loadKundendaten(Convert.ToInt32(kno));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void frmBestellung_Load(object sender, EventArgs e)
        {
            currentIndex = -1;
            TotalMwst19 =  MwstAnfahrt; 
            TotalMwst7 = 0;
            valaddedMwst = 0;
            valremMwst=0;
            valueadded = 0;
            gesamt_pfand = 0;
            label5.Text = valueadded.ToString();
            valueremoved = 0;
            label6.Text = valueremoved.ToString();
            tbTotalMwst.Text = String.Format("{0:0.00}", MwstAnfahrt);
            LoadOptionVariable();
            listView1.Enabled = false;
            button1.Enabled = false;
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmBestellung_KeyDown);
            tbArtikel.Focus();
        }
        private void LoadOptionVariable()
        {
               if (conn.State.ToString() == "Closed")
                conn.Open();
               // var_var takes two prints=1 means two zettel
            string sql = "Select var_value from dbbari.option_variable where var_name='two_print';";
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
                if (rdr["var_value"].ToString() == "1")
                    two_print = true;
                else
                    two_print = false;
            }
            try
            {
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void frmBestellung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (button1.Enabled == true) // Drucken
                    button1.PerformClick();

            }
            else if (e.KeyCode == Keys.F10)
            {
                button2.PerformClick(); // Speichern

            }
            else if (e.KeyCode == Keys.F4)
            {
               button4.PerformClick(); 
            }
            else if (e.KeyCode == Keys.F5)
            {
                button3.PerformClick(); // Felder leeren
            }


        }
        public void PerformListFill()
        {

            if (conn.State.ToString() == "Closed")
            conn.Open();
            listView1.Items.Clear();
            string sql = "SELECT * From dbbari.Speisekarte ";
            string sql2 = "SELECT * From dbbari.zutaten";
            cmd = new MySqlCommand(sql, conn);
            try
            {

                rdr = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    try
                    {
                        // Artikel Liste
                        ListViewItem item = new ListViewItem(rdr["Bezeichnung"].ToString());
                        item.SubItems.Add(rdr["ArtikelNr"].ToString());
                        item.SubItems.Add(rdr["Verkaufpreis"].ToString());
                        lvArtikel.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

            }

            try
            {
                rdr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd = new MySqlCommand(sql2, conn);
            try
            {

                rdr = cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    try
                    {
                        // zutaten liste
                        ListViewItem item = new ListViewItem(rdr["ZutatName"].ToString());
                        item.SubItems.Add(rdr["Preis"].ToString());
                        listView1.Items.Add(item);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

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

        
        private string pfand="OHNE"; //TODO:: No need of global pfand
        private string oldArtikel; //tODO :: what does old artikel has function of
        private void tbArtikel_GotFocus(object sender, EventArgs e)
        {
            tbArtikel.SelectAll();
            paneltbArtikel.BackColor = SystemColors.Highlight;
        }
        private void tbArtikel_LostFocus(object sender, EventArgs e)
        {
            paneltbArtikel.BackColor = SystemColors.Control;
            
               

        }
      
        private void tbArtikel_KeyDown(object sender, KeyEventArgs e)
        {
           
            oldArtikel = tbArtikel.Text;
            if (conn.State.ToString() == "Closed")
                conn.Open();

            if (e.KeyCode == Keys.Left)
            {
                listView1.Focus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                lvArtikel.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            { 
                lvBestellung.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {   tbAnfahrtkosten.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (tbArtikel.Text != "")
                {

                    string sql;
                    sql = "SELECT * FROM dbbari.speisekarte WHERE ArtikelNr='" + tbArtikel.Text.Trim() + "';";
                    cmd = new MySqlCommand(sql, conn);

                    try
                    {
                        rdr = cmd.ExecuteReader();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rdr.HasRows)
                    {
                        if (rdr.Read())
                        {

                            tbBezeichnung.Text = rdr["Bezeichnung"].ToString();
                            tbPreis.Text = rdr["verkaufpreis"].ToString();
                           
                            tbMwSt.Text = rdr["Mwst"].ToString();
                            pfand = rdr["pfand"].ToString(); // global variable string
                            tbMenge.Focus();
                            tbMenge.Text = "1";

                        }
                    }
                    else
                    {
                        MessageBox.Show("Artikel nicht gefunden");
                        tbArtikel.Focus();
                        tbArtikel.Text = oldArtikel;
                    }

                    try
                    {
                        rdr.Close();
                        conn.Close();
                        tbArtikel.Focus();
                        tbArtikel.Text = oldArtikel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else if (e.KeyCode == Keys.Tab)
                {
                    if (tbArtikel.Text != "")
                    {
                        if (!rdr.IsClosed)
                            rdr.Close();
                        if (conn.State.ToString() == "Closed")
                            conn.Open();

                        string sql;
                        sql = "SELECT * FROM dbbari.speisekarte WHERE ArtikelNr='" + tbArtikel.Text.Trim() + "';";
                        cmd = new MySqlCommand(sql, conn);

                        try
                        {
                            rdr = cmd.ExecuteReader();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        MessageBox.Show("Test Log Case: Unkonwn");
                        if (rdr.HasRows)
                        {
                            if (rdr.Read())
                            {

                                tbBezeichnung.Text = rdr["Bezeichnung"].ToString();
                                tbPreis.Text = rdr["verkaufpreis"].ToString();
                                tbMwSt.Text = rdr["Mwst"].ToString();
                                pfand = rdr["pfand"].ToString();

                                tbMenge.Focus();


                            }
                        }
                        else
                        {
                            MessageBox.Show("Artikel nicht gefunden");
                            tbArtikel.Focus();
                            tbArtikel.Text = oldArtikel;
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
                }
                else
                {
                    MessageBox.Show("Bitte geben sie die Artikel Nummer ein", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbArtikel.Focus();
                }

            }
            
        }
        private void AddArtikle()


        {
            /*TODO:: MWST Rechnung is wrong brutto price/1,07 for netto price and brutto price -netto price is the MWST*/
            /* Here calculated with direct price *0,07 which is wrong as the prices are brutto prices*/
            /* same is true for pfand so pfand can be simple artikel */
            double tempVar = 0;
            grundpreis = 0;
            
                //  if(tbPreis.Text!="" && tbMenge.Text!="") // Checked Need to change
                tempVar = (Convert.ToDouble(tbMenge.Text)) * (Convert.ToDouble(tbPreis.Text));
                grundpreis = tempVar;


                double tempMwst7, tempMwst19, tempMwst;
                ListViewItem val = new ListViewItem(tbArtikel.Text);
                val.SubItems.Add(tbBezeichnung.Text);
                val.SubItems.Add(tbMenge.Text);
                val.SubItems.Add(tbPreis.Text);
                val.SubItems.Add(tempVar.ToString());
                if (tbMwSt.Text == "7")
                {
                    tempMwst7 = tempVar * 0.07;
                    TotalMwst7 += tempMwst7;
                    tempMwst19 = 0;
                }
                else if (tbMwSt.Text == "19")
                {
                    tempMwst19 = tempVar * 0.19;
                    TotalMwst19 += tempMwst19;
                    tempMwst7 = 0;
                }
                else
                {
                    tempMwst = 0;
                    tempMwst19 = 0;
                    tempMwst7 = 0;
                }
                tempMwst = tempMwst19 + tempMwst7;
                grundmwst = tempMwst;
                val.SubItems.Add(tempMwst.ToString());
                lvBestellung.Items.Add(val);
               
                currentIndex = lvBestellung.Items.Count - 1;
                 if (tbGesamt.Text != "")
                {
                    grundpreis = Convert.ToDouble(tbGesamt.Text) + tempVar;

                    tbGesamt.Text = String.Format("{0:0.00}", (grundpreis.ToString()));
                }

                else
                {
                    grundpreis = tempVar;
                    tbGesamt.Text = tempVar.ToString();
                }
                 txtRabatt.Text = ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
                 txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(txtRabatt.Text)).ToString();
            grundmwst = Convert.ToDouble(tbTotalMwst.Text) + tempMwst;
                tbTotalMwst.Text = (grundmwst.ToString());


                button1.Enabled = true;
                listView1.Enabled = true;
                if (pfand == "PFAND1")
                    pfandAdd("PFAND1");
                else if (pfand == "PFAND2")
                    pfandAdd("PFAND2");
                else if (pfand == "PFAND3")
                    pfandAdd("PFAND3");

                PerformClear();
                tbArtikel.Focus();
            
            
        }
        private void tbMenge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbMenge_LostFocus(this, null);
        }
        private void tbMenge_LostFocus(object sender, EventArgs e)
        {
            AddArtikle();
        }
        private void PerformClear()
        {
          //  tbArtikel.Text = "";
            tbBezeichnung.Text = "";
            tbMenge.Text = "";
            tbPreis.Text = "";
        }
       private void Speicher_Online()// Nur online bestellung speichern TODO:: What is online bestellung
        {
            int BestNr = getBestellNr_online(System.DateTime.Now.ToShortDateString());
            if (conn.State.ToString() == "Closed")
                conn.Open();
            if (lvBestellung.Items.Count != 0)
            {
                try
                {

                    int idRech;
                    MySqlCommand cmd1 = new MySqlCommand();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd2.Connection = conn;
                    cmd.Parameters.Clear();

                    cmd1.CommandText = "INSERT INTO dbbari.abbr VALUES (NULL, @Datum , @Zeit, @RestBetrag, @Betrag, @Rabatt,  @idKundendaten,@Fahrer,@Mwst7,@Mwst19,@BestellNr)";
                    cmd1.Prepare();
                    cmd1.Parameters.AddWithValue("Datum", System.DateTime.Now.ToShortDateString());
                    cmd1.Parameters.AddWithValue("Zeit", System.DateTime.Now.ToShortTimeString());
                    cmd1.Parameters.AddWithValue("RestBetrag", Math.Round(Convert.ToDouble(txtDifference.Text), 3));
                    cmd1.Parameters.AddWithValue("Betrag", Math.Round(Convert.ToDouble(tbGesamt.Text), 3));
                    cmd1.Parameters.AddWithValue("Rabatt", (Convert.ToDouble(tbGesamt.Text)-(AnfahrtKosten+gesamt_pfand))*Rabbatt/100); // Need to be given real Rabatt
                    cmd1.Parameters.AddWithValue("idKundendaten", kundenreference);
                    cmd1.Parameters.AddWithValue("Mwst7", Math.Round(TotalMwst7, 3));
                    cmd1.Parameters.AddWithValue("Mwst19", Math.Round(TotalMwst19, 3));
                    cmd1.Parameters.AddWithValue("BestellNr", BestNr);
                    if (kundenreference == "0")  //TODO:: No need to hardcode this stuff unsure if it is used
                        cmd1.Parameters.AddWithValue("Fahrer", "Hausverkauf");
                    else
                        cmd1.Parameters.AddWithValue("Fahrer", "Kein Fahrer");


                    cmd1.ExecuteNonQuery();

                    cmd1.CommandText = "select last_insert_id()";
                    idRech = Convert.ToInt32(cmd1.ExecuteScalar());
                    // MessageBox.Show(idRech.ToString());
                    //TODO:: Alternative method of adding "zutaten may be a new data set for Extras and not that 
                    // much hard coded + - Calculcation
                    for (int i = 0; i < lvBestellung.Items.Count; i++)
                    {
                    /*    if (lvBestellung.Items[i].SubItems[0].Text == "+" || lvBestellung.Items[i].SubItems[0].Text == "-")
                        {
                            //Skip this
                            //MessageBox.Show(lvBestellung.Items[i].SubItems[1].Text);
                        }
                        else
                        {
                            
                            cmd2.CommandText = "INSERT INTO dbbari.Bestell VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text), 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                            MessageBox.Show("Saving online"+idRech);
                        }*/
                        if (lvBestellung.Items[i].SubItems[0].Text == "+")
                        {

                            double tempRabatt=Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)*Rabbatt/100),3);
                            cmd2.CommandText = "INSERT INTO Bestell VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                        else if (lvBestellung.Items[i].SubItems[0].Text == "-")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestell VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                        else if (lvBestellung.Items[i].SubItems[0].Text == "+-")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestell VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", 0);
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                        else
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestell VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Convert.ToDouble(String.Format("{0:00.00}", lvBestellung.Items[i].SubItems[5].Text)));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                    }

                    MessageBox.Show("Küche Gerät nicht Angeschlossen");
                    this.Close();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else if (lvBestellung.Items.Count == 0)
            {
                MessageBox.Show("Bestellung ist Leer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void Speicher_Daten() // It saves the data without printing
        {

            //TODO:: here also + - of Extras "Zustaten" not hard coded may be a method as the code is used in
            // other methods
            if (conn.State.ToString() == "Closed")
                conn.Open();
            if (lvBestellung.Items.Count != 0)
            {
                try
                {
                   if (checkBox1.Checked)
                        Speicher_Online();
       
                    int idRech;
                    MySqlCommand cmd1 = new MySqlCommand();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd2.Connection = conn;
                    cmd.Parameters.Clear();

                    cmd1.CommandText = "INSERT INTO dbbari.abbrechnung VALUES (NULL, @Datum , @Zeit, @RestBetrag, @Betrag, @Rabatt,  @idKundendaten,@Fahrer,@Mwst7,@Mwst19,@BestellNr)";
                    cmd1.Prepare();
                    cmd1.Parameters.AddWithValue("Datum", System.DateTime.Now.ToShortDateString());
                    cmd1.Parameters.AddWithValue("Zeit", System.DateTime.Now.ToShortTimeString());
                    cmd1.Parameters.AddWithValue("RestBetrag", Math.Round(Convert.ToDouble(txtDifference.Text),3));
                    cmd1.Parameters.AddWithValue("Betrag", Math.Round(Convert.ToDouble(tbGesamt.Text),3));
                    cmd1.Parameters.AddWithValue("Rabatt", (Convert.ToDouble(tbGesamt.Text)-(AnfahrtKosten+gesamt_pfand))*Rabbatt/100); // Need to be given real Rabatt
                    cmd1.Parameters.AddWithValue("idKundendaten", kundenreference);
                    cmd1.Parameters.AddWithValue("Mwst7", Math.Round(TotalMwst7,3));
                    cmd1.Parameters.AddWithValue("Mwst19", Math.Round(TotalMwst19,3));
                    cmd1.Parameters.AddWithValue("BestellNr", BestellNr);
                    if (kundenreference == "0")
                        cmd1.Parameters.AddWithValue("Fahrer", "Hausverkauf");
                    else
                        cmd1.Parameters.AddWithValue("Fahrer", "Kein Fahrer");

                    
                    cmd1.ExecuteNonQuery();

                    cmd1.CommandText = "select last_insert_id()";
                    idRech = Convert.ToInt32(cmd1.ExecuteScalar());
                    // MessageBox.Show(idRech.ToString());
                    for (int i = 0; i < lvBestellung.Items.Count; i++)
                    {
                     /*   if (lvBestellung.Items[i].SubItems[0].Text == "+" || lvBestellung.Items[i].SubItems[0].Text == "-")
                        {
                           //Skip this
                            //MessageBox.Show(lvBestellung.Items[i].SubItems[1].Text);
                        }
                        else
                        {
                            cmd2.CommandText = "INSERT INTO dbbari.Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text),3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text),3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                        }*/
                        if (lvBestellung.Items[i].SubItems[0].Text == "+")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);

                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                        else if (lvBestellung.Items[i].SubItems[0].Text == "-")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                        else if (lvBestellung.Items[i].SubItems[0].Text == "+-")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", 0);
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                        else
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Convert.ToDouble(String.Format("{0:00.00}", lvBestellung.Items[i].SubItems[5].Text)));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                         
                        }
                    }

                    MessageBox.Show("Bestellung ist Gespeichert");
                    this.Close();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else if (lvBestellung.Items.Count == 0)
            {
                MessageBox.Show("Bestellung ist Leer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myClearForm();
        }
        private void myClearForm()
        {
            tbArtikel.Text = "";
            tbBezeichnung.Text = "";
            tbGesamt.Text = "";
            tbMenge.Text = "";
            tbPreis.Text = "";
            lvBestellung.Items.Clear();
            tbGesamt.Text = "";
            txtDifference.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BestellNr = getBestellNr(System.DateTime.Now.ToShortDateString());
            Speicher_Daten(); // Save the Data

        }
        private string KundenTelefone, KundenName, KundenAddresse, KundenPLZ, KundenOrt,KundenHinweis;
        private double AnfahrtKosten, Rabbatt,MwstAnfahrt;
        private void loadKundendaten(int kundenid)
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();
            string sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" +kundenid + ";";
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
                KundenName = rdr["KundenName"].ToString();
                
                KundenOrt = rdr["ort"].ToString();
                KundenPLZ= rdr["PLZ"].ToString();
                KundenAddresse = rdr["strasse"].ToString()+" ."+rdr["StrNo"].ToString();
                KundenTelefone = rdr["kundennr"].ToString();
                KundenHinweis=rdr["zusatz"].ToString();
                AnfahrtKosten =Convert.ToDouble( rdr["AnfahrtKosten"].ToString());
                MwstAnfahrt = Math.Round(AnfahrtKosten * 0.19,3);
                tbAnfahrtkosten.Text = String.Format("{0:0.00}",AnfahrtKosten);
                // Add anfahrt kosten in total
                tbGesamt.Text = String.Format("{0:0.00}",AnfahrtKosten);
                // Mwst of Anfahrt in Mwst Total
                Rabbatt = Convert.ToDouble(rdr["Rabatt"].ToString()); // nehmen wir an dass rabatt in % gespeichert dann Rabatt==00%
                lblRabatt.Text = Rabbatt + "% Rabatt is Aktiviert";
                label13.Font = new Font("Arial", 12, FontStyle.Bold);
                label13.Text = KundenName + " -  " + KundenTelefone + " - " + KundenAddresse+" "+KundenPLZ+" - "+KundenOrt ;
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
        // Takes the Bestell Nummer from current date and adds one to it so that we have
        // new Bestell nummer
        private int getBestellNr(string datum)
        {
            MySqlCommand cmd1 = new MySqlCommand();
            int returnvalue=0;
            if (conn.State.ToString() == "Closed")
                conn.Open();
           
            cmd1.Connection = conn;
            
            cmd1.CommandText = "Select Max(BestellNr) from dbbari.abbrechnung where datum='" + datum + "';";
            try
            {
                rdr1 = cmd1.ExecuteReader();
                if (rdr1.HasRows)
                {
                    rdr1.Read();

                    if (rdr1[0].ToString() == "0")
                        returnvalue = 1;
                    else if (rdr1[0].ToString() == "")
                        returnvalue = 1;
                    else
                    {
                        returnvalue = Convert.ToInt32(rdr1[0].ToString()) + 1;
                    }
                }
                else
                {
                    returnvalue = 1;
                }
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                returnvalue = 1;
               
            }
            rdr1.Close();
            conn.Close();
               
            return returnvalue;
        }
        private int getBestellNr_online(string datum) // TODO:: The Function of getBestellNr Online ? is that ever used
        {
            MySqlCommand cmd1 = new MySqlCommand();
            int returnvalue = 0;
            if (conn.State.ToString() == "Closed")
                conn.Open();

            cmd1.Connection = conn;

            cmd1.CommandText = "Select Max(BestellNr) from dbbari.abbr where datum='" + datum + "';";
            try
            {
                rdr1 = cmd1.ExecuteReader();
                if (rdr1.HasRows)
                {
                    rdr1.Read();

                    if (rdr1[0].ToString() == "0")
                        returnvalue = 1;
                    else if (rdr1[0].ToString() == "")
                        returnvalue = 1;
                    else
                    {
                        returnvalue = Convert.ToInt32(rdr1[0].ToString()) + 1;
                    }
                }
                else
                {
                    returnvalue = 1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                returnvalue = 1;

            }
            rdr1.Close();
            conn.Close();

            return returnvalue;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BestellNr = getBestellNr(System.DateTime.Now.ToShortDateString());
            Zettel_Drucken(); // Save and print the reciept
         
        }
        private void Zettel_Drucken()
        {
             string []Artikel_Nummer = new string[lvBestellung.Items.Count];
                    string[] Artikel_Text = new string[lvBestellung.Items.Count];
                    double[] Artikel_Preis = new double[lvBestellung.Items.Count]; 
                    int[] Artikel_Anzahl = new int[lvBestellung.Items.Count];
            if (conn.State.ToString() == "Closed")
                conn.Open();
            if (lvBestellung.Items.Count != 0)
            {
                try
                {
                    RecieptPrint obj = new RecieptPrint(lvBestellung.Items.Count);
                    if (checkBox1.Checked)
                        Speicher_Online();
                   
                            
                    int idRech;
                    MySqlCommand cmd1 = new MySqlCommand();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd2.Connection = conn;
                    cmd.Parameters.Clear();

                    cmd1.CommandText = "INSERT INTO dbbari.abbrechnung VALUES (NULL, @Datum , @Zeit, @RestBetrag, @Betrag, @Rabatt,  @idKundendaten,@Fahrer,@Mwst7,@Mwst19,@BestellNr)";
                    cmd1.Prepare();
                    cmd1.Parameters.AddWithValue("Datum", System.DateTime.Now.ToShortDateString());
                    cmd1.Parameters.AddWithValue("Zeit", System.DateTime.Now.ToShortTimeString());
                    cmd1.Parameters.AddWithValue("RestBetrag", Convert.ToDouble(String.Format("{0:00.00}",txtDifference.Text)));
                    cmd1.Parameters.AddWithValue("Betrag", Convert.ToDouble(String.Format("{0:00.00}",tbGesamt.Text))); // Gesamt inclusive Anfahrtkosten
                    cmd1.Parameters.AddWithValue("Rabatt", (Convert.ToDouble(tbGesamt.Text)-(AnfahrtKosten+gesamt_pfand))*Rabbatt/100); // Need to be given real Rabatt in Zahlen nicht in % 
                    cmd1.Parameters.AddWithValue("idKundendaten", kundenreference);
                    cmd1.Parameters.AddWithValue("Mwst7", Math.Round(TotalMwst7,3));
                    cmd1.Parameters.AddWithValue("Mwst19", Math.Round(TotalMwst19,3));
                    cmd1.Parameters.AddWithValue("BestellNr", BestellNr);
                    if (kundenreference == "0")
                        cmd1.Parameters.AddWithValue("Fahrer", "Hausverkauf");
                    else
                        cmd1.Parameters.AddWithValue("Fahrer", "Kein Fahrer");

                    cmd1.ExecuteNonQuery();
                   
                    cmd1.CommandText = "select last_insert_id()";
                    idRech = Convert.ToInt32(cmd1.ExecuteScalar());
                    // MessageBox.Show(idRech.ToString());
                    for (int i = 0; i < lvBestellung.Items.Count; i++)
                    {
                        if (lvBestellung.Items[i].SubItems[0].Text == "+")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                           cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                           cmd2.Prepare();
                           cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                           cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                           cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                           cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)-tempRabatt,3));
                           cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                           cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text),3));
                           cmd2.ExecuteNonQuery();
                           cmd2.Parameters.Clear();
                           // add artikle to printReciept obj
                           Artikel_Nummer[i] = "+";
                           Artikel_Text[i] = lvBestellung.Items[i].SubItems[0].Text+" "+lvBestellung.Items[i].SubItems[1].Text;
                           Artikel_Preis[i] =  Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text);
                           Artikel_Anzahl[i] = Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text);
                       
                        }
                        else if( lvBestellung.Items[i].SubItems[0].Text == "-")
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                            // add artikle to printReciept obj
                            Artikel_Nummer[i] = "-";
                            Artikel_Text[i] = lvBestellung.Items[i].SubItems[0].Text + " " + lvBestellung.Items[i].SubItems[1].Text;
                            Artikel_Preis[i] = Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text);
                            Artikel_Anzahl[i] = Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text);
                       
                        }
                        else if( lvBestellung.Items[i].SubItems[0].Text == "+-")
                          {
                              double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text)-tempRabatt, 3));
                            cmd2.Parameters.AddWithValue("Menge", 0);
                            cmd2.Parameters.AddWithValue("MwSt", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[5].Text), 3));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                            // add artikle to printReciept obj
                            Artikel_Nummer[i] = "+-";
                            Artikel_Text[i] = lvBestellung.Items[i].SubItems[0].Text + " " + lvBestellung.Items[i].SubItems[1].Text;
                            Artikel_Preis[i] = Convert.ToDouble(lvBestellung.Items[i].SubItems[4].Text);
                            Artikel_Anzahl[i] = 0;// Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text);
                       
                        }
                        else
                        {
                            double tempRabatt = Math.Round((Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text) * Rabbatt / 100), 3);
                            cmd2.CommandText = "INSERT INTO Bestellung VALUES (NULL, @RechnungNr, @ArtikelNr, @Bezeichnung, @VerkaufPreis, @Menge, @MwSt)";
                            cmd2.Prepare();
                            cmd2.Parameters.AddWithValue("RechnungNr", idRech);
                            cmd2.Parameters.AddWithValue("ArtikelNr", lvBestellung.Items[i].SubItems[0].Text);
                            cmd2.Parameters.AddWithValue("Bezeichnung", lvBestellung.Items[i].SubItems[1].Text);
                            cmd2.Parameters.AddWithValue("VerkaufPreis", Math.Round(Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text)-tempRabatt,3));
                            cmd2.Parameters.AddWithValue("Menge", Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text));
                            cmd2.Parameters.AddWithValue("MwSt", Convert.ToDouble(String.Format("{0:00.00}",lvBestellung.Items[i].SubItems[5].Text)));
                            cmd2.ExecuteNonQuery();
                            cmd2.Parameters.Clear();
                            // add artikle to printReciept obj
                            Artikel_Nummer[i] = lvBestellung.Items[i].SubItems[0].Text;
                            Artikel_Text[i] = lvBestellung.Items[i].SubItems[1].Text;
                            Artikel_Preis[i] = Convert.ToDouble(lvBestellung.Items[i].SubItems[3].Text);
                            Artikel_Anzahl[i] = Convert.ToInt32(lvBestellung.Items[i].SubItems[2].Text);
                            
                        }
                    }

                    obj.Artikel_Nummer = Artikel_Nummer;
                    obj.Artikel_Text = Artikel_Text;
                    obj.Artikel_Preis = Artikel_Preis;
                    obj.Artikel_Anzahl = Artikel_Anzahl;
                    bool isGrillPfane = checkArtikel(Artikel_Nummer[0]);
                    if (isGrillPfane)
                    {

                        // TODO :: move it to some center place or global Class and also 
                        // condition with there ID Would be better
                        obj.Title_Text = "Tamarinde";  
                        obj.Addresse_Text_Line1 = "Grubenstr. 7 - 18055 Rostock ";
                        obj.Addresse_Text_Line2 = " Tel. : 0381 21055633";
                        obj.Addresse_Text_Line3 = "Tel2. : ";
                        obj.Oeffenung_Text_Line1 = "Fax : ";
                    }
                    else
                    {
                        obj.Title_Text = "Tamarinde";
                        obj.Addresse_Text_Line1 = "Grubenstr. 7 - 18055 Rostock";
                        obj.Addresse_Text_Line2 = " Tel. 0381 21055633";
                        obj.Addresse_Text_Line3 = "Tel2. ";
                        obj.Oeffenung_Text_Line1 = "";
                    } 
                    obj.Bestellung_Text = "Bestellung - " +BestellNr+" - "+ System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                    obj.KundenName_Text = KundenName;
                    obj.idkunde = kundenreference;
                    obj.KundenNr_Text = KundenTelefone;
                    obj.KundenAddresse_Text = KundenAddresse;
                    obj.KundenAddresse_Text2= KundenPLZ + " " + KundenOrt;
                    obj.Hinweise_Text = KundenHinweis;
                    obj.MwSt7 = TotalMwst7;
                    obj.MwSt19 = TotalMwst19;
                    // Calculating Rabatt Zahl 
                    Double Rabbatt_Zahl = (Convert.ToDouble(tbGesamt.Text)-(AnfahrtKosten+gesamt_pfand))*Rabbatt/100;
                    obj.Rabatt = Rabbatt_Zahl;// in zahlen nicht in percentage Rabbatt;
                    obj.Anfahrt_Kosten = AnfahrtKosten;
                    obj.Gesamt_Betrag = Convert.ToDouble(tbGesamt.Text);
                    // paper sizes
                    
                   System.Drawing.Printing.PaperSize  pkSize;
                    for (int i = 0; i < obj.PrinterSettings.PaperSizes.Count; i++)
                    {
                        pkSize = obj.PrinterSettings.PaperSizes[i];
                        if (pkSize.PaperName.ToString() == "A5")
                        {
                            a5index = i;
                            break;
                        }  

                    }
                    if(obj.PrinterSettings.PaperSizes[a5index].PaperName=="A5")
                    obj.DefaultPageSettings.PaperSize=obj.PrinterSettings.PaperSizes[a5index];
                    obj.Print();
                   
                    if (two_print)
                    {
                        obj.Title_Text = "Küche";
                        obj.Print();
                    }
                    this.Close();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+" test"+"1098");

                }
            }

        }
        // check if artikle is grillpfanne or balldino artikel may be add data set to artikel to let 
        // know to which shop it belongs
        private bool checkArtikel(string artikelText)
        {
            bool retValue;

            // BUG or NOT Both conditions do the same thing understand why it is neeeded
            if ((artikelText.Substring(artikelText.Length - 1, 1) == "a") || (artikelText.Substring(artikelText.Length - 1, 1) == "k") || (artikelText.Substring(artikelText.Length - 1, 1) == "b") || (artikelText.Substring(artikelText.Length - 1, 1) == "c") || (artikelText.Substring(artikelText.Length - 1, 1) == "d") || (artikelText.Substring(artikelText.Length - 1, 1) == "e") || (artikelText.Substring(artikelText.Length - 1, 1) == "f") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "r") || (artikelText.Substring(artikelText.Length - 1, 1) == "s") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "l") || (artikelText.Substring(artikelText.Length - 1, 1) == "X") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "i") || (artikelText.Substring(artikelText.Length - 1, 1) == "K") || (artikelText.Substring(artikelText.Length - 1, 1) == "L") || (artikelText.Substring(artikelText.Length - 1, 1) == "z") || (artikelText.Substring(0, 1) == "a"))
                artikelText=artikelText.Substring(0,artikelText.Length-1);
            if ((artikelText.Substring(artikelText.Length - 1, 1) == "a") || (artikelText.Substring(artikelText.Length - 1, 1) == "k") || (artikelText.Substring(artikelText.Length - 1, 1) == "b") || (artikelText.Substring(artikelText.Length - 1, 1) == "c") || (artikelText.Substring(artikelText.Length - 1, 1) == "d") || (artikelText.Substring(artikelText.Length - 1, 1) == "e") || (artikelText.Substring(artikelText.Length - 1, 1) == "f") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "r") || (artikelText.Substring(artikelText.Length - 1, 1) == "s") || (artikelText.Substring(artikelText.Length - 1, 1) == "v") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "l") || (artikelText.Substring(artikelText.Length - 1, 1) == "X") || (artikelText.Substring(artikelText.Length - 1, 1) == "m") || (artikelText.Substring(artikelText.Length - 1, 1) == "i") || (artikelText.Substring(artikelText.Length - 1, 1) == "K") || (artikelText.Substring(artikelText.Length - 1, 1) == "L") || (artikelText.Substring(artikelText.Length - 1, 1) == "z") || (artikelText.Substring(0, 1) == "a"))
                artikelText = artikelText.Substring(0,artikelText.Length-1);
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
      
        string selectedArtikleNo;
        private void lvArtikel_GotFocus(object sender, EventArgs e)
        {
            panelSpeiseKarte.BackColor = SystemColors.Highlight;
        }
        private void lvArtikel_LostFocus(object sender, EventArgs e)
        {
            panelSpeiseKarte.BackColor = SystemColors.Control;
        }
        private void lvArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ListView.SelectedListViewItemCollection selecteditem =
                this.lvArtikel.SelectedItems;


            foreach (ListViewItem item in selecteditem)
            {

               selectedArtikleNo = item.SubItems[1].Text;

            }

            
        }
       
        private void lvArtikel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();
            
            string sql = "Select * from dbbari.speisekarte where ArtikelNr='" + selectedArtikleNo + "';";
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

                double tempVar;
                double tempMwst,tempMwst7,tempMwst19;
                ListViewItem val = new ListViewItem(rdr["ArtikelNr"].ToString());
                val.SubItems.Add(rdr["Bezeichnung"].ToString());
                tbMwSt.Text = rdr["Mwst"].ToString();
                pfand = rdr["pfand"].ToString();
                val.SubItems.Add("1");
                tempVar=Convert.ToDouble(rdr["VerkaufPreis"].ToString());
                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());
                if (tbMwSt.Text == "7")
                {
                    tempMwst7 = Math.Round(tempVar * 0.07,3);
                    TotalMwst7 += tempMwst7;
                    tempMwst19 = 0;
                }
                else if (tbMwSt.Text == "19")
                {
                    tempMwst19 = Math.Round(tempVar * 0.19,3);
                    TotalMwst19 += tempMwst19;
                    tempMwst7 = 0;
                }
                else
                {
                    tempMwst = 0;
                    tempMwst19 = 0;
                    tempMwst7 = 0;
                }
                tempMwst = tempMwst19 + tempMwst7;
                val.SubItems.Add(tempMwst.ToString());

                lvBestellung.Items.Add(val);
                if (tbGesamt.Text != "")
                    tbGesamt.Text = String.Format("{0:0.00}", (Convert.ToDouble(tbGesamt.Text) + Convert.ToDouble(tempVar)).ToString());
                else
                    tbGesamt.Text = tempVar.ToString();
                
                txtRabatt.Text = ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
                txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(txtRabatt.Text)).ToString();
                tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) + tempMwst).ToString();
                button1.Enabled = true;
                PerformClear();

                tbArtikel.Focus();
                listView1.Enabled = true;
            
            }

            try
            {
                rdr.Close();
                conn.Close();
              //  MessageBox.Show("Reader Closed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (pfand == "PFAND1")
                pfandAdd("PFAND1");
            else if (pfand == "PFAND2")
                pfandAdd("PFAND2");
            else if (pfand == "PFAND3")
                pfandAdd("PFAND3");

            
        }
        private void lvArtikel_KeyDown(object sender, KeyEventArgs e)  // kEy down und double click do same but why different Function
            // still they are Different may be a BUG 
        {
            if (e.KeyCode == Keys.Escape)
            {
                tbArtikel.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
          
                if (conn.State.ToString() == "Closed")
                    conn.Open();
               
                string sql = "Select * from dbbari.speisekarte where ArtikelNr='" + selectedArtikleNo + "';";
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

                    tbArtikel.Text = rdr["ArtikelNr"].ToString();
                   tbBezeichnung.Text= rdr["Bezeichnung"].ToString();
                    tbMwSt.Text = rdr["Mwst"].ToString();
                    pfand = rdr["pfand"].ToString();
                   tbMenge.Text="1";
                tbPreis.Text=  rdr["VerkaufPreis"].ToString();
                try
                {
                    rdr.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                    AddArtikle();
                    listView1.Enabled = true;
            
                }


/*                                if (pfand == "PFAND1")
                    pfandAdd("PFAND1");
                else if (pfand == "PFAND2")
                    pfandAdd("PFAND2");
                else if (pfand == "PFAND3")
                    pfandAdd("PFAND3");
                */
            }
            if (e.KeyCode == Keys.Right)
                listView1.Focus();
            if (e.KeyCode == Keys.Left)
                tbArtikel.Focus();
            

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
// Zutaten Listview 1
        private void listView1_GotFocus(object sender, EventArgs e)
        {
            panelZutaten.BackColor = SystemColors.Highlight;
        }
        private void listView1_LostFocus(object sender, EventArgs e)
        {
            panelZutaten.BackColor = SystemColors.Control;
        }
        string selectedZutat;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListView.SelectedListViewItemCollection selecteditem =
                this.listView1.SelectedItems;


            foreach (ListViewItem item in selecteditem)
            {

                selectedZutat = item.SubItems[0].Text;

            }


        }

        // TODO:: Pfand Add is needed to be removed need to implement as extra attached to artikle some how
        private void pfandAdd(string var_name)
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();

            string sql = "Select var_value from dbbari.option_variable where var_name='" + var_name + "';";
            cmd = new MySqlCommand(sql, conn);
            try
            {
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+"pfand reader");
            }
            if (rdr.Read())
            {

                double tempVar;
                ListViewItem val = new ListViewItem("+");
                val.SubItems.Add("pfand");
                val.SubItems.Add("1");
                tempVar = Convert.ToDouble(rdr["var_value"].ToString());
                gesamt_pfand += tempVar;
                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());
                
                val.SubItems.Add("0"); // Mwst to zero


                lvBestellung.Items.Add(val);
                if (tbGesamt.Text != "")
                    tbGesamt.Text = String.Format("{0:0.00}", (Convert.ToDouble(tbGesamt.Text) + Convert.ToDouble(tempVar)).ToString());
                else
                    tbGesamt.Text = tempVar.ToString();

  //              tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) + tempMwst).ToString();
                txtRabatt.Text = ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
                txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(txtRabatt.Text)).ToString();
                button1.Enabled = true;
                PerformClear();
                tbArtikel.Focus();

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
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddZutat();
            /*  if (conn.State.ToString() == "Closed")
                conn.Open();
            
            string sql = "Select * from dbbari.Zutaten where ZutatName='" + selectedZutat + "';";
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

                double tempVar;
                double tempMwst, tempMwst7, tempMwst19;
                ListViewItem val = new ListViewItem("+");
                val.SubItems.Add(rdr["ZutatName"].ToString());
                val.SubItems.Add("1");
                tbMwSt.Text = rdr["Mwst"].ToString();
                tempVar = Convert.ToDouble(rdr["Preis"].ToString());
                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());
                if (tbMwSt.Text == "7")
                {
                    tempMwst7 = Math.Round(tempVar * 0.07,3);
                    TotalMwst7 += tempMwst7;
                    tempMwst19 = 0;
                }
                else if (tbMwSt.Text == "19")
                {
                    tempMwst19 = Math.Round(tempVar * 0.19,3);
                    TotalMwst19 += tempMwst19;
                    tempMwst7 = 0;
                }
                else
                {
                    tempMwst = 0;
                    tempMwst19 = 0;
                    tempMwst7 = 0;
                }
                tempMwst = tempMwst19 + tempMwst7;
                
                val.SubItems.Add(tempMwst.ToString());

                
                lvBestellung.Items.Add(val);
                if (tbGesamt.Text != "")
                    tbGesamt.Text = String.Format("{0:0.00}", (Convert.ToDouble(tbGesamt.Text) + Convert.ToDouble(tempVar)).ToString());
                else
                    tbGesamt.Text = tempVar.ToString();
                
                tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) + tempMwst).ToString();
            
                button1.Enabled = true;
                PerformClear();
               
            }

            try
            {
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
        }
        private void addZutat()
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();

            string sql = "Select * from dbbari.Zutaten where ZutatName='" + selectedZutat + "';";
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

                double tempVar;
                double tempMwst, tempMwst7, tempMwst19;
                ListViewItem val = new ListViewItem("+");
                val.SubItems.Add(rdr["ZutatName"].ToString());
                val.SubItems.Add("1");
                tbMwSt.Text = rdr["Mwst"].ToString();
                tempVar = Convert.ToDouble(rdr["Preis"].ToString());
               
                label5.Text = valueadded.ToString();

                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());
                
                    if (tbMwSt.Text == "7")
                    {
                        tempMwst7 = Math.Round(tempVar * 0.07, 3);
                        TotalMwst7 += tempMwst7;
                        tempMwst19 = 0;
                    }
                    else if (tbMwSt.Text == "19")
                    {
                        tempMwst19 = Math.Round(tempVar * 0.19, 3);
                        TotalMwst19 += tempMwst19;
                        tempMwst7 = 0;
                    }
                    else
                    {
                        tempMwst = 0;
                        tempMwst19 = 0;
                        tempMwst7 = 0;
                    }

                    tempMwst = tempMwst19 + tempMwst7;
                    val.SubItems.Add(tempMwst.ToString());
                   
                    lvBestellung.Items.Add(val);

                    if (tbGesamt.Text != "")
                        tbGesamt.Text = String.Format("{0:0.00}", (Convert.ToDouble(tbGesamt.Text) + Convert.ToDouble(tempVar)).ToString());
                    else
                        tbGesamt.Text = tempVar.ToString();
                    
                    valueadded += tempVar;
                    label5.Text = valueadded.ToString();
                    tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) + tempMwst).ToString();
                   txtRabatt.Text= ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
                   txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(txtRabatt.Text)).ToString();
                button1.Enabled = true;
                PerformClear();

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
        //TODO:: Add and remove zutat are buggy
        // Some time removing zutaten do wrong price calculation 
        // This happens some time to artikle also

        private void removeZutat()
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();

            string sql = "Select * from dbbari.Zutaten where ZutatName='" + selectedZutat + "';";
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
                double tempVar;
                double tempMwst, tempMwst7, tempMwst19;
                ListViewItem val = new ListViewItem("-");
                val.SubItems.Add(rdr["ZutatName"].ToString());
                val.SubItems.Add("1");
                tbMwSt.Text = rdr["Mwst"].ToString();
                tempVar = Convert.ToDouble(rdr["Preis"].ToString());
              
                label6.Text = valueremoved.ToString();
                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());

                    if (tbMwSt.Text == "7")
                    {
                        tempMwst7 = Math.Round(tempVar * 0.07, 3);
                        TotalMwst7 -= tempMwst7;
                        valremMwst -= tempMwst7;
                        tempMwst19 = 0;
                    }
                    else if (tbMwSt.Text == "19")
                    {
                        tempMwst19 = Math.Round(tempVar * 0.19, 3);
                        TotalMwst19 -= tempMwst19;
                        valremMwst -= tempMwst19;
                        tempMwst7 = 0;
                    }
                    else
                    {
                        tempMwst = 0;
                        tempMwst19 = 0;
                        tempMwst7 = 0;
                    }

                    tempMwst = tempMwst19 + tempMwst7;

                    val.SubItems.Add("-" + tempMwst.ToString());
                    tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) - tempMwst).ToString();
         
                    MessageBox.Show(tempVar.ToString());
                      
                    lvBestellung.Items.Add(val);
                    if (tbGesamt.Text != "")
                        tbGesamt.Text = String.Format("{0:0.00}", (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(tempVar)).ToString());
                    else
                        tbGesamt.Text = tempVar.ToString();
                    valueremoved -= tempVar;
                    label6.Text = valueremoved.ToString();
                    //   tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) - tempMwst).ToString();
                    txtRabatt.Text = ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
                    txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(txtRabatt.Text)).ToString();
                    
               button1.Enabled = true;
               PerformClear();


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
        private void AddZutat()
        {
            double zutatmwst = 0,zutatprice=0;

            if (conn.State.ToString() == "Closed")
                conn.Open();
            
            string sql = "Select * from dbbari.Zutaten where ZutatName='" + selectedZutat + "';";
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

                double tempVar;
                double tempMwst, tempMwst7, tempMwst19;
                ListViewItem val = new ListViewItem("+");
                val.SubItems.Add(rdr["ZutatName"].ToString());
                val.SubItems.Add("1");
                tbMwSt.Text = rdr["Mwst"].ToString();
                tempVar = Convert.ToDouble(rdr["Preis"].ToString());

              
                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());

                if (tbMwSt.Text == "7")
                {
                    tempMwst7 = Math.Round(tempVar * 0.07, 3);
                    tempMwst19 = 0;
                }
                else if (tbMwSt.Text == "19")
                {
                    tempMwst19 = Math.Round(tempVar * 0.19, 3);
                    tempMwst7 = 0;
                }
                else
                {
                    tempMwst = 0;
                    tempMwst19 = 0;
                    tempMwst7 = 0;
                }

                tempMwst = tempMwst19 + tempMwst7;
                val.SubItems.Add(tempMwst.ToString());
                string previousArtikelNo=lvBestellung.Items[lvBestellung.Items.Count - 1].SubItems[0].Text;
                bool isPrevioisArtiklePfand = getIsPrevioisArtiklePfand();
               // MessageBox.Show(previousArtikelNo);
                if ((previousArtikelNo != "+" & previousArtikelNo != "-" & previousArtikelNo != "+-") | isPrevioisArtiklePfand)
                {
                    
                    //Create new Object
                    obj = new Zutaten();
                    obj.addZutat(rdr["ZutatName"].ToString(), tempVar, tempMwst);
                    lvBestellung.Items.Add(val);
                    val = new ListViewItem("+-");
                    val.SubItems.Add("Zutaten");
                    val.SubItems.Add("");
                    val.SubItems.Add(obj.getValueadded().ToString());
                    val.SubItems.Add(obj.getValueadded().ToString());
                    val.SubItems.Add(obj.getMwst().ToString());
                    zutatprice = obj.getValueadded();
                    zutatmwst = obj.getMwst(); //    tbMwSt.Text = rdr["Mwst"].ToString();
                    lvBestellung.Items.Add(val);
                    

                }
                else
                {

                    obj.addZutat(rdr["ZutatName"].ToString(), tempVar, tempMwst);

                    lvBestellung.Items[lvBestellung.Items.Count - 1] = val;
                    val = new ListViewItem("+-");
                    val.SubItems.Add("Zutaten");
                    val.SubItems.Add("");
                    val.SubItems.Add("");
                    val.SubItems.Add(obj.getValueadded().ToString());
                    val.SubItems.Add(obj.getMwst().ToString());
                    zutatmwst = obj.getMwst();
                    zutatprice = obj.getValueadded();
                    //    tbMwSt.Text = rdr["Mwst"].ToString();
                    lvBestellung.Items.Add(val);
                
                }
                if (tbGesamt.Text != "")
                    tbGesamt.Text = String.Format("{0:0.00}", (grundpreis + zutatprice).ToString());
                else
                    tbGesamt.Text = zutatprice.ToString();

                txtRabatt.Text = ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
             
                tbTotalMwst.Text = (grundmwst + zutatmwst).ToString();
                button1.Enabled = true;
                PerformClear();

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
        private bool getIsPrevioisArtiklePfand()
        {
            //MessageBox.Show(lvBestellung.Items[lvBestellung.Items.Count - 1].SubItems[1].Text);
            if (lvBestellung.Items[lvBestellung.Items.Count - 1].SubItems[1].Text == "pfand")
                return true;
            else
            return false;
        }
        private void RemoveZutat()
        {
            double zutatmwst = 0,zutatprice=0;
            if (conn.State.ToString() == "Closed")
                conn.Open();

            string sql = "Select * from dbbari.Zutaten where ZutatName='" + selectedZutat + "';";
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

                double tempVar;
                double tempMwst, tempMwst7, tempMwst19;
                ListViewItem val = new ListViewItem("-");
                val.SubItems.Add(rdr["ZutatName"].ToString());
                val.SubItems.Add("1");
                tbMwSt.Text = rdr["Mwst"].ToString();
                tempVar = Convert.ToDouble(rdr["Preis"].ToString());


                val.SubItems.Add(tempVar.ToString());
                val.SubItems.Add(tempVar.ToString());

                if (tbMwSt.Text == "7")
                {
                    tempMwst7 = Math.Round(tempVar * 0.07, 3);
                    tempMwst19 = 0;
                }
                else if (tbMwSt.Text == "19")
                {
                    tempMwst19 = Math.Round(tempVar * 0.19, 3);
                    tempMwst7 = 0;
                }
                else
                {
                    tempMwst = 0;
                    tempMwst19 = 0;
                    tempMwst7 = 0;
                }

                tempMwst = tempMwst19 + tempMwst7;
                val.SubItems.Add(tempMwst.ToString());
                string previousArtikelNo = lvBestellung.Items[lvBestellung.Items.Count - 1].SubItems[0].Text;
                if (previousArtikelNo != "+" & previousArtikelNo != "-" & previousArtikelNo != "+-")
                {
                    //Create new Object
                    obj = new Zutaten();
                    obj.removeZutat(rdr["ZutatName"].ToString(), tempVar, tempMwst);
                    lvBestellung.Items.Add(val);
                    val = new ListViewItem("+-");
                    val.SubItems.Add("Zutaten");
                    val.SubItems.Add("");
                    val.SubItems.Add(obj.getValueadded().ToString());
                    val.SubItems.Add(obj.getValueadded().ToString());
                    val.SubItems.Add(obj.getMwst().ToString());
                    zutatprice = obj.getValueadded();
                    zutatmwst = obj.getMwst(); 
                    lvBestellung.Items.Add(val);


                }
                else
                {
                    obj.removeZutat(rdr["ZutatName"].ToString(), tempVar, tempMwst);

                    lvBestellung.Items[lvBestellung.Items.Count - 1] = val;
                    val = new ListViewItem("+-");
                    val.SubItems.Add("Zutaten");
                    val.SubItems.Add("");
                    val.SubItems.Add("");
                    val.SubItems.Add(obj.getValueadded().ToString());
                    val.SubItems.Add(obj.getMwst().ToString());
                    zutatprice = obj.getValueadded();
                    zutatmwst = obj.getMwst(); 
                    lvBestellung.Items.Add(val);

                }
                if (tbGesamt.Text != "")
                    tbGesamt.Text = String.Format("{0:0.00}", (grundpreis +zutatprice).ToString());
                else
                    tbGesamt.Text = zutatprice.ToString();

               

            
                tbTotalMwst.Text = (grundmwst + zutatmwst).ToString();
                button1.Enabled = true;
                PerformClear();

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
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                tbArtikel.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                AddZutat();
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                AddZutat();
                          }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
          {      
                RemoveZutat();   
            }
            else if (e.KeyCode == Keys.Escape)
            {
                tbArtikel.Focus();
            }
            else if (e.KeyCode == Keys.Right)
                tbArtikel.Focus();
            else if (e.KeyCode == Keys.Left)
                lvArtikel.Focus();
        }

        

     

        private void lvBestellung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
              
                tbArtikel.Focus();
            }
            else if (e.KeyCode == Keys.F7)
            {
                button5.PerformClick();
               /* ListView.SelectedListViewItemCollection items = lvBestellung.SelectedItems;
                foreach (ListViewItem item in items)
                {
                    try
                    {
                    // decrease Mwst 7 Mwst 19 gesamt 
                    double ItemPreis=Math.Round(Convert.ToDouble(item.SubItems[4].Text),3);
                    double itemMwst=Math.Round(Convert.ToDouble(item.SubItems[5].Text),3);
                    double itemMwst7 = Math.Round(ItemPreis * 0.07);
                    double itemMwst19 = Math.Round(ItemPreis * 0.19,3);
                    double totalMwst=Math.Round(Convert.ToDouble(tbTotalMwst.Text),3);
                    //MessageBox.Show((ItemPreis * 0.07).ToString());
                    tbGesamt.Text = (Convert.ToDouble(tbGesamt.Text) - ItemPreis).ToString();
                    // Test if Mwst is 19% or 7%
                    if (itemMwst == itemMwst7) // 7%
                        TotalMwst7 = TotalMwst7 - itemMwst;
                    else if (itemMwst == itemMwst19) //19%
                        TotalMwst19 -= itemMwst;
                    
                    tbTotalMwst.Text = (totalMwst - itemMwst).ToString();
                    
                        item.Remove();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }*/
                   
            }
        }

   

        private void button5_Click(object sender, EventArgs e) // delete selected from the list
        {

            ListView.SelectedListViewItemCollection items = lvBestellung.SelectedItems;
            foreach (ListViewItem item in items)
            {
                
                string nextvalue;    
               
                    try
                    {
                        nextvalue = lvBestellung.Items[item.Index + 1].SubItems[0].Text;
                    }
                    catch {
                        nextvalue = "";
                    }
                try
                {
                    // Artikel ohne Zutaten
                    if (item.SubItems[0].Text != "+" & item.SubItems[0].Text != "-" & item.SubItems[0].Text != "+-" & nextvalue != "+" & nextvalue != "-" & nextvalue != "+-")
                    {
                        double ItemPreis = Math.Round(Convert.ToDouble(item.SubItems[4].Text), 3);
                        double itemMwst = Math.Round(Convert.ToDouble(item.SubItems[5].Text), 3);
                        double itemMwst7 = Math.Round(ItemPreis * 0.07, 3);
                        double itemMwst19 = Math.Round(ItemPreis * 0.19, 3);
                        double totalMwst = Math.Round(Convert.ToDouble(tbTotalMwst.Text), 3);
                        //MessageBox.Show((ItemPreis * 0.07).ToString());
                        tbGesamt.Text = (Convert.ToDouble(tbGesamt.Text) - ItemPreis).ToString();
                        txtRabatt.Text = ((Convert.ToDouble(tbGesamt.Text) - (AnfahrtKosten + gesamt_pfand)) * Rabbatt / 100).ToString();
                        txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - Convert.ToDouble(txtRabatt.Text)).ToString();
            //marketinng
                      //  txtDifference.Text = (Convert.ToDouble(tbGesamt.Text) - (ItemPreis-(ItemPreis/10))).ToString();
                        // Test if Mwst is 19% or 7%
                        if (itemMwst == itemMwst7) // 7%
                            TotalMwst7 = TotalMwst7 - itemMwst;
                        else if (itemMwst == itemMwst19) //19%
                            TotalMwst19 -= itemMwst;

                        tbTotalMwst.Text = (totalMwst - itemMwst).ToString();

                        item.Remove();
                    if(item.Index<currentIndex)
                        currentIndex -= 1;
        
                    }
                    else
                    {
                       // find current zutaten preis
                     double difference=  Recalculate(item.Index);
                     tbGesamt.Text = (Convert.ToDouble(tbGesamt.Text) - difference).ToString();
                     tbTotalMwst.Text = (Convert.ToDouble(tbTotalMwst.Text) - restmwst).ToString();//

               /*         // remove 
                     tbGesamt.Text = (Convert.ToDouble(tbGesamt.Text) - difference).ToString();
                     double ItemPreis = Math.Round(Convert.ToDouble(item.SubItems[4].Text), 3);
                     double itemMwst = Math.Round(Convert.ToDouble(item.SubItems[5].Text), 3);
                     double itemMwst7 = Math.Round(ItemPreis * 0.07, 3);
                     double itemMwst19 = Math.Round(ItemPreis * 0.19, 3);
                     double totalMwst = Math.Round(Convert.ToDouble(tbTotalMwst.Text), 3);
                     if (itemMwst == itemMwst7) // 7%
                         TotalMwst7 = TotalMwst7 - itemMwst;
                     else if (itemMwst == itemMwst19) //19%
                         TotalMwst19 -= itemMwst;

                     tbTotalMwst.Text = (totalMwst - itemMwst).ToString();

                     item.Remove();
                 */       //MessageBox.Show((ItemPreis * 0.07).ToString());
                          
                        //recalculate zutaten preis

                        //subtract the difference from tbgesamt
                        if ( item.Index>=currentIndex)
                            MessageBox.Show("Current Item");

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private double Recalculate(int current) // TODO:: Why recalculate some intelligent Method is needed
        {
            double oldBetrag = 0, oldmwst = 0;
            if (lvBestellung.Items[current].SubItems[0].Text == "+" || lvBestellung.Items[current].SubItems[0].Text == "-")// zutate gelöscht mit +
            {
                double zutatprice=Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text);
                lvBestellung.Items[current].Remove();
                bool check = true;
              while(check) // goes down to zutaten and saves oldbetrag        
              {
               
                      if (lvBestellung.Items[current].SubItems[0].Text == "+-" )
                    {
                        oldBetrag = Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text);
                        oldmwst = Convert.ToDouble(lvBestellung.Items[current].SubItems[5].Text);
                          check = false;  
                    }
                   else
                          current++;
                }
                
              current--;
              int zutatencount = 0;
              for (int i = current; i > 0; i--)// go again up to artikel
              {
                  if (lvBestellung.Items[current].SubItems[0].Text == "+" || lvBestellung.Items[current].SubItems[0].Text == "-")
                  {
                      zutatencount++;
                      current--;
                  }

              }
                current++;// go to first zutat
              //recalculate
          
              obj = new Zutaten();
              for (int i = 0; i <= zutatencount; i++)
              {
                  if (lvBestellung.Items[current].SubItems[0].Text == "+")
                  {
                      obj.addZutat(lvBestellung.Items[current].SubItems[1].Text, Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text), Convert.ToDouble(lvBestellung.Items[current].SubItems[5].Text));
                      current++;
                  }
                  else if (lvBestellung.Items[current].SubItems[0].Text == "-")
                  {
                      obj.removeZutat(lvBestellung.Items[current].SubItems[1].Text, Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text), Convert.ToDouble(lvBestellung.Items[current].SubItems[5].Text));
                      current++;
                  }
                  else if (lvBestellung.Items[current].SubItems[0].Text == "+-")
                  {
                      lvBestellung.Items[current].SubItems[4].Text=obj.getValueadded().ToString();
                      lvBestellung.Items[current].SubItems[5].Text = obj.getMwst().ToString();     
                      
                  }
              }
              restmwst = oldmwst - obj.getMwst();
                return oldBetrag - obj.getValueadded();
             
            }

            else if (lvBestellung.Items[current].SubItems[0].Text == "+-") // alle zutaten löschen
            {
                oldBetrag = Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text);
                lvBestellung.Items[current].Remove();
                current--;
                for (int i = current; i > 0; i--)
                {
                    if (lvBestellung.Items[current].SubItems[0].Text == "+" || lvBestellung.Items[current].SubItems[0].Text == "-")
                    {
                        lvBestellung.Items[current].Remove();
                        current--;
                    }

                }
                return oldBetrag;
            }
            else // gesamt Item löschen
            {
                oldBetrag = Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text);
                restmwst = Convert.ToDouble(lvBestellung.Items[current].SubItems[5].Text);
                lvBestellung.Items[current].Remove();
                
                for (int i = current; i > 0; i--)
                {
                    if (lvBestellung.Items[current].SubItems[0].Text == "+" || lvBestellung.Items[current].SubItems[0].Text == "-")
                    {
                        lvBestellung.Items[current].Remove();
                        
                    }
                    else if (lvBestellung.Items[current].SubItems[0].Text == "+-")
                    {
                        oldBetrag += Convert.ToDouble(lvBestellung.Items[current].SubItems[4].Text);
                        restmwst += Convert.ToDouble(lvBestellung.Items[current].SubItems[5].Text);
                        lvBestellung.Items[current].Remove();
                        break;    
                    }
                    
                }
                return oldBetrag;
            }
            

                
        }
       
        private void lvBestellung_GotFocus(object sender, EventArgs e)
        {
            panel1.BackColor = SystemColors.Highlight;
        }
        private void lvBestellung_LostFocus(object sender, EventArgs e)
        {
            panel1.BackColor = SystemColors.Control;
        }

        private void lvBestellung_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        

        
        
        
    }
}