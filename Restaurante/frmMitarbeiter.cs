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
            cmbTatigkeit.Items.Add("Fahrer");
            cmbTatigkeit.Items.Add("Pizza Bäcker");
            cmbTatigkeit.Items.Add("Küchen Hilfe");
            cmbTatigkeit.Items.Add("Reinigung");
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
            if (txtMitarbeiterName.Text != "") // Update Record
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("mitarbeiter", "MitarbeiterName", txtMitarbeiterName.Text.Trim());

                if (reader.Read())
                {
                    MessageBox.Show("Diese Mitarbeiter Exsistiert Schon");
                    recordNr = Convert.ToInt32(reader[0].ToString());
                    PerformDataFill(ref reader);
                }
                else // Füge Neuen Record
                {
                    if (txtMitarbeiterName.Text == "") // Name
                    {
                        MessageBox.Show("Bitte geben sie die Name");
                        txtMitarbeiterName.Focus();
                    }
                    else if (tbStraße.Text == "")
                    {
                        MessageBox.Show("Bitte wählen sie die Strasse");
                        tbStraße.Focus();
                    }
                    else if (txtOrt.Text == "") // Ort
                    {
                        MessageBox.Show("Bitte geben sie den Ort");
                        txtOrt.Focus();
                    }
                    else if (txtPLZ.Text == "" || !IsInteger(txtPLZ.Text))
                    {
                        MessageBox.Show("Post Leitzahl soll ein Nummer sein");
                        txtPLZ.Focus();
                    }
                    else
                    {
                        try
                        {                           
                            string[] values = {  txtMitarbeiterName.Text.Trim(),
                                                tbStraße.Text.Trim(),
                                                txtStrNo.Text.Trim(),
                                                txtzusatz.Text.Trim(),
                                                txtPLZ.Text.Trim(),
                                                txtOrt.Text.Trim(),
                                                cmbTatigkeit.Text.Trim(),
                                                Convert.ToInt32(getCheckBoxStatus()).ToString()
                                                };
                            rData.addData("mitarbeiter", values);
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
            txtMitarbeiterName.Text = reader["MitarbeiterName"].ToString();
            txtOrt.Text = reader["ort"].ToString();
            txtPLZ.Text = reader["PLZ"].ToString();
            tbStraße.Text = reader["strasse"].ToString();
            txtStrNo.Text = reader["StrNo"].ToString();
            txtzusatz.Text = reader["zusatz"].ToString();

            button3.Enabled = true;
        }

        private void myClearForm()
        {
            txtMitarbeiterName.Clear();
            txtStrNo.Clear();
            txtzusatz.Clear();
            txtPLZ.Clear();
            txtOrt.Clear();
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
            if (txtMitarbeiterName.Text != "")
            {
                int found = rData.getCount("mitarbeiter", "MitarbeiterName", txtMitarbeiterName.Text.Trim());
                if (found == 1)
                {
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("mitarbeiter", "MitarbeiterName", txtMitarbeiterName.Text.Trim());

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
                    MySqlDataReader reader = rData.getDataReader("mitarbeiter", "MitarbeiterName", txtMitarbeiterName.Text.Trim());

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
                    MySqlDataReader reader = rData.searchDaten("mitarbeiter", "MitarbeiterName", "%" + txtMitarbeiterName.Text.Trim() + "%");
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