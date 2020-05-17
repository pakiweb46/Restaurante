using MySql.Data.MySqlClient;
using System;
using System.Drawing;
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
        private MySqlDataReader rdr;

        public frmKundenDaten()
        {
            InitializeComponent();
            try
            {
                // initialize indexer
                recordNr = 0;
                // Reader instances hasrows== true wenn rows are there
                conn.Open();
                string sql = "SELECT count(*) FROM dbbari.kundendaten;";
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
            string sql;
            if (tbKundenNr.Text != "") // Update Record
            {
                sql = "SELECT * FROM dbbari.kundendaten Where KundenNr=" + tbKundenNr.Text.Trim() + ";";
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
                    MessageBox.Show("Kunden Nummer Exsistiert Schon");
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
                        MySqlCommand cmd1 = new MySqlCommand();

                        try
                        {
                            cmd1.Connection = conn;

                            cmd.Parameters.Clear();

                            cmd1.CommandText = "INSERT INTO dbbari.kundendaten VALUES (NULL, @KundenNr , @Anrede, @KundenName, @Strasse, @StrNo,  @zusatz, @PLZ, @Ort,@Anfahrtkosten, @Rabatt)";
                            cmd1.Prepare();
                            cmd1.Parameters.AddWithValue("KundenNr", tbKundenNr.Text.Trim());
                            cmd1.Parameters.AddWithValue("Anrede", tbAnrede.Text.Trim());
                            cmd1.Parameters.AddWithValue("KundenName", tbKundenName.Text.Trim());
                            cmd1.Parameters.AddWithValue("Strasse", tbStraße.Text.Trim());
                            cmd1.Parameters.AddWithValue("StrNo", tbStrNo.Text.Trim());
                            cmd1.Parameters.AddWithValue("zusatz", tbZusatz.Text.Trim());
                            cmd1.Parameters.AddWithValue("PLZ", Convert.ToInt64(tbPLZ.Text.Trim()));
                            cmd1.Parameters.AddWithValue("Ort", tbOrt.Text.Trim());
                            cmd1.Parameters.AddWithValue("Anfahrtkosten", Convert.ToDouble(tbAnfahrt.Text.Trim()));
                            cmd1.Parameters.AddWithValue("Rabatt", Convert.ToDouble(tbRabatt.Text.Trim()));

                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Kundendaten sind gespeichert");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
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
                button10.PerformClick();// Ändern
            }
            else if (e.KeyCode == Keys.F5)
            {
                button9.PerformClick();// leeren
            }
        }

        private void button2_Click(object sender, EventArgs e) // Button Suchen
        {
            string sql;
            if (tbKundenNr.Text != "")
            {
                sql = "SELECT * FROM dbbari.kundendaten Where KundenNr=" + tbKundenNr.Text.Trim() + ";";
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
            else if (tbKundenName.Text != "")
            {
                int found = 0; // Number of matching records found
                sql = "SELECT count(*) FROM dbbari.kundendaten Where KundenName='" + tbKundenName.Text.Trim() + "';";

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
                    sql = "SELECT * FROM dbbari.kundendaten Where KundenName='" + tbKundenName.Text.Trim() + "';";
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
                    sql = "SELECT * FROM dbbari.kundendaten Where KundenName='" + tbKundenName.Text.Trim() + "';";
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

                    sql = "SELECT * FROM dbbari.kundendaten Where KundenName Like '%" + tbKundenName.Text.Trim() + "%';";
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
                            ListViewItem item = new ListViewItem(rdr["KundenNr"].ToString());

                            item.SubItems.Add(rdr["KundenName"].ToString());
                            item.SubItems.Add(rdr["idKundendaten"].ToString());
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
                MessageBox.Show("Für Suchen Bitte geben sie Kunden Nummer oder Kunden Name ein");
            }
        }

        private void button8_Click(object sender, EventArgs e) // Forward
        {
            if (recordNr != getMaxId())
            {
                recordNr = getMinId(recordNr); // gets minimum id which is greater than record number
            }

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

        private void button6_Click(object sender, EventArgs e) // Back Button
        {
            if (recordNr != getMinId())
                recordNr = getMaxId(recordNr); // gets maximum id which is smaller than recordNr
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

        private int getMaxId()
        {
            int maxid = -1;
            string sql = "SELECT max(idKundendaten) FROM dbbari.kundendaten ;";
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

        private int getMaxId(int magicno)
        {
            int maxid = -1;
            string sql = "SELECT max(idKundendaten) FROM dbbari.kundendaten Where idKundendaten<" + magicno + ";";
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

        private int getMinId()
        {
            int minid = -1;
            string sql = "SELECT min(idKundendaten) FROM dbbari.kundendaten ;";
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

        private int getMinId(int magicNo)
        {
            int minid = -1;
            string sql = "SELECT min(idKundendaten) FROM dbbari.kundendaten where idKundendaten >" + magicNo + " ;";
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

        private void button7_Click(object sender, EventArgs e) // Goto End of record
        {
            // recordNr = recordCount;
            int maxid = getMaxId();
            recordNr = maxid;
            if (maxid != -1)
            {
                string sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" + maxid + ";";
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

        private void button5_Click(object sender, EventArgs e) // Begin of Record
        {
            int minid = getMinId();
            recordNr = minid;
            string sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" + minid + ";";
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

        private void tbStraße_LostFocus(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM dbbari.stadtplan Where strasse='" + tbStraße.Text.Trim() + "';";
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

        private void tbStraße_TextChanged(object sender, EventArgs e)
        {
        }

        private void PerformDataFill()
        {
            myClearForm();
            tbKundenNr.Text = rdr["kundennr"].ToString();
            tbAnrede.Text = rdr["Anrede"].ToString();
            tbKundenName.Text = rdr["KundenName"].ToString();
            tbOrt.Text = rdr["ort"].ToString();
            tbPLZ.Text = rdr["PLZ"].ToString();
            tbStraße.Text = rdr["strasse"].ToString();
            tbStrNo.Text = rdr["StrNo"].ToString();
            tbZusatz.Text = rdr["zusatz"].ToString();
            tbAnfahrt.Text = rdr["Anfahrtkosten"].ToString();
            tbRabatt.Text = rdr["Rabatt"].ToString();
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

        private void button10_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd1 = new MySqlCommand(); ;
            cmd1.Connection = conn;

            cmd.Parameters.Clear();

            try
            {
                cmd1.CommandText = "UPDATE dbbari.kundendaten SET KundenNr=?KundenNr , Anrede=?Anrede, KundenName=?KundenName, Strasse=?Strasse, StrNo=?StrNo,  zusatz=?zusatz, PLZ=?PLZ, Ort=?Ort, Anfahrtkosten=?Anfahrtkosten, Rabatt=?Rabatt WHERE idKundendaten=" + recordNr + ";";
                cmd1.Prepare();
                cmd1.Parameters.Add("KundenNr", MySqlDbType.VarChar).Value = tbKundenNr.Text.Trim();
                cmd1.Parameters.Add("Anrede", MySqlDbType.VarChar).Value = tbAnrede.Text.Trim();
                cmd1.Parameters.Add("KundenName", MySqlDbType.VarChar).Value = tbKundenName.Text.Trim();
                cmd1.Parameters.Add("Strasse", MySqlDbType.VarChar).Value = tbStraße.Text.Trim();
                cmd1.Parameters.Add("StrNo", MySqlDbType.VarChar).Value = tbStrNo.Text.Trim();
                cmd1.Parameters.Add("zusatz", MySqlDbType.VarChar).Value = tbZusatz.Text.Trim();
                cmd1.Parameters.Add("PLZ", MySqlDbType.Int64).Value = Convert.ToInt64(tbPLZ.Text.Trim());
                cmd1.Parameters.Add("Ort", MySqlDbType.VarChar).Value = tbOrt.Text.Trim();
                cmd1.Parameters.Add("Anfahrtkosten", MySqlDbType.Double).Value = Convert.ToDouble(tbAnfahrt.Text.Trim());
                cmd1.Parameters.Add("Rabatt", MySqlDbType.Double).Value = Convert.ToDouble(tbRabatt.Text.Trim());

                cmd1.ExecuteNonQuery();
                MessageBox.Show("Daten sind Geändert");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}