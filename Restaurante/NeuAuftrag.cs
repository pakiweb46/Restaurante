﻿using System;
using System.Drawing;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Restaurante

{
    public partial class NeuAuftrag : Form
    {
        DialogResult NeuKunde; // Hold if Neu Kunde Speichern or not        
        public string kundenreference;
        //KundeHolder obj_kunde = new KundeHolder();
      
        private string CustomerId;// Contains the current idKundendaten
        private bool isDataComplete=true;//ob kundendaten sind vollständig

       
        MySqlConnection conn = new MySqlConnection(Globals.connString);
        MySqlConnection conn1 = new MySqlConnection(Globals.connString);

        RestauranteData rData;

        public int recordNr, recordCount;
        
   
        public string SelectedKunde;
        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt64(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public NeuAuftrag()
        {
            InitializeComponent();
            rData = new RestauranteData();
            recordNr = 0;

        }
       
        private void UpdateChanges(string Knummer)
        {
            
            try
            {
                rData.openReadConnection();
                MySqlDataReader readerKunde = rData.getKundenDaten("idkundendaten", Knummer );
                if ( readerKunde.Read() )
                {
              
                  if (tbAnfahrt.Text != readerKunde["Anfahrtkosten"].ToString())
                        rData.updateAnfahrtKosten(Knummer, Convert.ToDouble(tbAnfahrt.Text.Trim()));                
                  if (tbName.Text != readerKunde["KundenName"].ToString())
                        rData.updateKundenName(Knummer, tbName.Text.Trim());
                  if (tbOrt.Text != readerKunde["Ort"].ToString())
                        rData.updateOrt(Knummer,tbOrt.Text.Trim());
                  if (tbPLZ.Text != readerKunde["PLZ"].ToString())
                        rData.updatePLZ(Knummer, Convert.ToInt64(tbPLZ.Text.Trim()));
                  if (tbStrasse.Text != readerKunde["Strasse"].ToString())
                        rData.updateStrasse(Knummer, tbStrasse.Text.Trim());
                  if (tbStrNo.Text != readerKunde["StrNo"].ToString())
                        rData.updateSNo(Knummer, tbStrNo.Text.Trim());
                  if (tbTelefon.Text != readerKunde["KundenNr"].ToString())
                        rData.updateKundenNummer(Knummer, tbTelefon.Text.Trim());
                  if (tbZusatz.Text != readerKunde["Zusatz"].ToString())
                        rData.updateZusatz(Knummer, tbZusatz.Text.Trim());
                  if (tbRabatt.Text != readerKunde["Rabatt"].ToString())
                        rData.updateRabatt(Knummer, Convert.ToDouble(tbRabatt.Text.Trim()));                 
                }
                readerKunde.Close();
                rData.closeReadConnection();
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // pass data to frmBestellung
            
                if (NeuKunde == DialogResult.No)
                    UpdateChanges(tbKNr.Text);
                if (NeuKunde == DialogResult.Yes)
                    Speicher_Daten();
                if (isDataComplete)
                {
                this.Hide();
                frmBestellung objBestellung = new frmBestellung(kundenreference);
                objBestellung.PerformListFill();
                objBestellung.ShowDialog();
                objBestellung.Close();
                this.Show();
                this.Focus();
                tbTelefon.Focus();
                btnWeiter.Enabled = false;
                btnfldLeeren.PerformClick();
                webBrowser1.Navigate("about:blank");
                tbTelefon.Focus();
            }

        }

        private void NeuAuftrag_Load(object sender, EventArgs e)
        {
            btnMapDrucken.Hide();
            tbKNr.Enabled = true;
            tbAnfahrt.Text = "0";
            tbRabatt.Text = "0";
            tbDatum.Text = System.DateTime.Now.ToShortDateString();
            tbDatum.Enabled = false;
            btnWeiter.Enabled = false;
            textBox14.Visible = false;
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(NeuAuftrag_KeyDown);
            tbStrasse.AutoCompleteCustomSource = StrassenVonDatenBank();
            lwKundenDaten.Visible = false;  
            tbTelefon.Focus();
        }
        private void NeuAuftrag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btnWeiter.Enabled == true)
            {
                
                    btnWeiter.PerformClick();

            }
            else if (e.KeyCode == Keys.F10)
            {
                btnSpeichern.PerformClick(); // Speichern

            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (lwKundenDaten.Visible)
                {
                    for (int i = lwKundenDaten.Items.Count; i > 0; i--)
                        lwKundenDaten.Items.Remove(lwKundenDaten.Items[0]);
                   lwKundenDaten.Visible = false;
                }
                else
                      btnfldLeeren.PerformClick();
                
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (isFormClear())
                    btnHauptmenu.PerformClick();
                else
                btnfldLeeren.PerformClick(); // Felder leeren
            }
            else if (e.KeyCode == Keys.F1)
            {
                SearchRecord();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.Hide();
                frmAbrechnung myAbrechnung = new frmAbrechnung();
                myAbrechnung.ShowDialog();
                myAbrechnung.Close();
                this.Show();
            }

        }
        private void PerformDataFill(MySqlDataReader reader)
        {

            CustomerId = reader["idKundendaten"].ToString();
            tbKNr.Text = CustomerId;
            kundenreference = reader["idKundendaten"].ToString();
            tbName.Text = reader["KundenName"].ToString();
            tbOrt.Text = reader["ort"].ToString();
            tbPLZ.Text = reader["PLZ"].ToString();
            tbStrasse.Text = reader["strasse"].ToString();
            tbStrNo.Text = reader["StrNo"].ToString();
            tbZusatz.Text = reader["zusatz"].ToString();
            tbTelefon.Text = reader["KundenNr"].ToString();
            tbRabatt.Text = reader["Rabatt"].ToString();
            tbAnfahrt.Text = reader["Anfahrtkosten"].ToString();
            SelectedKunde = kundenreference;
            btnWeiter.Enabled = true;
            btnWeiter.Focus();
            
        }
       
        private void SearchRecord()
        {
            // Code from KundenDaten
            NeuKunde = DialogResult.No;
            if (tbTelefon.Text != "" && tbTelefon.Focused) //if telefon is not empty and telefone is focused
            {
                int found = rData.getKundenCount("KundenNr",tbTelefon.Text.Trim() );

                if (found == 1) // One Record Found
                {
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.getKundenDaten("KundenNr",tbTelefon.Text.Trim());
                    if (readerKunde.Read())
                    {
                        PerformDataFill(readerKunde);
                        tbKNr.Enabled = false;
                    }
                    readerKunde.Close();
                    rData.closeReadConnection();
                }
                else if (found > 1) // More than One records found
                {
                    // Complete string match where more items have same name
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.getKundenDaten("KundenNr",tbTelefon.Text.Trim());
                    if (readerKunde.HasRows)
                    {
                        while (readerKunde.Read())
                        {
                            ListViewItem item = new ListViewItem(readerKunde["KundenNr"].ToString());
                            item.SubItems.Add(readerKunde["KundenName"].ToString());
                            item.SubItems.Add(readerKunde["idKundendaten"].ToString());
                            item.SubItems.Add(readerKunde["Strasse"].ToString() + "." + readerKunde["strno"].ToString());
                            lwKundenDaten.Items.Add(item);

                        }
                        lwKundenDaten.Visible = true;
                        lwKundenDaten.Enabled = true;
                    }
                     readerKunde.Close();
                     rData.closeReadConnection();
                    
                }
                else
                {
                    // not full matching found match String segments 
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.searchKundenDaten("KundenNr",tbTelefon.Text.Trim() + " % ");
                   
                    if (readerKunde.HasRows)
                    {

                        while (readerKunde.Read())
                        {
                            ListViewItem item = new ListViewItem(readerKunde["KundenNr"].ToString());
                            item.SubItems.Add(readerKunde["KundenName"].ToString());
                            item.SubItems.Add(readerKunde["idKundendaten"].ToString());
                            item.SubItems.Add(readerKunde["Strasse"].ToString() + "." + readerKunde["strno"].ToString());
                            lwKundenDaten.Items.Add(item);
                        }

                        lwKundenDaten.Visible = true;
                        lwKundenDaten.Enabled = true;
                    
                    }
                    else
                    {
                        NeuKunde = MessageBox.Show("Daten Nicht gefunden. Soll Neu Kunde eingefügt werden", "?", MessageBoxButtons.YesNo);
                        if (NeuKunde == DialogResult.Yes)
                        {
                            
                            tbName.Focus();
                            tbKNr.Enabled = false;
                            btnWeiter.Enabled = true;
                        }
                        lwKundenDaten.Visible = false;


                    }
                    readerKunde.Close();
                    rData.closeReadConnection();
                
                }

            }
            else if (tbKNr.Text!="" && tbKNr.Focused) // Kunden Nummer Search
            {
                rData.openReadConnection();
                MySqlDataReader readerKunde = rData.getKundenDaten("idkundendaten",tbKNr.Text);                               
                if (readerKunde.Read())
                {
                    PerformDataFill(readerKunde);
                }
                readerKunde.Close();
                rData.closeReadConnection();             
            }

            else if (tbName.Text != "" && tbName.Focused) // Name Search
            {
                int found = rData.getKundenCount("KundenName",tbName.Text.Trim());

                if (found == 1) // One Name Found
                {
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.getKundenDaten("KundenName",tbName.Text.Trim());


                    if (readerKunde.Read())
                    {
                        PerformDataFill(readerKunde);
                        tbKNr.Enabled = false;
                    }
                    readerKunde.Close();
                    rData.closeReadConnection();

                }
                else if (found > 1)
                {
                    // Complete string match where more items have same name
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.getKundenDaten("KundenName",tbName.Text.Trim());
                   
                    if (readerKunde.HasRows)
                    {
                        while (readerKunde.Read())
                        {
                            ListViewItem item = new ListViewItem(readerKunde["KundenNr"].ToString());
                            item.SubItems.Add(readerKunde["KundenName"].ToString());
                            item.SubItems.Add(readerKunde["idKundendaten"].ToString());
                            item.SubItems.Add(readerKunde["Strasse"].ToString() + "." + readerKunde["strno"].ToString());
                            lwKundenDaten.Items.Add(item);

                        }
                        lwKundenDaten.Visible = true;
                        lwKundenDaten.Enabled = true;
                        //listView1.Visible = false;
                    }
                     readerKunde.Close();
                     rData.closeReadConnection();
                    
                }
                else
                {
                    // not full matching found match String segments 
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.searchKundenDaten("KundenName",tbName.Text.Trim() + "%");
                    if (readerKunde.HasRows)
                    {

                        while (readerKunde.Read())
                        {
                            ListViewItem item = new ListViewItem(readerKunde["KundenNr"].ToString());
                            item.SubItems.Add(readerKunde["KundenName"].ToString());
                            item.SubItems.Add(readerKunde["idKundendaten"].ToString());
                            item.SubItems.Add(readerKunde["Strasse"].ToString() + "." + readerKunde["strno"].ToString());
                            lwKundenDaten.Items.Add(item);
                        }

                        lwKundenDaten.Visible = true;
                        lwKundenDaten.Enabled = true;

                    }
                    else
                    {
                        NeuKunde = MessageBox.Show("Daten Nicht gefunden. Soll Neu Kunde eingefügt werden", "?", MessageBoxButtons.YesNo);
                        if (NeuKunde == DialogResult.Yes)
                        {
                            tbTelefon.Focus();
                            tbKNr.Enabled = false;
                            btnWeiter.Enabled = true;
                        }
                        lwKundenDaten.Visible = false;
                        //listView1.Visible = true;
                    }
                    readerKunde.Close();
                    rData.closeReadConnection();
                }

            }
            else if (tbName.Focused)
            {
                MessageBox.Show("Für Suchen Bitte geben sie Name ein");

            }
            else if (tbTelefon.Focused)
            {
                MessageBox.Show("Für Suchen Bitte geben sie Anfangsziffern ein");

            }
        }
        private void lwKundenDaten_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lwKundenDaten.Visible == true)
            {

                ListView.SelectedListViewItemCollection selecteditem =
                    this.lwKundenDaten.SelectedItems;


                foreach (ListViewItem item in selecteditem)
                {

                    recordNr = Convert.ToInt32(item.SubItems[2].Text);

                }
                rData.openReadConnection();
                MySqlDataReader readerKunde = rData.getKundenDaten("idkundendaten",Convert.ToString(recordNr));
                //Search nach kunden Id and fill form
                if (readerKunde.Read())
                {
                    PerformDataFill( readerKunde );
                    tbKNr.Enabled = false;
                }
                readerKunde.Close();
                rData.closeReadConnection();
            
                lwKundenDaten.Items.Clear();
                lwKundenDaten.Visible = false;
                lwKundenDaten.Enabled = false;
                btnWeiter.Focus();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbTelefon_KeyDown(object sender, KeyEventArgs e)
        {
            bool containsLetter = false;
            if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Tab)
            {


                {
                    containsLetter = false;
                    for (int i = 0; i < tbTelefon.Text.Length; i++)
                    {
                        if (!char.IsNumber(tbTelefon.Text[i]))
                            containsLetter = true;
                    }
                    if (containsLetter)
                    {
                        MessageBox.Show("Es Sollen Nur Ziffern Sein");
                        tbTelefon.Focus();
                    }

                }

            }
            if (e.KeyCode == Keys.Down)
                tbKNr.Focus();
            if (e.KeyCode == Keys.Up)
                tbRabatt.Focus();
        

            

        }

        private AutoCompleteStringCollection StrassenVonDatenBank()
        {
            AutoCompleteStringCollection colValues = new AutoCompleteStringCollection();
            rData.openReadConnection();
            MySqlDataReader readerStadtPlan = rData.getAllStadtPlan();
            while (readerStadtPlan.Read())
            {
                colValues.Add(readerStadtPlan["strasse"].ToString());

            }
            readerStadtPlan.Close();
            rData.closeReadConnection();
            return colValues;

        }
        private void myClearForm()
        {
            tbKNr.Text = "";
            tbKNr.Enabled = true;
            tbName.Text = "";
            tbName.Enabled = true;
            tbOrt.Text = "";
            tbOrt.Enabled = true;
            tbPLZ.Text = "";
            tbPLZ.Enabled = true;
            tbStrasse.Text = "";
            tbStrasse.Enabled = true;
            tbStrNo.Text = "";
            tbAnfahrt.Text = "0";
            tbRabatt.Text = "0";
            tbStrNo.Enabled = true;
            tbTelefon.Text = "";
            tbTelefon.Enabled = true;
            tbZusatz.Text = "";
            tbZusatz.Enabled = true;
            tbTelefon.Focus();

         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myClearForm();
        }
        private void tbStrasse_GotFocus(object sender, EventArgs e)
        {
            panelStrasse.BackColor = SystemColors.Highlight;
        }
        private void tbStrasse_LostFocus(object sender, EventArgs e)
        {
            panelStrasse.BackColor = SystemColors.Control;
            rData.openReadConnection();
            MySqlDataReader readerStrasse = rData.getStadtPlan("strasse",tbStrasse.Text.Trim());
            if (readerStrasse.Read())
            {

                tbOrt.Text = readerStrasse["ort"].ToString();
                tbPLZ.Text = readerStrasse["PLZ"].ToString();

            }
            readerStrasse.Close();
            rData.closeReadConnection();
        }
        private void Speicher_Daten()
    {
        if (NeuKunde == DialogResult.Yes)
        {
            if (tbTelefon.Text == "")
            {
                MessageBox.Show("Bitte geben sie die Telfone Nummer");
                tbTelefon.Focus();
                isDataComplete = false;
            }
            else if (tbName.Text == "")
            {
                MessageBox.Show("Bitte geben sie die Kunden Name");
                tbName.Focus();
                isDataComplete = false;
            }
            else if (tbStrNo.Text == "")
            {
                MessageBox.Show("Bitte Haus Nummer Angeben");
                tbStrNo.Focus();
                isDataComplete = false;
            }
            else if (tbStrasse.Text == "")
            {
                MessageBox.Show("Bitte geben sie Straße Ein");
                tbStrasse.Focus();
                isDataComplete = false;
            }
            else if (tbOrt.Text == "")
            {
                MessageBox.Show("Bitte geben sie den Ort");
                tbOrt.Focus();
                isDataComplete = false;
            }
            else if (tbPLZ.Text == "" || !IsInteger(tbPLZ.Text))
            {
                MessageBox.Show("Postleitzahl soll ein Nummer sein");
                tbPLZ.Focus();
                isDataComplete = false;
            }
            else if (tbAnfahrt.Text == "" || !IsInteger(tbPLZ.Text))
            {
                MessageBox.Show("Anfahrt soll ein Nummer sein");
                tbAnfahrt.Focus();
                isDataComplete = false;
            }
            else if (tbRabatt.Text == "" || !IsInteger(tbPLZ.Text))
            {
                MessageBox.Show("Rabatt soll ein Nummer sein");
                tbRabatt.Focus();
                isDataComplete = false;
            }


            else
            {

                MySqlCommand cmd1 = new MySqlCommand();

                try
                {

                    cmd1.Connection = conn;
                    cmd1.Parameters.Clear();
                    cmd1.CommandText = "INSERT INTO dbbari.kundendaten VALUES (NULL, @KundenNr , @Anrede, @KundenName, @Strasse, @StrNo,  @zusatz, @PLZ, @Ort, @Anfahrtkosten, @Rabatt)";
                    cmd1.Prepare();
                    cmd1.Parameters.AddWithValue("KundenNr", tbTelefon.Text.Trim());
                    cmd1.Parameters.AddWithValue("Anrede", "Herrn");
                    cmd1.Parameters.AddWithValue("KundenName", tbName.Text.Trim());
                    cmd1.Parameters.AddWithValue("Strasse", tbStrasse.Text.Trim());
                    cmd1.Parameters.AddWithValue("StrNo", tbStrNo.Text.Trim());
                    cmd1.Parameters.AddWithValue("zusatz", tbZusatz.Text.Trim());
                    cmd1.Parameters.AddWithValue("PLZ", Convert.ToInt64(tbPLZ.Text.Trim()));
                    cmd1.Parameters.AddWithValue("Ort", tbOrt.Text.Trim());
                    cmd1.Parameters.AddWithValue("Anfahrtkosten", Convert.ToDouble(tbAnfahrt.Text.Trim()));
                    cmd1.Parameters.AddWithValue("Rabatt", Convert.ToDouble(tbRabatt.Text.Trim()));
                            
                    cmd1.ExecuteNonQuery();
                    cmd1.CommandText = "select last_insert_id()";
                    kundenreference = Convert.ToInt32(cmd1.ExecuteScalar()).ToString();
                    NeuKunde = DialogResult.No;
                    MessageBox.Show("Neu daten sind gespeichert");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                isDataComplete = true;
            }
        }
        else if (tbTelefon.Text == "")
        {
            MessageBox.Show("Kunden Information ist Leer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NeuKunde == DialogResult.No)
            {
                UpdateChanges(tbKNr.Text);
                MessageBox.Show("Änderungen sind Gespeichert");
            }
            if (NeuKunde == DialogResult.Yes)
                Speicher_Daten();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                if (tbStrasse.Text != "" & System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    if (checkBox1.Checked)
                    {
                    //  webBrowser1.Navigate("http://www.einkauf-agent.de/gmaps.htm?Ziel=" + tbStrasse.Text + " " + tbStrNo.Text + " Hannover");
                       if(tbOrt.Text.Contains("Groß Buchholz"))
                           webBrowser1.Navigate(Application.StartupPath.ToString() + "\\Resources\\Gmaps.htm?Ziel=" + tbStrasse.Text + " " + tbStrNo.Text +" " +tbPLZ.Text+ " Hannover Groß Buchholz");
                        
                        else if (tbOrt.Text.Contains("Hannover"))
                        {
                            webBrowser1.Navigate(Application.StartupPath.ToString() + "\\Resources\\Gmaps.htm?Ziel=" + tbStrasse.Text + " " + tbStrNo.Text + " Hannover");
                        }
                        else if (tbOrt.Text.Contains("Langenhagen"))
                        {
                            string strasssse = tbStrasse.Text.Replace("(LGH)", "");

                            webBrowser1.Navigate(Application.StartupPath.ToString() + "\\Resources\\Gmaps.htm?Ziel=" + strasssse + " " + tbStrNo.Text + " Langenhagen");
                        }
                           btnMapDrucken.Show();
                    }
                    else
                    {
                        webBrowser1.Navigate("about:blank");
                        btnMapDrucken.Hide();
                    }
                else if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == false)
                    MessageBox.Show("Sie benötigen ein Internetfähiges Rechner für Google Maps");
                else if (tbStrasse.Text == "")
                    MessageBox.Show("Bitte Geben sie eine Strasse ein ");
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
           /* this.Close();
            TestForm rpt = new TestForm();
            rpt.ShowDialog();
            */
            string[] all = System.Reflection.Assembly.GetEntryAssembly().
                GetManifestResourceNames();

            foreach (string one in all)
            {
                MessageBox.Show(one);
            }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        // tbTelefone
        private void tbTelefone_LostFocus(object sender, EventArgs e)
        {
            panelTelefon.BackColor = SystemColors.Control;

        }


        private void tbTelefon_GotFocus(object sender, EventArgs e)
        {
            panelTelefon.BackColor = SystemColors.Highlight;  
        }
        // keydownevent is implemented above

        //tbKNr
        private void tbKNr_GotFocus(object sender, EventArgs e)
        {
            panelKNr.BackColor = SystemColors.Highlight;
        }
        private void tbKNr_LostFocus(object sender, EventArgs e)
        {
            panelKNr.BackColor = SystemColors.Control;
        }
        private void tbKNr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbName.Focus();
            if (e.KeyCode == Keys.Up)
                tbTelefon.Focus();
        }
        //tbName
        private void tbName_GotFocus(object sender, EventArgs e)
        {
            panelName.BackColor = SystemColors.Highlight;
        }
        private void tbName_LostFocus(object sender, EventArgs e)
        {
            panelName.BackColor = SystemColors.Control;
        }
        private bool isFormClear()
        {
           
            if (tbTelefon.Text == "" & tbZusatz.Text == "" & tbStrNo.Text == "" & tbStrasse.Text == ""  & tbPLZ.Text == "" & tbOrt.Text == "" & tbName.Text == "" & tbKNr.Text == "")
                return true;
            else
                return false;
        }
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbStrasse.Focus();
            if (e.KeyCode == Keys.Up)
                tbKNr.Focus();
        }
        //tbstrasse keydown
        private void tbStrasse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbPLZ.Focus();
            if (e.KeyCode == Keys.Up)
                tbName.Focus();
            if (e.KeyCode == Keys.Tab)
                tbStrNo.Focus();
        }
        
        //tbStrNo
        private void tbStrNo_GotFocus(object sender, EventArgs e)
        {
            panelStrNo.BackColor = SystemColors.Highlight;
        }
        private void tbStrNo_LostFocus(object sender, EventArgs e)
        {
            panelStrNo.BackColor = SystemColors.Control;
        }
        private void tbStrNo_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Down)
                tbPLZ.Focus();
            if (e.KeyCode == Keys.Up)
                tbStrasse.Focus();
                
        }

        //tbPLZ
        private void tbPLZ_GotFocus(object sender, EventArgs e)
        {
            panelPLZ.BackColor = SystemColors.Highlight;
        }
        private void tbPLZ_LostFocus(object sender, EventArgs e)
        {
            panelPLZ.BackColor = SystemColors.Control;
        }
        private void tbPLZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                tbStrNo.Focus();
            if (e.KeyCode == Keys.Right)
                tbOrt.Focus();
            if (e.KeyCode == Keys.Down)
                tbZusatz.Focus();
            if (e.KeyCode == Keys.Up)
                tbStrasse.Focus();
        }
        //tbOrt
        private void tbOrt_GotFocus(object sender, EventArgs e)
        {
            panelOrt.BackColor = SystemColors.Highlight;
        }
        private void tbOrt_LostFocus(object sender, EventArgs e)
        {
            panelOrt.BackColor = SystemColors.Control;
        }
        private void tbOrt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                tbPLZ.Focus();
            if (e.KeyCode == Keys.Right)
                tbZusatz.Focus();
            if (e.KeyCode == Keys.Down)
                tbZusatz.Focus();
            if (e.KeyCode == Keys.Up)
                tbStrNo.Focus();
        }
        //tbHimweis
        private void tbZusatz_GotFocus(object sender, EventArgs e)
        {
            panelZusatz.BackColor = SystemColors.Highlight;
        }
        private void tbZusatz_LostFocus(object sender, EventArgs e)
        {

            panelZusatz.BackColor = SystemColors.Control;
      
        }
        private void tbZusatz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbAnfahrt.Focus();
            if (e.KeyCode == Keys.Up)
                tbPLZ.Focus();
        }
        //tbAnfahrt
        private void tbAnfahrt_GotFocus(object sender, EventArgs e)
        {
            panelAnfahrt.BackColor = SystemColors.Highlight;
        }
        private void tbAnfahrt_LostFocus(object sender, EventArgs e)
        {
            panelAnfahrt.BackColor = SystemColors.Control;
        }
        private void tbAnfahrt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                tbZusatz.Focus();
            if (e.KeyCode == Keys.Right)
                tbRabatt.Focus();
            if (e.KeyCode == Keys.Down)
                tbTelefon.Focus();
            if (e.KeyCode == Keys.Up)
                tbZusatz.Focus();
        }
        //tbRabatt
        private void tbRabatt_GotFocus(object sender, EventArgs e)
        {
            panelRabatt.BackColor = SystemColors.Highlight;
        }
        private void tbRabatt_LostFocus(object sender, EventArgs e)
        {
            panelRabatt.BackColor = SystemColors.Control;
        }
        private void tbRabatt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                tbAnfahrt.Focus();
            if (e.KeyCode == Keys.Right)
                tbTelefon.Focus();
            if (e.KeyCode == Keys.Down)
                tbTelefon.Focus();
            if (e.KeyCode == Keys.Up)
                tbZusatz.Focus();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Print();
        }


    }
}
