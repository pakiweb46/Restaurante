using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class frmKundenDaten : Form
    {
        // TextBox Focus Color
        private static Color FocusColor = Color.Cyan;

        private static Color BlurColor = Color.White;

        // MYSQL String
        private static string connStr = Globals.connString;

        private MySqlConnection conn = new MySqlConnection(connStr);
        public int recordNr, recordCount;
        private MySqlCommand cmd;
        private RestauranteData rData;
        public IFormatProvider providerEn;

        public frmKundenDaten()
        {
            InitializeComponent();
            rData = new RestauranteData();
            providerEn = CultureInfo.CreateSpecificCulture("en-GB");
            // initialize indexer
            recordNr = 0;
            recordCount = rData.getCount("kundendaten");
        }

        private void label3_Click(object sender, EventArgs e)
        {
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

        private void frmKundenDaten_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown +=
                new KeyEventHandler(this.frmKundenDaten_KeyDown);
            recordNr = getMinId();
            tbStraße.AutoCompleteCustomSource = StrassenVonDatenBank();
            tbAnfahrt.Text = "0";
            tbRabatt.Text = "0";
            button3.Enabled = false;
        }

        private void tbStraße_GotFocus(object sender, EventArgs e)
        {
            tbStraße.BackColor = FocusColor;
        }

        private void text_GotFocus(object sender, EventArgs e)
        {
            //tbStrNo.BackColor = System.Drawing.Color.LightSeaGreen;
            Control control = (TextBox)sender;
            control.BackColor = FocusColor;
        }

        private void text_LostFocus(object sender, EventArgs e)
        {
            Control control = (TextBox)sender;
            control.BackColor = BlurColor;
        }

        private void button1_Click(object sender, EventArgs e) // Button Speichern
        {
            if (tbKundenNr.Text != "") // Update Record
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("kundendaten", "KundenNr", tbKundenNr.Text.Trim());

                if (reader.Read())
                {
                    MessageBox.Show("Kunden Nummer Exsistiert Schon");
                    recordNr = Convert.ToInt32(reader[0].ToString());
                    PerformDataFill(ref reader);
                }
                else // Füge Neuen Record
                {
                    if (tbKundenName.Text == "")
                    {
                        MessageBox.Show("Bitte geben sie die Kunden Name");
                        tbKundenName.Focus();
                    }
                    else if (tbKundenNr.Text == "")
                    {
                        MessageBox.Show("Bitte geben sie die Kunden Nummer");
                        tbKundenNr.Focus();
                    }
                    else if (tbStraße.Text == "")
                    {
                        MessageBox.Show("Bitte wählen sie die Strasse");
                        tbStraße.Focus();
                    }
                    else if (tbOrt.Text == "")
                    {
                        MessageBox.Show("Bitte geben sie den Ort");
                        tbOrt.Focus();
                    }
                    else if (tbPLZ.Text == "" || !IsInteger(tbPLZ.Text))
                    {
                        MessageBox.Show("Post Leitzahl soll ein Nummer sein");
                        tbPLZ.Focus();
                    }
                    else
                    {
                        try
                        {
                            string[] values = { tbKundenNr.Text.Trim(),
                                                tbAnrede.Text.Trim(),
                                                tbKundenName.Text.Trim(),
                                                tbStraße.Text.Trim(),
                                                tbStrNo.Text.Trim(),
                                                 tbZusatz.Text.Trim(),
                                                 tbPLZ.Text.Trim(),
                                                 tbOrt.Text.Trim(),
                                                 Convert.ToDouble(tbAnfahrt.Text.Trim()).ToString(providerEn),
                                                 Convert.ToDouble(tbRabatt.Text.Trim()).ToString(providerEn)
                                                };
                            rData.addData("kundendaten", values);
                            MessageBox.Show("Kundendaten sind gespeichert");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                reader.Close();
                rData.closeReadConnection();
            }
        }

        private void frmKundenDaten_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button2.PerformClick(); // Suchen
            }
            else if (e.KeyCode == Keys.F4)
            {
                button1.PerformClick(); // Specihern
            }
            else if (e.KeyCode == Keys.F7)
            {
                button3.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button4.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnAendern.PerformClick();// Ändern
            }
            else if (e.KeyCode == Keys.F5)
            {
                button9.PerformClick();// leeren
            }
        }

        private void button2_Click(object sender, EventArgs e) // Button Suchen
        {
            if (tbKundenNr.Text != "")
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("kundendaten", "KundenNr", tbKundenNr.Text.Trim());
                if (reader.Read())
                {
                    recordNr = Convert.ToInt32(reader[0].ToString());

                    PerformDataFill(ref reader);
                }

                reader.Close();
                rData.closeReadConnection();
            }
            else if (tbKundenName.Text != "")
            {
                int found = rData.getCount("kundendaten", "KundenName", tbKundenName.Text.Trim());
                if (found == 1)
                {
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("kundendaten", "KundenName", tbKundenName.Text.Trim());
                    if (reader.Read())
                    {
                        recordNr = Convert.ToInt32(reader[0].ToString());
                        PerformDataFill(ref reader);
                    }
                    reader.Close();
                    rData.closeReadConnection();
                }
                else if (found > 1)
                {
                    // Complete string match where more items have same name
                    listView1.Visible = true;
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("kundendaten", "KundenName", tbKundenName.Text.Trim());
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["KundenNr"].ToString());
                            item.SubItems.Add(reader["KundenName"].ToString());
                            item.SubItems.Add(reader["idKundendaten"].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    reader.Close();
                    rData.closeReadConnection();
                    MessageBox.Show("Mehere daten gefunden wählen sie aus der liste");
                }
                else
                {
                    // not full matching found match String segments
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.searchDaten("kundendaten", "KundenName", "%" + tbKundenName.Text.Trim() + "%");
                    listView1.Visible = true;
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Mehere daten gefunden wählen sie aus der liste");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["KundenNr"].ToString());

                            item.SubItems.Add(reader["KundenName"].ToString());
                            item.SubItems.Add(reader["idKundendaten"].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Daten nicht gefunden");
                    }
                    reader.Close();
                    rData.closeReadConnection();
                }
            }
            else
            {
                MessageBox.Show("Für Suchen Bitte geben sie Kunden Nummer oder Kunden Name ein");
            }
        }

        private void button8_Click(object sender, EventArgs e) // Forward
        {
            if (recordNr != getMaxId())
            {
                recordNr = getMinId(recordNr); // gets minimum id which is greater than record number
            }

            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", recordNr.ToString());
            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void button6_Click(object sender, EventArgs e) // Back Button
        {
            if (recordNr != getMinId())
                recordNr = getMaxId(recordNr); // gets maximum id which is smaller than recordNr
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", recordNr.ToString());

            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private int getMaxId()
        {
            return rData.getMax("kundendaten", "idKundendaten"); //-1 fehler
        }

        private int getMaxId(int magicno)
        {
            return rData.getMin("kundendaten", "idKundendaten", "idKundendaten", magicno.ToString(), "<");  // -1 ist fehler
        }

        private int getMinId()
        {
            return rData.getMin("kundendaten", "idKundendaten");  // -1 ist fehler
            // -1 ist fehler
        }

        private int getMinId(int magicNo)
        {
            return rData.getMin("kundendaten", "idKundendaten", "idKundendaten", magicNo.ToString(), ">");  // -1 ist fehler
        }

        private void button7_Click(object sender, EventArgs e) // Goto End of record
        {
            // recordNr = recordCount;
            int maxid = getMaxId();
            recordNr = maxid;
            if (maxid != -1)
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", maxid.ToString());

                if (reader.Read())
                {
                    PerformDataFill(ref reader);
                }
                reader.Close();
                rData.closeReadConnection();
            }
            else if (maxid == -1)
            {
                MessageBox.Show("Ein Fehler ist getretten");
            }
        }

        private AutoCompleteStringCollection StrassenVonDatenBank()
        {
            // Hier wird eine Liste erstellt die später an die Textbox gehangen wird.
            int strcount;
            AutoCompleteStringCollection colValues = new AutoCompleteStringCollection();

            //colValues.AddRange(new string[] { "Berlin", "Hamburg", "Bremen", "Stuttgart", "Saarbrücken", "Frankfurt a.M." });
            strcount = rData.getCount("stadtplan");
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("stadtplan");
            while (reader.Read())
            {
                colValues.Add(reader["strasse"].ToString());
            }
            reader.Close();
            rData.closeReadConnection();
            return colValues;
        }

        private void button5_Click(object sender, EventArgs e) // Begin of Record
        {
            int minid = getMinId();
            recordNr = minid;
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", minid.ToString());
            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void tbStraße_LostFocus(object sender, EventArgs e)
        {
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("stadtplan", "strasse", tbStraße.Text.Trim());
            if (reader.Read())
            {
                tbOrt.Text = reader["ort"].ToString();
                tbPLZ.Text = reader["PLZ"].ToString();
            }
            reader.Close();
            rData.closeReadConnection();
            tbStraße.BackColor = BlurColor;
        }

        private void frmKundenDaten_FormClosing(object sender, EventArgs e)
        {
            // closing Db connection
            if (cmd != null)
                cmd.Dispose();
            if (conn != null)
            {
                try
                {
                    conn.Close();
                    conn.Dispose();
                }
                catch
                { }
            }
        }

        private void listView1_SelectedIndexChanged(
          object sender, System.EventArgs e)
        {
            //int kundenId;
            ListView.SelectedListViewItemCollection selecteditem =
                this.listView1.SelectedItems;

            foreach (ListViewItem item in selecteditem)
            {
                recordNr = Convert.ToInt32(item.SubItems[2].Text);
            }

            //Search nach kunden Id and fill form

            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", recordNr.ToString());
            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
            listView1.Items.Clear();
            listView1.Visible = false;
        }

        private void tbStraße_TextChanged(object sender, EventArgs e)
        {
        }

        private void PerformDataFill(ref MySqlDataReader reader)
        {
            myClearForm();
            tbKundenNr.Text = reader["kundennr"].ToString();
            tbAnrede.Text = reader["Anrede"].ToString();
            tbKundenName.Text = reader["KundenName"].ToString();
            tbOrt.Text = reader["ort"].ToString();
            tbPLZ.Text = reader["PLZ"].ToString();
            tbStraße.Text = reader["strasse"].ToString();
            tbStrNo.Text = reader["StrNo"].ToString();
            tbZusatz.Text = reader["zusatz"].ToString();
            tbAnfahrt.Text = reader["Anfahrtkosten"].ToString();
            tbRabatt.Text = reader["Rabatt"].ToString();
            button3.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e) // Clear Button
        {
            myClearForm();
        }

        private void myClearForm()
        {
            tbAnrede.Clear();
            tbKundenName.Clear();
            tbKundenNr.Clear();
            tbOrt.Clear();
            tbPLZ.Clear();
            tbStraße.Clear();
            tbStrNo.Clear();
            tbZusatz.Clear();
            tbRabatt.Text = "0";
            tbAnfahrt.Text = "0";
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e) // Button Löschen Delete
        {
            DialogResult result;
            result = MessageBox.Show("Sind sie sicher dass die Kunden Löchen Möchten!" + "\n" + "In Abbrechnung wird Gelöchte Kunde nicht Erscheinen ", "Vorsicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = "DELETE FROM dbbari.kundendaten Where idKundendaten=" + recordNr + ";";
                cmd = new MySqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    button9.PerformClick();
                    if (recordNr != getMinId())
                        recordNr = getMinId();
                    else
                        recordNr = getMaxId();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void tbKundenName_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAendern_Click(object sender, EventArgs e)
        {
            string[] variable = { "KundenNr", "Anrede", "KundenName", "Strasse", "StrNo", "zusatz", "PLZ", "Ort", "Anfahrtkosten", "Rabatt" };
            string[] value = { tbKundenNr.Text.Trim(),
                                tbAnrede.Text.Trim(),
                                tbKundenName.Text.Trim(),
                                tbStraße.Text.Trim(),
                                tbStrNo.Text.Trim(),
                                tbZusatz.Text.Trim(),
                                tbPLZ.Text.Trim(),
                                tbOrt.Text.Trim(),
                                tbAnfahrt.Text.Trim(),
                                tbRabatt.Text.Trim() };
            rData.updateData("kundendaten", variable, value, "idKundendaten", recordNr.ToString());
            MessageBox.Show("Daten sind Geändert");
        }
    }
}