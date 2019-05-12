using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LieferDienst;
namespace Restaurante
{
    public partial class frmSpeiseKarte : Form
    {
        static string connStr = Globals.connString;
        private int recordNr;
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlCommand cmd,cmd2;
        MySqlDataReader rdr;
  
        public frmSpeiseKarte()
        {
            InitializeComponent();
        }

        private void frmSpeiseKarte_Load(object sender, EventArgs e)
        {
            pfandBox.Items.Add("OHNE");
            pfandBox.Items.Add("PFAND1");
            pfandBox.Items.Add("PFAND2");
            pfandBox.Items.Add("PFAND3");

            lvArtikel.Visible = false;
            KeyPreview = true;
            this.KeyDown+=new KeyEventHandler(frmSpeiseKarte_KeyDown);
            PerformListFill();
        }
        private void PerformSearch(string ArtikelNr)
        {
            //if (conn.State.ToString() == "Close")
                conn.Open();
            string sql;
            if (tbArtikel.Text != "")
            {
                sql = "SELECT * FROM dbbari.speisekarte Where ArtikelNr='" + ArtikelNr + "';";
                cmd2 = new MySqlCommand(sql, conn);
                
                try
                {
                    rdr = cmd2.ExecuteReader();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()+"h1");
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
                    MessageBox.Show(ex.ToString()+"h2");
                }

            }

            else if (tbBezeichnung.Text != "")
            {
                int found = 0; // Number of matching records found
                sql = "SELECT count(*) FROM dbbari.speisekarte Where Bezeichnung='" + tbBezeichnung.Text.Trim() + "';";

                cmd2 = new MySqlCommand(sql, conn);

                try
                {

                    rdr = cmd2.ExecuteReader();
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
                    sql = "SELECT * FROM dbbari.speisekarte Where Bezeichnung='" + tbBezeichnung.Text.Trim() + "';";
                    cmd2 = new MySqlCommand(sql, conn);

                    try
                    {

                        rdr = cmd2.ExecuteReader();
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
                    //listView1.Visible = true;
                    sql = "SELECT * FROM dbbari.speisekarte Where Bezeichnung='" + tbBezeichnung.Text.Trim() + "';";
                    cmd2 = new MySqlCommand(sql, conn);

                    try
                    {

                        rdr = cmd2.ExecuteReader();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ListViewItem item = new ListViewItem(rdr["ArtikelNr"].ToString());
                            item.SubItems.Add(rdr["Bezeichnung"].ToString());
                            item.SubItems.Add(rdr["verkaufpreis"].ToString());
                            lvArtikel.Items.Add(item);


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
            string sql;
            conn.Open();
            if (tbArtikel.Text != "") // Update Record
            {
                sql = "SELECT * FROM dbbari.speisekarte Where ArtikelNr='" + tbArtikel.Text.Trim() + "';";
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
                    MessageBox.Show("Artikel Exsistiert Schon");
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

                            cmd1.Connection = conn;
                            cmd1.Parameters.Clear();
                            cmd1.CommandText = "INSERT INTO dbbari.speisekarte VALUES (NULL, @ArtikelNr , @Bezeichnung, @Zusatz, @VerkaufPreis, @MwSt, @pfandvar)";
                            cmd1.Prepare();
                            cmd1.Parameters.AddWithValue("ArtikelNr", tbArtikel.Text.Trim());
                            cmd1.Parameters.AddWithValue("Bezeichnung", tbBezeichnung.Text.Trim());
                            cmd1.Parameters.AddWithValue("Zusatz", tbZusatz.Text.Trim());
                            cmd1.Parameters.AddWithValue("VerkaufPreis", Convert.ToDouble(tbVerkaufPreis.Text.Trim()));
                            cmd1.Parameters.AddWithValue("MwSt", Convert.ToDouble(tbMwSt.Text.Trim()));
                            if (pfandBox.SelectedText!="")
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
                    }
                }
                try
                {
                    conn.Close();
                    PerformListFill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void PerformDataFill()
        {
            myClearForm();
            recordNr = Convert.ToInt32(rdr[0].ToString());
            tbArtikel.Text = rdr["ArtikelNr"].ToString();
            tbBezeichnung.Text = rdr["Bezeichnung"].ToString();
            tbMwSt.Text = rdr["Mwst"].ToString();
            tbZusatz.Text = rdr["zusatz"].ToString();
            tbVerkaufPreis.Text = rdr["VerkaufPreis"].ToString();
            
        }
        
        private void PerformListFill()
        {
        string sql;
        sql = "SELECT * From dbbari.Speisekarte ";
            conn.Open();
            lvArtikel.Items.Clear();
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
                    try
                    {
                        ListViewItem item = new ListViewItem(rdr["ArtikelNr"].ToString());
                        item.SubItems.Add(rdr["Bezeichnung"].ToString());
                        item.SubItems.Add(rdr["Verkaufpreis"].ToString());
                        lvArtikel.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

            }
            try
            {
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            lvArtikel.Visible = true;
        }

        private void btnÄndern_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand(); ;
            cmd1.Connection = conn;
            
            cmd.Parameters.Clear();

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
                MessageBox.Show(ex.Message+"Insert Error");
            }
        }

        private void lvArtikel_MouseDoubleClick(object sender, EventArgs e)
        {
            string sql;
            int lstIndex;
            conn.Open();
           lstIndex=Convert.ToInt32(lvArtikel.SelectedIndices[0].ToString());            
                sql = "SELECT * FROM dbbari.speisekarte Where ArtikelNr='" + lvArtikel.Items[lstIndex].Text.Trim()+ "';";
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
                    try
                    {
                        rdr.Close();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }

        private void lvArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLöschen_Click(object sender, EventArgs e)
        {
            if (tbArtikel.Text != "" && tbBezeichnung.Text != "")
            {
                if (conn.State.ToString() == "Closed")        conn.Open();
            
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
