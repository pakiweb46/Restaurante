using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Windows.Forms;

//TODO:: NEXT Previous doesnt function
namespace Restaurante
{
    public partial class frmZutaten : Form
    {
        private static string connStr = Globals.connString;
        private int recordNr;
        private MySqlConnection conn = new MySqlConnection(connStr);
        private MySqlCommand cmd;
        private RestauranteData rData;
        private IFormatProvider providerEn;

        public frmZutaten()
        {
            InitializeComponent();
            rData = new RestauranteData();
            providerEn = CultureInfo.CreateSpecificCulture("en-GB");
        }

        private void frmZutaten_Load(object sender, EventArgs e)
        {
            tbArtikel.Enabled = false;
            lvArtikel.Visible = false;
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmZutaten_KeyDown);
            PerformListFill();
        }

        private void frmZutaten_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
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
            else if (e.KeyCode == Keys.F1)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            if (tbBezeichnung.Text != "")
            {
                // Number of matching records found
                int found = rData.getCount("zutaten", "ZutatName", tbBezeichnung.Text.Trim());
                if (found == 1)
                {
                    rData.openReadConnection();
                    MySqlDataReader reader = rData.getDataReader("zutaten", "ZutatName", tbBezeichnung.Text.Trim());

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
                    MySqlDataReader reader = rData.getDataReader("zutaten", "ZutatName", tbBezeichnung.Text.Trim());
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["idZutaten"].ToString());
                            item.SubItems.Add(reader["ZutatName"].ToString());
                            item.SubItems.Add(reader["Preis"].ToString());
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
            tbArtikel.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void PerformDataFill(ref MySqlDataReader reader)
        {
            myClearForm();
            recordNr = Convert.ToInt32(reader[0].ToString());
            tbArtikel.Text = reader["idZutaten"].ToString();
            tbBezeichnung.Text = reader["ZutatName"].ToString();
            tbMwSt.Text = reader["Mwst"].ToString();
            tbVerkaufPreis.Text = reader["Preis"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();
            if (tbBezeichnung.Text != "") // Update Record
            {
                rData.openReadConnection();
                MySqlDataReader reader = rData.getDataReader("zutaten", "ZutatName", tbBezeichnung.Text.Trim());
                if (reader.Read())
                {
                    MessageBox.Show("Diese Zutat Exsistiert Schon");
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
                        try
                        {
                            string[] values = { tbBezeichnung.Text.Trim(),
                                                Convert.ToDouble(tbVerkaufPreis.Text.Trim()).ToString(providerEn),
                                                Convert.ToDouble(tbMwSt.Text.Trim()).ToString(providerEn)
                                                };
                            rData.addData("Zutaten", values);
                            MessageBox.Show("Zutat Eingefügt");
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

        private void PerformListFill()
        {
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("zutaten");

            lvArtikel.Items.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(reader["idZutaten"].ToString());
                        item.SubItems.Add(reader["ZutatName"].ToString());
                        item.SubItems.Add(reader["Preis"].ToString());
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
            string[] variable = { "ZutatName", "Preis", "MwSt" };
            string[] value = { tbBezeichnung.Text.Trim(),
                               tbVerkaufPreis.Text.Trim(),
                               tbMwSt.Text.Trim() };
            rData.updateData("Zutaten", variable, value, "idZutaten", recordNr.ToString());
            MessageBox.Show("Zutat Daten sind Geändert");
            lvArtikel.Items.Clear();
            PerformListFill();
        }

        private void lvArtikel_MouseDoubleClick(object sender, EventArgs e)
        {
            int lstIndex;
            lstIndex = Convert.ToInt32(lvArtikel.SelectedIndices[0].ToString());
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("zutaten", "idZutaten", lvArtikel.Items[lstIndex].Text.Trim());
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
                conn.Open();
                DialogResult result;
                result = MessageBox.Show("Sind sie sicher dass Zutat '" + tbBezeichnung.Text + "' Löchen Möchten! ", "Vorsicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string sql = "DELETE FROM dbbari.zutaten Where idZutaten=" + recordNr + ";";
                    cmd = new MySqlCommand(sql, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Daten sind gelöscht");
                        button9.PerformClick();

                        PerformListFill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void btnZuruck_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}