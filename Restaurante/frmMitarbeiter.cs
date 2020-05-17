using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class frmMitarbeiter : Form
    {
        private static Color FocusColor = Color.Cyan;
        private static Color BlurColor = Color.White;

        // MYSQL String
        private static string connStr = Globals.connString;

        private MySqlConnection conn = new MySqlConnection(connStr);
        public int recordNr, recordCount;
        private MySqlCommand cmd;
        private RestauranteData rData;

        public frmMitarbeiter()
        {
            InitializeComponent();
            rData = new RestauranteData();
            // initialize indexer
            recordNr = 0;
            recordCount = rData.getCount("mitarbeiter");
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMitarbeiter_Load(object sender, EventArgs e)
        {
            listView1.Visible = false;
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmMitarbeiter_KeyDown);
            recordNr = getMinId();
            tbStraße.AutoCompleteCustomSource = StrassenVonDatenBank();
            button3.Enabled = false;
            comboBox1.Items.Add("Fahrer");
            comboBox1.Items.Add("Pizza Bäcker");
            comboBox1.Items.Add("Küchen Hilfe");
            comboBox1.Items.Add("Reinigung");
        }

        private int getMinId()
        {
            return rData.getMin("mitarbeiter", "idMitarbeiter");
            // -1 ist fehler
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

        private void frmMitarbeiter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button2.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.F7)
            {
                button3.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button4.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                button9.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "") // Update Record
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("mitarbeiter", "MitarbeiterName", textBox3.Text.Trim());

                if (reader.Read())
                {
                    MessageBox.Show("Diese Mitarbeiter Exsistiert Schon");
                    recordNr = Convert.ToInt32(reader[0].ToString());
                    PerformDataFill(ref reader);
                }
                else // Füge Neuen Record
                {
                    if (textBox3.Text == "") // Name
                    {
                        MessageBox.Show("Bitte geben sie die Name");
                        textBox3.Focus();
                    }
                    else if (tbStraße.Text == "")
                    {
                        MessageBox.Show("Bitte wählen sie die Strasse");
                        tbStraße.Focus();
                    }
                    else if (textBox8.Text == "") // Ort
                    {
                        MessageBox.Show("Bitte geben sie den Ort");
                        textBox8.Focus();
                    }
                    else if (textBox7.Text == "" || !IsInteger(textBox7.Text))
                    {
                        MessageBox.Show("Post Leitzahl soll ein Nummer sein");
                        textBox7.Focus();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand();

                        try
                        {
                            cmd1.Connection = conn;

                            cmd1.CommandText = "INSERT INTO dbbari.mitarbeiter VALUES (NULL, @MitarbeiterName , @Strasse, @StrNo, @PLZ, @Ort, @Tatigkeit, @Adminrechte, @zusatz)";
                            cmd1.Prepare();

                            cmd1.Parameters.AddWithValue("MitarbeiterName", textBox3.Text.Trim());
                            cmd1.Parameters.AddWithValue("Strasse", tbStraße.Text.Trim());
                            cmd1.Parameters.AddWithValue("StrNo", textBox5.Text.Trim());
                            cmd1.Parameters.AddWithValue("zusatz", textBox6.Text.Trim());
                            cmd1.Parameters.AddWithValue("PLZ", Convert.ToInt64(textBox7.Text.Trim()));
                            cmd1.Parameters.AddWithValue("Ort", textBox8.Text.Trim());
                            cmd1.Parameters.AddWithValue("Tatigkeit", comboBox1.Text.Trim());
                            cmd1.Parameters.AddWithValue("Adminrechte", Convert.ToInt32(getCheckBoxStatus()));
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Mitarbeiter ist gespeichert");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        conn.Close();
                    }
                }
                reader.Close();
                rData.closeReadConnection();
            }
        }

        private int getCheckBoxStatus()
        {
            if (checkBox1.Checked)
                return 1;
            else
                return 0;
        }

        private void PerformDataFill(ref MySqlDataReader reader)
        {
            myClearForm();
            textBox3.Text = reader["MitarbeiterName"].ToString();
            textBox8.Text = reader["ort"].ToString();
            textBox7.Text = reader["PLZ"].ToString();
            tbStraße.Text = reader["strasse"].ToString();
            textBox5.Text = reader["StrNo"].ToString();
            textBox6.Text = reader["zusatz"].ToString();

            button3.Enabled = true;
        }

        private void myClearForm()
        {
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            tbStraße.Clear();
            checkBox1.Checked = false;
        }

        private int getMaxId()
        {
            return rData.getMax("mitarbeiter", "idmitarbeiter");  // -1 ist fehler
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Sind sie sicher diese mitarbeiter Löschen Möchten!", "Vorsicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                conn.Open();
                string sql = "DELETE FROM dbbari.mitarbeiter Where idMitarbeiter=" + recordNr + ";";
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
                conn.Close();
            }
        }

        private int getMinId(int magicNo)
        {
            return rData.getMin("mitarbeiter", "idmitarbeiter", "idMitarbeiter", magicNo.ToString(), ">");  // -1 ist fehler
        }

        private int getMaxId(int magicno)
        {
            return rData.getMax("mitarbeiter", "idmitarbeiter", "idMitarbeiter", magicno.ToString(), "<");  // -1 ist fehler
                                                                                                            // -1 ist fehler
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (recordNr != getMaxId())
            {
                recordNr = getMinId(recordNr); // gets minimum id which is greater than record number
            }
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("mitarbeiter", "idMitarbeiter", recordNr.ToString());

            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int maxid = getMaxId();
            recordNr = maxid;
            if (maxid != -1)
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("mitarbeiter", "idMitarbeiter", maxid.ToString());
                if (reader.Read())
                {
                    PerformDataFill(ref reader);
                }
                reader.Close();
                rData.closeReadConnection();
            }
            else if (maxid == -1)
            {
                MessageBox.Show("Ein Fehler ist getretten.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (recordNr != getMinId())
                recordNr = getMaxId(recordNr); // gets maximum id which is smaller than recordNr
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("mitarbeiter", "idMitarbeiter", recordNr.ToString());
            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int minid = getMinId();
            recordNr = minid;
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("mitarbeiter", "idMitarbeiter", minid.ToString());
            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                int found = rData.getCount("mitarbeiter", "MitarbeiterName", textBox3.Text.Trim());
                if (found == 1)
                {
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("mitarbeiter", "MitarbeiterName", textBox3.Text.Trim());

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
                    MySqlDataReader reader = rData.getDataReader("mitarbeiter", "MitarbeiterName", textBox3.Text.Trim());

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["MitarbeiterName"].ToString());
                            item.SubItems.Add(reader["idMitarbeiter"].ToString());
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
                    MySqlDataReader reader = rData.searchDaten("mitarbeiter", "MitarbeiterName", "%" + textBox3.Text.Trim() + "%");
                    listView1.Visible = true;
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Mehere daten gefunden wählen sie aus der liste");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["MitarbeiterName"].ToString());
                            item.SubItems.Add(reader["idMitarbeiter"].ToString());
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
                MessageBox.Show("Für Suchen Bitte geben sie der Name ein");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selecteditem =
                this.listView1.SelectedItems;

            foreach (ListViewItem item in selecteditem)
            {
                recordNr = Convert.ToInt32(item.SubItems[1].Text);
            }

            //Search nach kunden Id and fill form
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("mitarbeiter", "idMitarbeiter", recordNr.ToString());
            if (reader.Read())
            {
                PerformDataFill(ref reader);
            }
            rData.closeReadConnection();
            listView1.Items.Clear();
            listView1.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            myClearForm();
        }
    }
}