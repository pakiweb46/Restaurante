using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

//TODO:: NEXT PREVIOUS NOT FUNCTIONING
namespace Restaurante
{
    public partial class frmSpeiseKarte : Form
    {
        private static string connStr = Globals.connString;
        private int recordNr;
        private MySqlConnection conn = new MySqlConnection(connStr);
        private MySqlCommand cmd;

        private RestauranteData rData;

        public frmSpeiseKarte()
        {
            InitializeComponent();
            rData = new RestauranteData();
        }

        private void frmSpeiseKarte_Load(object sender, EventArgs e)
        {
            pfandBox.Items.Add("OHNE");
            pfandBox.Items.Add("PFAND1");
            pfandBox.Items.Add("PFAND2");
            pfandBox.Items.Add("PFAND3");

            lvArtikel.Visible = false;
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmSpeiseKarte_KeyDown);
            PerformListFill();
        }

        private void PerformSearch(string ArtikelNr)
        {
            if (tbArtikel.Text != "")
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("speisekarte", "ArtikelNr", ArtikelNr);
                if (reader.Read())
                {
                    recordNr = Convert.ToInt32(reader[0].ToString());

                    PerformDataFill(ref reader);
                }
                reader.Close();
                rData.closeReadConnection();
            }
            else if (tbBezeichnung.Text != "")
            {
                int found = rData.getCount("speisekarte", "Bezeichnung", tbBezeichnung.Text.Trim());
                if (found == 1)
                {
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("speisekarte", "Bezeichnung", tbBezeichnung.Text.Trim());
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
                    //listView1.Visible = true;
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("speisekarte", "Bezeichnung", tbBezeichnung.Text.Trim());
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["ArtikelNr"].ToString());
                            item.SubItems.Add(reader["Bezeichnung"].ToString());
                            item.SubItems.Add(reader["verkaufpreis"].ToString());
                            lvArtikel.Items.Add(item);
                        }
                    }
                    reader.Close();
                    rData.closeReadConnection();
                    MessageBox.Show("Mehere daten gefunden wählen sie aus der liste");
                }
                else
                {
                    MessageBox.Show("Daten nicht gefunden");
                }
            }
            else
            {
                MessageBox.Show("Für Suchen Bitte geben sie Kunden Nummer oder Kunden Name ein");
            }
            if (conn.State.ToString() != "Closed")
                conn.Close();
        }

        private void frmSpeiseKarte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                PerformSearch(tbArtikel.Text);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnSpeichern.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnÄndern.PerformClick();
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnLöschen.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnZuruck.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                button9.PerformClick();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            myClearForm();
        }

        private void myClearForm()
        {
            tbArtikel.Clear();
            tbBezeichnung.Clear();
            tbMwSt.Clear();
            tbVerkaufPreis.Clear();
            tbZusatz.Clear();
            tbArtikel.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbArtikel.Text != "") // Update Record
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("speisekarte", "ArtikelNr", tbArtikel.Text.Trim());
                if (reader.Read())
                {
                    MessageBox.Show("Artikel Exsistiert Schon");
                    recordNr = Convert.ToInt32(reader[0].ToString());
                    PerformDataFill(ref reader);
                }
                else // Füge Neuen Record
                {
                    if (tbBezeichnung.Text == "")
                    {
                        MessageBox.Show("Bitte geben sie den ArtikelBezeichnung");
                        tbBezeichnung.Focus();
                    }
                    else if (tbVerkaufPreis.Text == "")
                    {
                        MessageBox.Show("Bitte geben sie Verkauf Preis");
                        tbVerkaufPreis.Focus();
                    }
                    else if (tbMwSt.Text == "")
                    {
                        MessageBox.Show("Bitte geben sie Mehrwehrsteur(MwSt)");
                        tbMwSt.Focus();
                    }
                    else
                    {
                        MySqlCommand cmd1 = new MySqlCommand();
                        try
                        {
                            conn.Open();
                            cmd1.Connection = conn;
                            cmd1.Parameters.Clear();
                            cmd1.CommandText = "INSERT INTO dbbari.speisekarte VALUES (NULL, @ArtikelNr , @Bezeichnung, @Zusatz, @VerkaufPreis, @MwSt, @pfandvar)";
                            cmd1.Prepare();
                            cmd1.Parameters.AddWithValue("ArtikelNr", tbArtikel.Text.Trim());
                            cmd1.Parameters.AddWithValue("Bezeichnung", tbBezeichnung.Text.Trim());
                            cmd1.Parameters.AddWithValue("Zusatz", tbZusatz.Text.Trim());
                            cmd1.Parameters.AddWithValue("VerkaufPreis", Convert.ToDouble(tbVerkaufPreis.Text.Trim()));
                            cmd1.Parameters.AddWithValue("MwSt", Convert.ToDouble(tbMwSt.Text.Trim()));
                            if (pfandBox.SelectedText != "")
                                cmd1.Parameters.AddWithValue("pfandvar", pfandBox.SelectedText);
                            else
                                cmd1.Parameters.AddWithValue("pfandvar", "OHNE");

                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Artikel Eingefügt");
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
                PerformListFill();
            }
        }

        private void PerformDataFill(ref MySqlDataReader reader)
        {
            myClearForm();
            recordNr = Convert.ToInt32(reader[0].ToString());
            tbArtikel.Text = reader["ArtikelNr"].ToString();
            tbBezeichnung.Text = reader["Bezeichnung"].ToString();
            tbMwSt.Text = reader["Mwst"].ToString();
            tbZusatz.Text = reader["zusatz"].ToString();
            tbVerkaufPreis.Text = reader["VerkaufPreis"].ToString();
        }

        private void PerformListFill()
        {
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("Speisekarte");
            lvArtikel.Items.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(reader["ArtikelNr"].ToString());
                        item.SubItems.Add(reader["Bezeichnung"].ToString());
                        item.SubItems.Add(reader["Verkaufpreis"].ToString());
                        lvArtikel.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
            lvArtikel.Visible = true;
        }

        private void btnÄndern_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand(); ;
            cmd1.Connection = conn;

            cmd1.Parameters.Clear();

            try
            {
                cmd1.CommandText = "UPDATE dbbari.speisekarte SET ArtikelNr=?ArtikelNr , Bezeichnung=?Bezeichnung, Zusatz=?Zusatz, VerkaufPreis=?VerkaufPreis, MwSt=?MwSt WHERE idSpeiseKarte=" + recordNr + ";";
                cmd1.Prepare();
                cmd1.Parameters.Add("ArtikelNr", MySqlDbType.VarChar).Value = tbArtikel.Text.Trim();
                cmd1.Parameters.Add("Bezeichnung", MySqlDbType.VarChar).Value = tbBezeichnung.Text.Trim();
                cmd1.Parameters.Add("Zusatz", MySqlDbType.VarChar).Value = tbZusatz.Text.Trim();
                cmd1.Parameters.Add("VerkaufPreis", MySqlDbType.Double).Value = Convert.ToDouble(tbVerkaufPreis.Text.Trim());
                cmd1.Parameters.Add("MwSt", MySqlDbType.VarChar).Value = tbMwSt.Text.Trim();
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Artikel Daten sind Geändert");
                lvArtikel.Items.Clear();
                conn.Close();
                PerformListFill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Insert Error");
            }
        }

        private void lvArtikel_MouseDoubleClick(object sender, EventArgs e)
        {
            int lstIndex = Convert.ToInt32(lvArtikel.SelectedIndices[0].ToString());
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("speisekarte", "ArtikelNr", lvArtikel.Items[lstIndex].Text.Trim());
            if (reader.Read())
            {
                recordNr = Convert.ToInt32(reader[0].ToString());
                PerformDataFill(ref reader);
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void lvArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnLöschen_Click(object sender, EventArgs e)
        {
            if (tbArtikel.Text != "" && tbBezeichnung.Text != "")
            {
                if (conn.State.ToString() == "Closed") conn.Open();

                DialogResult result;
                result = MessageBox.Show("Sind sie sicher dass Artikel '" + tbBezeichnung.Text + "' Löchen Möchten! ", "Vorsicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string sql = "DELETE FROM dbbari.speisekarte Where idSpeiseKarte=" + recordNr + ";";
                    cmd = new MySqlCommand(sql, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Daten sind gelöscht");
                        button9.PerformClick();
                        conn.Close();
                        PerformListFill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmZutaten obj = new frmZutaten();
            obj.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }
    }
}