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
    public partial class frmZutaten : Form
    {
        static string connStr = Class1.connString;
        private int recordNr;
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlCommand cmd,cmd2;
        MySqlDataReader rdr;

        public frmZutaten()
        {
            InitializeComponent();
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
            //if (conn.State.ToString() == "Close")
            conn.Open();
            string sql;
            
            if (tbBezeichnung.Text != "")
            {
                int found = 0; // Number of matching records found
                sql = "SELECT count(*) FROM dbbari.zutaten Where ZutatName='" + tbBezeichnung.Text.Trim() + "';";

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
                    sql = "SELECT * FROM dbbari.zutaten Where ZutatName='" + tbBezeichnung.Text.Trim() + "';";
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
                    sql = "SELECT * FROM dbbari.zutaten Where ZutatName='" + tbBezeichnung.Text.Trim() + "';";
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
                            ListViewItem item = new ListViewItem(rdr["idZutaten"].ToString());
                            item.SubItems.Add(rdr["ZutatName"].ToString());
                            item.SubItems.Add(rdr["Preis"].ToString());
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
        private void PerformDataFill()
        {
            myClearForm();
            recordNr = Convert.ToInt32(rdr[0].ToString());
            tbArtikel.Text = rdr["idZutaten"].ToString();
            tbBezeichnung.Text = rdr["ZutatName"].ToString();
            tbMwSt.Text = rdr["Mwst"].ToString();
            tbVerkaufPreis.Text = rdr["Preis"].ToString();

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if(conn.State.ToString()=="Closed")
            conn.Open();
            if (tbBezeichnung.Text != "") // Update Record
            {
                sql = "SELECT * FROM dbbari.zutaten Where ZutatName='" + tbBezeichnung.Text + "';";
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
                    MessageBox.Show("Diese Zutat Exsistiert Schon");
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
                            cmd1.CommandText = "INSERT INTO dbbari.Zutaten VALUES (NULL,  @Bezeichnung, @VerkaufPreis, @MwSt)";
                            cmd1.Prepare();
                            cmd1.Parameters.AddWithValue("Bezeichnung", tbBezeichnung.Text.Trim());
                            cmd1.Parameters.AddWithValue("VerkaufPreis", Convert.ToDouble(tbVerkaufPreis.Text.Trim()));
                            cmd1.Parameters.AddWithValue("MwSt", Convert.ToDouble(tbMwSt.Text.Trim()));
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Zutat Eingefügt");

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
        private void PerformListFill()
        {
            string sql;
            sql = "SELECT * From dbbari.zutaten ";
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
                        ListViewItem item = new ListViewItem(rdr["idZutaten"].ToString());
                        item.SubItems.Add(rdr["ZutatName"].ToString());
                        item.SubItems.Add(rdr["Preis"].ToString());
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
                cmd1.CommandText = "UPDATE dbbari.Zutaten SET  ZutatName=?Bezeichnung, Preis=?VerkaufPreis, MwSt=?MwSt WHERE idZutaten=" + recordNr + ";";
                cmd1.Prepare();
                cmd1.Parameters.Add("Bezeichnung", MySqlDbType.VarChar).Value = tbBezeichnung.Text.Trim();
                cmd1.Parameters.Add("VerkaufPreis", MySqlDbType.Double).Value = Convert.ToDouble(tbVerkaufPreis.Text.Trim());
                cmd1.Parameters.Add("MwSt", MySqlDbType.VarChar).Value = tbMwSt.Text.Trim();
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Zutat Daten sind Geändert");
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
            string sql;
            int lstIndex;
            conn.Open();
            lstIndex = Convert.ToInt32(lvArtikel.SelectedIndices[0].ToString());
            sql = "SELECT * FROM dbbari.zutaten Where idZutaten='" + lvArtikel.Items[lstIndex].Text.Trim() + "';";
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
                if (conn.State.ToString() == "Closed") conn.Open();

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
