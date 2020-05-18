using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Restaurante

{
    public partial class NeuAuftrag : Form
    {
        public string kundenreference;
        public int recordNr, recordCount;
        public string SelectedKunde;
        private MySqlConnection conn = new MySqlConnection(Globals.connString);
        private MySqlConnection conn1 = new MySqlConnection(Globals.connString);
        private string CustomerId;

        // Contains the current idKundendaten
        private bool isDataComplete = true;

        private DialogResult NeuKunde; // Hold if Neu Kunde Speichern or not
                                       //KundeHolder obj_kunde = new KundeHolder();

        //ob kundendaten sind vollständig
        private RestauranteData rData;
        private IFormatProvider providerEn;

        public NeuAuftrag()
        {
            InitializeComponent();
            rData = new RestauranteData();
            providerEn = CultureInfo.CreateSpecificCulture("en-GB");
            recordNr = 0;
        }

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

        private void btnWeiter_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            myClearForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Print();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (tbStrasse.Text != "" & System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                if (checkBox1.Checked)
                {
                    //  webBrowser1.Navigate("http://www.einkauf-agent.de/gmaps.htm?Ziel=" + tbStrasse.Text + " " + tbStrNo.Text + " Hannover");
                    if (tbOrt.Text.Contains("Groß Buchholz"))
                        webBrowser1.Navigate(Application.StartupPath.ToString() + "\\Resources\\Gmaps.htm?Ziel=" + tbStrasse.Text + " " + tbStrNo.Text + " " + tbPLZ.Text + " Hannover Groß Buchholz");
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

        private bool isFormClear()
        {
            if (tbTelefon.Text == "" & tbZusatz.Text == "" & tbStrNo.Text == "" & tbStrasse.Text == "" & tbPLZ.Text == "" & tbOrt.Text == "" & tbName.Text == "" & tbKNr.Text == "")
                return true;
            else
                return false;
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
                MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "idkundendaten", Convert.ToString(recordNr));
                //Search nach kunden Id and fill form
                if (readerKunde.Read())
                {
                    PerformDataFill(readerKunde);
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
                int found = rData.getCount("kundendaten", "KundenNr", tbTelefon.Text.Trim());

                if (found == 1) // One Record Found
                {
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "KundenNr", tbTelefon.Text.Trim());
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
                    MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "KundenNr", tbTelefon.Text.Trim());
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
                    MySqlDataReader readerKunde = rData.searchDaten("kundendaten", "KundenNr", tbTelefon.Text.Trim() + " % ");

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
            else if (tbKNr.Text != "" && tbKNr.Focused) // Kunden Nummer Search
            {
                rData.openReadConnection();
                MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "idkundendaten", tbKNr.Text);
                if (readerKunde.Read())
                {
                    PerformDataFill(readerKunde);
                }
                readerKunde.Close();
                rData.closeReadConnection();
            }
            else if (tbName.Text != "" && tbName.Focused) // Name Search
            {
                int found = rData.getCount("kundendaten", "KundenName", tbName.Text.Trim());

                if (found == 1) // One Name Found
                {
                    rData.openReadConnection();
                    MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "KundenName", tbName.Text.Trim());

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
                    MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "KundenName", tbName.Text.Trim());

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
                    MySqlDataReader readerKunde = rData.searchDaten("kundendaten", "KundenName", tbName.Text.Trim() + "%");
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
                  try
                    {
                        string[] values = { tbTelefon.Text.Trim(),
                                             "Herrn",
                                             tbName.Text.Trim(),
                                             tbStrasse.Text.Trim(),
                                             tbStrNo.Text.Trim(),
                                             tbZusatz.Text.Trim(),
                                             tbPLZ.Text.Trim(),
                                             tbOrt.Text.Trim(),
                                             Convert.ToDouble(tbAnfahrt.Text.Trim()).ToString(providerEn),
                                             Convert.ToDouble(tbRabatt.Text.Trim()).ToString(providerEn)
                                                };
                        kundenreference = rData.addData("kundendaten", values).ToString();
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

        private AutoCompleteStringCollection StrassenVonDatenBank()
        {
            AutoCompleteStringCollection colValues = new AutoCompleteStringCollection();
            rData.openReadConnection();
            MySqlDataReader readerStadtPlan = rData.getDataReader("stadtplan");
            while (readerStadtPlan.Read())
            {
                colValues.Add(readerStadtPlan["strasse"].ToString());
            }
            readerStadtPlan.Close();
            rData.closeReadConnection();
            return colValues;
        }

        //tbAnfahrt
        private void tbAnfahrt_GotFocus(object sender, EventArgs e)
        {
            panelAnfahrt.BackColor = SystemColors.Highlight;
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

        private void tbAnfahrt_LostFocus(object sender, EventArgs e)
        {
            panelAnfahrt.BackColor = SystemColors.Control;
        }

        //tbKNr
        private void tbKNr_GotFocus(object sender, EventArgs e)
        {
            panelKNr.BackColor = SystemColors.Highlight;
        }

        private void tbKNr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbName.Focus();
            if (e.KeyCode == Keys.Up)
                tbTelefon.Focus();
        }

        // keydownevent is implemented above
        private void tbKNr_LostFocus(object sender, EventArgs e)
        {
            panelKNr.BackColor = SystemColors.Control;
        }

        //tbName
        private void tbName_GotFocus(object sender, EventArgs e)
        {
            panelName.BackColor = SystemColors.Highlight;
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbStrasse.Focus();
            if (e.KeyCode == Keys.Up)
                tbKNr.Focus();
        }

        private void tbName_LostFocus(object sender, EventArgs e)
        {
            panelName.BackColor = SystemColors.Control;
        }

        //tbOrt
        private void tbOrt_GotFocus(object sender, EventArgs e)
        {
            panelOrt.BackColor = SystemColors.Highlight;
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

        private void tbOrt_LostFocus(object sender, EventArgs e)
        {
            panelOrt.BackColor = SystemColors.Control;
        }

        //tbPLZ
        private void tbPLZ_GotFocus(object sender, EventArgs e)
        {
            panelPLZ.BackColor = SystemColors.Highlight;
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

        private void tbPLZ_LostFocus(object sender, EventArgs e)
        {
            panelPLZ.BackColor = SystemColors.Control;
        }

        //tbRabatt
        private void tbRabatt_GotFocus(object sender, EventArgs e)
        {
            panelRabatt.BackColor = SystemColors.Highlight;
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

        private void tbRabatt_LostFocus(object sender, EventArgs e)
        {
            panelRabatt.BackColor = SystemColors.Control;
        }

        private void tbStrasse_GotFocus(object sender, EventArgs e)
        {
            panelStrasse.BackColor = SystemColors.Highlight;
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

        private void tbStrasse_LostFocus(object sender, EventArgs e)
        {
            panelStrasse.BackColor = SystemColors.Control;
            rData.openReadConnection();
            MySqlDataReader readerStrasse = rData.getDataReader("stadtplan", "strasse", tbStrasse.Text.Trim());
            if (readerStrasse.Read())
            {
                tbOrt.Text = readerStrasse["ort"].ToString();
                tbPLZ.Text = readerStrasse["PLZ"].ToString();
            }
            readerStrasse.Close();
            rData.closeReadConnection();
        }

        //tbStrNo
        private void tbStrNo_GotFocus(object sender, EventArgs e)
        {
            panelStrNo.BackColor = SystemColors.Highlight;
        }

        private void tbStrNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbPLZ.Focus();
            if (e.KeyCode == Keys.Up)
                tbStrasse.Focus();
        }

        private void tbStrNo_LostFocus(object sender, EventArgs e)
        {
            panelStrNo.BackColor = SystemColors.Control;
        }

        private void tbTelefon_GotFocus(object sender, EventArgs e)
        {
            panelTelefon.BackColor = SystemColors.Highlight;
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

        // tbTelefone
        private void tbTelefone_LostFocus(object sender, EventArgs e)
        {
            panelTelefon.BackColor = SystemColors.Control;
        }

        //tbHimweis
        private void tbZusatz_GotFocus(object sender, EventArgs e)
        {
            panelZusatz.BackColor = SystemColors.Highlight;
        }

        private void tbZusatz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                tbAnfahrt.Focus();
            if (e.KeyCode == Keys.Up)
                tbPLZ.Focus();
        }

        private void tbZusatz_LostFocus(object sender, EventArgs e)
        {
            panelZusatz.BackColor = SystemColors.Control;
        }

        private void UpdateChanges(string Knummer)
        {
            try
            {
                rData.openReadConnection();
                MySqlDataReader readerKunde = rData.getDataReader("kundendaten", "idkundendaten", Knummer);
                if (readerKunde.Read())
                {
                    if (tbAnfahrt.Text != readerKunde["Anfahrtkosten"].ToString())
                        rData.updateSingleData("kundendaten","Anfahrtkosten", tbAnfahrt.Text.Trim(),"idKundendaten", Knummer);
                    if (tbName.Text != readerKunde["KundenName"].ToString())
                        rData.updateSingleData("kundendaten", "KundenName", tbName.Text.Trim(), "idKundendaten",Knummer);
                    if (tbOrt.Text != readerKunde["Ort"].ToString())
                        rData.updateSingleData("kundendaten", "Ort", tbOrt.Text.Trim(), "idKundendaten", Knummer);
                    if (tbPLZ.Text != readerKunde["PLZ"].ToString())
                        rData.updateSingleData("kundendaten", "PLZ", tbPLZ.Text.Trim(), "idKundendaten", Knummer);
                    if (tbStrasse.Text != readerKunde["Strasse"].ToString())
                        rData.updateSingleData("kundendaten", "Strasse", tbStrasse.Text.Trim(), "idKundendaten", Knummer);
                    if (tbStrNo.Text != readerKunde["StrNo"].ToString())
                        rData.updateSingleData("kundendaten", "StrNo", tbStrNo.Text.Trim(), "idKundendaten", Knummer);
                    if (tbTelefon.Text != readerKunde["KundenNr"].ToString())
                        rData.updateSingleData("kundendaten", "KundenNr", tbTelefon.Text.Trim(), "idKundendaten", Knummer);
                    if (tbZusatz.Text != readerKunde["Zusatz"].ToString())
                        rData.updateSingleData("kundendaten", "Zusatz", tbZusatz.Text.Trim(), "idKundendaten", Knummer);
                    if (tbRabatt.Text != readerKunde["Rabatt"].ToString())
                        rData.updateSingleData("kundendaten", "Rabatt", tbRabatt.Text.Trim(), "idKundendaten", Knummer);
                }
                readerKunde.Close();
                rData.closeReadConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
    }
}