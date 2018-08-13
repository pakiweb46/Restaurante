using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using LieferDienst;
namespace Restaurante

{
    public partial class NeuAuftrag : Form
    {
        DialogResult NeuKunde; // Hold if Neu Kunde Speichern or not        
        public string kundenreference;
        //KundeHolder obj_kunde = new KundeHolder();
        Speisekarte obj_speise = new Speisekarte();
        private string CustomerId;// Contains the current idKundendaten
        private bool isDataComplete=true;//ob kundendaten sind vollständig
        // MYSQL String 
        static string connStr = Class1.connString;
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlConnection conn1 = new MySqlConnection(connStr);
        
        public int recordNr, recordCount;
        MySqlCommand cmd;
        MySqlDataReader rdr;
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
            try
            {
                recordNr = 0;
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
       
        private void UpdateChanges(string Knummer)
        {
            
            try
            {
                cmd.CommandText = "SELECT * FROM dbbari.kundendaten WHERE idkundendaten=" + Knummer + ";";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    conn1.Open();
                   MySqlCommand cmd1 = new MySqlCommand(); ;
                  cmd1.Connection = conn1;
                  cmd.Parameters.Clear();
                  if (tbAnfahrt.Text != rdr["Anfahrtkosten"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET Anfahrtkosten=?Anfahrtkosten WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("Anfahrtkosten", MySqlDbType.Double).Value = Convert.ToDouble(tbAnfahrt.Text.Trim());
                      cmd1.ExecuteNonQuery();
                
                  }
                  if (tbName.Text != rdr["KundenName"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET KundenName=?KundenName WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("KundenName", MySqlDbType.VarChar).Value = tbName.Text.Trim();
                      cmd1.ExecuteNonQuery();

                  }
                  if (tbOrt.Text != rdr["Ort"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET Ort=?Ort WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("Ort", MySqlDbType.VarChar).Value = tbOrt.Text.Trim();               
                      cmd1.ExecuteNonQuery();
                  }
                  if (tbPLZ.Text != rdr["PLZ"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET PLZ=?PLZ WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("PLZ", MySqlDbType.Int64).Value = Convert.ToInt64(tbPLZ.Text.Trim());
                      cmd1.ExecuteNonQuery();
                  }
                  if (tbStrasse.Text != rdr["Strasse"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET Strasse=?Strasse WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("Strasse", MySqlDbType.VarChar).Value = tbStrasse.Text.Trim(); 
                      cmd1.ExecuteNonQuery();
                  }
                  if (tbStrNo.Text != rdr["StrNo"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET StrNo=?StrNo WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("StrNo", MySqlDbType.VarChar).Value = tbStrNo.Text.Trim();
                      cmd1.ExecuteNonQuery();
                  }
                  if (tbTelefon.Text!= rdr["KundenNr"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET KundenNr=?KundenNr WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("KundenNr", MySqlDbType.VarChar).Value = tbTelefon.Text.Trim();
                      cmd1.ExecuteNonQuery();
                  }
                  if (tbZusatz.Text != rdr["Zusatz"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET zusatz=?zusatz WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("zusatz", MySqlDbType.VarChar).Value = tbZusatz.Text.Trim();
                      cmd1.ExecuteNonQuery();
                  }
                  if (tbRabatt.Text != rdr["Rabatt"].ToString())
                  {
                      cmd1.CommandText = "UPDATE dbbari.kundendaten SET rabatt=?rabatt WHERE idKundendaten=" + Knummer + ";";
                      cmd1.Prepare();
                      cmd1.Parameters.Add("rabatt", MySqlDbType.Double).Value = Convert.ToDouble(tbRabatt.Text.Trim());
                      cmd1.ExecuteNonQuery();
                  }

                 
                }
                rdr.Close();
                conn1.Close();
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
                button1.Enabled = false;
                button3.PerformClick();
                webBrowser1.Navigate("about:blank");
                tbTelefon.Focus();
            }

        }

        private void NeuAuftrag_Load(object sender, EventArgs e)
        {
            button5.Hide();
            tbKNr.Enabled = true;
            tbAnfahrt.Text = "0";
            tbRabatt.Text = "0";
            tbDatum.Text = System.DateTime.Now.ToShortDateString();
            tbDatum.Enabled = false;
            button1.Enabled = false;
            textBox14.Visible = false;
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(NeuAuftrag_KeyDown);
            tbStrasse.AutoCompleteCustomSource = StrassenVonDatenBank();
            listView2.Visible = false;  
            tbTelefon.Focus();
        }
        private void NeuAuftrag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && button1.Enabled == true)
            {
                
                    button1.PerformClick();

            }
            else if (e.KeyCode == Keys.F10)
            {
                button2.PerformClick(); // Speichern

            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (listView2.Visible)
                {
                    for (int i = listView2.Items.Count; i > 0; i--)
                        listView2.Items.Remove(listView2.Items[0]);
                   listView2.Visible = false;
                }
                else
                      button3.PerformClick();
                
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (isFormClear())
                    button4.PerformClick();
                else
                button3.PerformClick(); // Felder leeren
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
        private void PerformDataFill()
        {

            CustomerId = rdr["idKundendaten"].ToString();
            tbKNr.Text = CustomerId;
            kundenreference = rdr["idKundendaten"].ToString();
            tbName.Text = rdr["KundenName"].ToString();
            tbOrt.Text = rdr["ort"].ToString();
            tbPLZ.Text = rdr["PLZ"].ToString();
            tbStrasse.Text = rdr["strasse"].ToString();
            tbStrNo.Text = rdr["StrNo"].ToString();
            tbZusatz.Text = rdr["zusatz"].ToString();
            tbTelefon.Text = rdr["kundennr"].ToString();
            tbRabatt.Text = rdr["Rabatt"].ToString();
            tbAnfahrt.Text = rdr["Anfahrtkosten"].ToString();
            SelectedKunde = kundenreference;
            button1.Enabled = true;
            button1.Focus();
            
        }
       
        private void SearchRecord()
        {
            // Code from KundenDaten
            NeuKunde = DialogResult.No;
            string sql;
            if (tbTelefon.Text != "" && tbTelefon.Focused) //if telefon is not empty and telefone is focused
            {
                int found = 0; // Number of matching records found
                sql = "SELECT count(*) FROM dbbari.kundendaten Where KundenNr='" + tbTelefon.Text.Trim() + "';";

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
                    found = Convert.ToInt16(rdr[0].ToString());
                }
                rdr.Close();
                if (found == 1) // One Record Found
                {
                    sql = "SELECT * FROM dbbari.kundendaten Where KundenNr='" + tbTelefon.Text.Trim() + "';";
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
                        PerformDataFill();
                        tbKNr.Enabled = false;
                    }

                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (found > 1) // More than One records found
                {
                    // Complete string match where more items have same name
                    sql = "SELECT * FROM dbbari.kundendaten Where KundenNr='" + tbTelefon.Text.Trim() + "';";
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
                            ListViewItem item = new ListViewItem(rdr["KundenNr"].ToString());
                            item.SubItems.Add(rdr["KundenName"].ToString());
                            item.SubItems.Add(rdr["idKundendaten"].ToString());
                            item.SubItems.Add(rdr["Strasse"].ToString() + "." + rdr["strno"].ToString());
                            listView2.Items.Add(item);

                        }
                        listView2.Visible = true;
                        listView2.Enabled = true;
                    }
                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    // not full matching found match String segments 

                    sql = "SELECT * FROM dbbari.kundendaten Where KundenNr Like '" + tbTelefon.Text.Trim() + "%';";
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
                            ListViewItem item = new ListViewItem(rdr["KundenNr"].ToString());
                            item.SubItems.Add(rdr["KundenName"].ToString());
                            item.SubItems.Add(rdr["idKundendaten"].ToString());
                            item.SubItems.Add(rdr["Strasse"].ToString() + "." + rdr["strno"].ToString());
                            listView2.Items.Add(item);
                        }

                        listView2.Visible = true;
                        listView2.Enabled = true;
                    
                    }
                    else
                    {
                        NeuKunde = MessageBox.Show("Daten Nicht gefunden. Soll Neu Kunde eingefügt werden", "?", MessageBoxButtons.YesNo);
                        if (NeuKunde == DialogResult.Yes)
                        {
                            
                            tbName.Focus();
                            tbKNr.Enabled = false;
                            button1.Enabled = true;
                        }
                        listView2.Visible = false;


                    }
                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

            }
            else if (tbKNr.Text!="" && tbKNr.Focused) // Kunden Nummer Search
            {
                try
                {
                sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" + Convert.ToInt64(tbKNr.Text) + ";";
                cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.ToString());
                }
                if (rdr.Read())
                {
                    PerformDataFill();
                }

                try
                {
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
             
            }

            else if (tbName.Text != "" && tbName.Focused) // Name Search
            {
                int found = 0; // Number of matching records found
                sql = "SELECT count(*) FROM dbbari.kundendaten Where KundenName='" + tbName.Text.Trim() + "';";

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
                    found = Convert.ToInt16(rdr[0].ToString());

                }
                rdr.Close();
                if (found == 1) // One Name Found
                {
                    sql = "SELECT * FROM dbbari.kundendaten Where KundenName='" + tbName.Text.Trim() + "';";
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
                        PerformDataFill();
                        tbKNr.Enabled = false;
                    }

                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (found > 1)
                {
                    // Complete string match where more items have same name

                    sql = "SELECT * FROM dbbari.kundendaten Where KundenName='" + tbName.Text.Trim() + "';";
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
                            ListViewItem item = new ListViewItem(rdr["KundenNr"].ToString());
                            item.SubItems.Add(rdr["KundenName"].ToString());
                            item.SubItems.Add(rdr["idKundendaten"].ToString());
                            item.SubItems.Add(rdr["Strasse"].ToString() + "." + rdr["strno"].ToString());
                            listView2.Items.Add(item);

                        }
                        listView2.Visible = true;
                        listView2.Enabled = true;
                        //listView1.Visible = false;
                    }
                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    // not full matching found match String segments 

                    sql = "SELECT * FROM dbbari.kundendaten Where KundenName Like '" + tbName.Text.Trim() + "%';";
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
                            ListViewItem item = new ListViewItem(rdr["KundenNr"].ToString());
                            item.SubItems.Add(rdr["KundenName"].ToString());
                            item.SubItems.Add(rdr["idKundendaten"].ToString());
                            item.SubItems.Add(rdr["Strasse"].ToString() + "." + rdr["strno"].ToString());
                            listView2.Items.Add(item);
                        }

                        listView2.Visible = true;
                        listView2.Enabled = true;

                    }
                    else
                    {
                        NeuKunde = MessageBox.Show("Daten Nicht gefunden. Soll Neu Kunde eingefügt werden", "?", MessageBoxButtons.YesNo);
                        if (NeuKunde == DialogResult.Yes)
                        {
                            tbTelefon.Focus();
                            tbKNr.Enabled = false;
                            button1.Enabled = true;
                        }
                        listView2.Visible = false;
                        //listView1.Visible = true;
                    }
                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

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
        private void listView2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listView2.Visible == true)
            {

                ListView.SelectedListViewItemCollection selecteditem =
                    this.listView2.SelectedItems;


                foreach (ListViewItem item in selecteditem)
                {

                    recordNr = Convert.ToInt32(item.SubItems[2].Text);

                }

                //Search nach kunden Id and fill form
                string sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" + recordNr + ";";
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
                    PerformDataFill();
                    tbKNr.Enabled = false;
                }

                try
                {
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                listView2.Items.Clear();
                listView2.Visible = false;
                listView2.Enabled = false;
                button1.Focus();
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
            // Hier wird eine Liste erstellt die später an die Textbox gehangen wird.
            int strcount;
            AutoCompleteStringCollection colValues = new AutoCompleteStringCollection();

            string sql = "SELECT count(*) FROM dbbari.stadtplan";
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
                strcount = Convert.ToInt32(rdr[0].ToString());
            rdr.Close();

            sql = "SELECT * FROM dbbari.stadtplan";
            cmd = new MySqlCommand(sql, conn);
            try
            {
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            while (rdr.Read())
            {
                colValues.Add(rdr["strasse"].ToString());

            }
            rdr.Close();

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
            string sql = "SELECT * FROM dbbari.stadtplan Where strasse='" + tbStrasse.Text.Trim() + "';";
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

                tbOrt.Text = rdr["ort"].ToString();
                tbPLZ.Text = rdr["PLZ"].ToString();

            }

            try
            {
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                MySqlCommand cmd1 = new MySqlCommand();

                try
                {

                    cmd1.Connection = conn;
                    cmd.Parameters.Clear();
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
                           button5.Show();
                    }
                    else
                    {
                        webBrowser1.Navigate("about:blank");
                        button5.Hide();
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

        private void tbStrasse_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Print();
        }

        private void tbTelefon_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbKNr_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
