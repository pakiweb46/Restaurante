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
        private MySqlDataReader rdr;

        public frmMitarbeiter()
        {
            InitializeComponent();
            try
            {
                // initialize indexer
                recordNr = 0;
                // Reader instances hasrows== true wenn rows are there
                conn.Open();
                string sql = "SELECT count(*) FROM dbbari.mitarbeiter;";
                cmd = new MySqlCommand(sql, conn);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    recordCount = Convert.ToInt16(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            int minid = -1;
            string sql = "SELECT min(idMitarbeiter) FROM dbbari.mitarbeiter ;";
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
                minid = Convert.ToInt32(rdr[0].ToString());
            }

            try
            {
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return minid;  // -1 ist fehler
        }

        private AutoCompleteStringCollection StrassenVonDatenBank()
        {
            // Hier wird eine Liste erstellt die später an die Textbox gehangen wird.
            int strcount;
            AutoCompleteStringCollection colValues = new AutoCompleteStringCollection();

            //colValues.AddRange(new string[] { "Berlin", "Hamburg", "Bremen", "Stuttgart", "Saarbrücken", "Frankfurt a.M." });
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
            string sql;
            if (textBox3.Text != "") // Update Record
            {
                sql = "SELECT * FROM dbbari.mitarbeiter Where MitarbeiterName='" + textBox3.Text.Trim() + "';";
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
                    MessageBox.Show("Diese Mitarbeiter Exsistiert Schon");
                    recordNr = Convert.ToInt32(rdr[0].ToString());
                    PerformDataFill();
                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else // Füge Neuen Record
                {
                    try
                    {
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
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
                    }
                }
            }
        }

        private int getCheckBoxStatus()
        {
            if (checkBox1.Checked)
                return 1;
            else
                return 0;
        }

        private void PerformDataFill()
        {
            myClearForm();
            textBox3.Text = rdr["MitarbeiterName"].ToString();
            textBox8.Text = rdr["ort"].ToString();
            textBox7.Text = rdr["PLZ"].ToString();
            tbStraße.Text = rdr["strasse"].ToString();
            textBox5.Text = rdr["StrNo"].ToString();
            textBox6.Text = rdr["zusatz"].ToString();

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
            int maxid = -1;
            string sql = "SELECT max(idmitarbeiter) FROM dbbari.mitarbeiter ;";
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
                maxid = Convert.ToInt32(rdr[0].ToString());
            }

            try
            {
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return maxid;  // -1 ist fehler
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Sind sie sicher diese mitarbeiter Löschen Möchten!", "Vorsicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
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
            }
        }

        private int getMinId(int magicNo)
        {
            int minid = -1;
            string sql = "SELECT min(idmitarbeiter) FROM dbbari.mitarbeiter where idMitarbeiter >" + magicNo + " ;";
            cmd = new MySqlCommand(sql, conn);

            try
            {
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    minid = Convert.ToInt32(rdr[0].ToString());
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return minid;  // -1 ist fehler
        }

        private int getMaxId(int magicno)
        {
            int maxid = -1;
            string sql = "SELECT max(idmitarbeiter) FROM dbbari.mitarbeiter Where idmitarbeiter<" + magicno + ";";
            cmd = new MySqlCommand(sql, conn);
            try
            {
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    maxid = Convert.ToInt32(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return maxid;  // -1 ist fehler
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (recordNr != getMaxId())
            {
                recordNr = getMinId(recordNr); // gets minimum id which is greater than record number
            }

            string sql = "SELECT * FROM dbbari.mitarbeiter Where idMitarbeiter=" + recordNr + ";";
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

        private void button7_Click(object sender, EventArgs e)
        {
            int maxid = getMaxId();
            recordNr = maxid;
            if (maxid != -1)
            {
                string sql = "SELECT * FROM dbbari.mitarbeiter Where idMitarbeiter=" + maxid + ";";
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
            else if (maxid == -1)
            {
                MessageBox.Show("Ein Fehler ist getretten");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (recordNr != getMinId())
                recordNr = getMaxId(recordNr); // gets maximum id which is smaller than recordNr
            string sql = "SELECT * FROM dbbari.mitarbeiter Where idMitarbeiter=" + recordNr + ";";
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

        private void button5_Click(object sender, EventArgs e)
        {
            int minid = getMinId();
            recordNr = minid;
            string sql = "SELECT * FROM dbbari.mitarbeiter Where idMitarbeiter=" + minid + ";";
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

        private void button2_Click(object sender, EventArgs e)
        {
            string sql;

            if (textBox3.Text != "")
            {
                int found = 0; // Number of matching records found
                sql = "SELECT count(*) FROM dbbari.mitarbeiter Where MitarbeiterName='" + textBox3.Text.Trim() + "';";

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
                if (found == 1)
                {
                    cmd.Parameters.Clear();
                    sql = "SELECT * FROM dbbari.mitarbeiter Where MitarbeiterName='" + textBox3.Text.Trim() + "';";
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
                        recordNr = Convert.ToInt32(rdr[0].ToString());
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
                else if (found > 1)
                {
                    // Complete string match where more items have same name
                    listView1.Visible = true;

                    sql = "SELECT * FROM dbbari.mitarbeiter Where MitarbeiterName='" + textBox3.Text.Trim() + "';";
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
                            ListViewItem item = new ListViewItem(rdr["MitarbeiterName"].ToString());
                            item.SubItems.Add(rdr["idMitarbeiter"].ToString());
                            listView1.Items.Add(item);
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
                    MessageBox.Show("Mehere daten gefunden wählen sie aus der liste");
                }
                else
                {
                    // not full matching found match String segments
                    sql = "SELECT * FROM dbbari.mitarbeiter Where MitarbeiterName Like '%" + textBox3.Text.Trim() + "%';";
                    cmd = new MySqlCommand(sql, conn);
                    listView1.Visible = true;
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
                        MessageBox.Show("Mehere daten gefunden wählen sie aus der liste");
                        while (rdr.Read())
                        {
                            ListViewItem item = new ListViewItem(rdr["MitarbeiterName"].ToString());
                            item.SubItems.Add(rdr["idMitarbeiter"].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Daten nicht gefunden");
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
            string sql = "SELECT * FROM dbbari.mitarbeiter Where idMitarbeiter=" + recordNr + ";";
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
            }

            try
            {
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            listView1.Items.Clear();
            listView1.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            myClearForm();
        }
    }
}