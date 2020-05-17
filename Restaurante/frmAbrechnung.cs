using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class frmAbrechnung : Form
    {
        /// <summary>

        #region printable list view variables

        private PrintListView lvObj;

        private chooseView objChooseView;

        #endregion printable list view variables

        /// </summary>
        private static string connStr = Globals.connString;

        private MySqlConnection conn = new MySqlConnection(connStr);
        private MySqlConnection conn2 = new MySqlConnection(connStr);
        public int recordNr, recordCount, rollNo = 0;
        private MySqlCommand cmd;
        private MySqlDataReader rdr;
        private RestauranteData rData;
        public bool monat = false, Jahr = false;
        public string datestring = System.DateTime.Now.ToShortDateString();
        public double gesamt;

        #region mitarberiter count

        public int mitarbetierCount;
        public string[] mitarbeiter;

        public int getMitarbeiterCount()
        {
            return rData.getCount("mitarbeiter", "Tatigkeit", "Fahrer");
        }

        public string[] loadFahrer()
        {
            string[] temp = new string[mitarbetierCount];
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("mitarbeiter", "Tatigkeit", "Fahrer");
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    temp[i] = reader[1].ToString();
                    i++;
                }
            }
            reader.Close();
            rData.closeReadConnection();
            return temp;
        }

        #endregion mitarberiter count

        /// <summary>
        /// Variablen für Tages Übersicht stri
        private double MwSt7_Text = 0;

        private double MwSt19_Text = 0;
        private double Hausverkauf_Text = 0;
        private double Ausserhaus_Text = 0;
        private double Abholer_Text = 0;
        private double Umsatz_Text = 0;

        /// </summary>
        public frmAbrechnung()
        {
            InitializeComponent();
            rData = new RestauranteData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbrechnung_Load(object sender, EventArgs e)
        {
            mitarbetierCount = getMitarbeiterCount();
            mitarbeiter = loadFahrer();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(frmAbrechnung_KeyDown);
            PerformListFill(dateTimePicker1.Value.ToShortDateString());
        }

        private void SetToZero()
        {
            // setting all values to zero
            MwSt7_Text = 0;
            MwSt19_Text = 0;
            Hausverkauf_Text = 0;
            Ausserhaus_Text = 0;
            Abholer_Text = 0;
            Umsatz_Text = gesamt;
        }

        private void dateTimePicker1_TextChanged(object sender, EventArgs e)
        {
            // setting all values to zero
            MwSt7_Text = 0;
            MwSt19_Text = 0;
            Hausverkauf_Text = 0;
            Ausserhaus_Text = 0;
            Abholer_Text = 0;
            Umsatz_Text = gesamt;
            ///
            if (monat == true && Jahr == false)
                PerformListFill(dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Year.ToString());
            else if (Jahr == true && monat == false)
                PerformListFill(Convert.ToInt32(dateTimePicker1.Value.Year.ToString()));
            else if (Jahr == false && monat == false)
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
        }

        private void PerformListFill(string datum)
        {
            try
            {
                conn.Open();
                listView1.Items.Clear();
                string sql = "SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno, dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "';";
                cmd = new MySqlCommand(sql, conn);
                gesamt = 0;

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
                        datestring = datum;
                        string tempKundernNr, tempKundenName, tempRestBetrag;
                        ListViewItem item = new ListViewItem(rdr["idRechnung"].ToString());
                        tempKundernNr = rdr["KundenNr"].ToString();
                        item.SubItems.Add(tempKundernNr);
                        tempKundenName = rdr["idKundenDaten"].ToString() + " - " + rdr["KundenName"].ToString() + " - " + rdr["Strasse"].ToString() + "." + rdr["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(rdr["Datum"].ToString());
                        item.SubItems.Add(rdr["Zeit"].ToString());
                        tempRestBetrag = rdr["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(rdr["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(rdr["BestellNr"].ToString());

                        // --------------- Für Tages Übersicht-------------------
                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(rdr["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(rdr["Mwst7"].ToString());
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
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void PerformListFill(string monat, string Jahr)
        {
            string datum = monat + "." + Jahr;
            //MessageBox.Show(datum);
            conn.Open();
            listView1.Items.Clear();
            string sql = "SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung WHERE dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum like '%" + datum + "';";
            cmd = new MySqlCommand(sql, conn);
            gesamt = 0;
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
                        datestring = datum;
                        string tempKundenName, tempRestBetrag, tempKundenNr;
                        ListViewItem item = new ListViewItem(rdr["idRechnung"].ToString());
                        tempKundenNr = rdr["KundenNr"].ToString();
                        item.SubItems.Add(tempKundenNr);
                        tempKundenName = rdr["idKundenDaten"].ToString() + " - " + rdr["KundenName"].ToString() + " - " + rdr["Strasse"].ToString() + "." + rdr["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(rdr["Datum"].ToString());
                        item.SubItems.Add(rdr["Zeit"].ToString());
                        tempRestBetrag = rdr["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(rdr["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(rdr["BestellNr"].ToString());
                        // --------------- Für Monats Übersicht-------------------

                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(rdr["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(rdr["Mwst7"].ToString());
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
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void PerformListFill(int Jahr)
        {
            string datum = Jahr.ToString();
            //MessageBox.Show(datum);
            conn.Open();
            listView1.Items.Clear();
            string sql = "SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum like '%" + datum + "';";
            cmd = new MySqlCommand(sql, conn);
            gesamt = 0;
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
                        datestring = Jahr.ToString();
                        string tempKundenName, tempRestBetrag, tempKundenNr;
                        ListViewItem item = new ListViewItem(rdr["idRechnung"].ToString());
                        tempKundenNr = rdr["KundenNr"].ToString();
                        item.SubItems.Add(tempKundenNr);
                        tempKundenName = rdr["idKundenDaten"].ToString() + " - " + rdr["KundenName"].ToString() + " - " + rdr["Strasse"].ToString() + "." + rdr["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(rdr["Datum"].ToString());
                        item.SubItems.Add(rdr["Zeit"].ToString());
                        tempRestBetrag = rdr["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(rdr["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(rdr["BestellNr"].ToString());
                        // --------------- Für Monats Übersicht-------------------

                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(rdr["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(rdr["Mwst7"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString() + "Here");
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
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void frmAbrechnung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                button1.PerformClick();// Storno
            }
            else if (e.KeyCode == Keys.F3)
            {
                button3.PerformClick();//Drucken
            }
            else if (e.KeyCode == Keys.F4)
            {
                button5.PerformClick();// Abbruch
            }
            else if (e.KeyCode == Keys.F9)
            {
                if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].SubItems[6].Text != "Hausverkauf")

                    listView1.SelectedItems[0].SubItems[6].Text = getFahrer();
            }
        }

        private string getFahrer()
        {
            string catchName = "Fehler";
            if (rollNo < mitarbetierCount)
            {
                catchName = mitarbeiter[rollNo];
                rollNo++;
            }
            else
            {
                rollNo = 0;
                catchName = mitarbeiter[rollNo];
            }

            if (conn.State.ToString() == "Closed")
                conn.Open();
            //Add this to UpdateFahrer
            MySqlCommand cmd1 = new MySqlCommand(); ;
            cmd1.Connection = conn;

            cmd.Parameters.Clear();

            try
            {
                // idrechnung from list
                cmd1.CommandText = "UPDATE dbbari.abbrechnung SET Fahrer=?catch WHERE idrechnung=" + Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) + ";";
                cmd1.Prepare();
                cmd1.Parameters.Add("catch", MySqlDbType.VarChar).Value = catchName;
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();

            return catchName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.FlatStyle == FlatStyle.Standard && Jahr == false)
            {
                button4.FlatStyle = FlatStyle.Flat;
                button4.BackColor = Color.LightGreen;
                monat = true;
                button6.Text = "Monatsübersicht";
                PerformListFill(dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Year.ToString());
            }
            else if (monat)
            {
                monat = false;
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
                button6.Text = "Tagesübersicht";
                button4.FlatStyle = FlatStyle.Standard;
                button4.UseVisualStyleBackColor = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.FlatStyle == FlatStyle.Standard && monat == false)
            {
                button2.FlatStyle = FlatStyle.Flat;
                button2.BackColor = Color.LightGreen;
                Jahr = true;
                button6.Text = "Jahresübersicht";
                PerformListFill(Convert.ToInt32(dateTimePicker1.Value.Year.ToString()));
            }
            else if (Jahr)
            {
                Jahr = false;
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
                button6.Text = "Tagesübersicht";
                button2.FlatStyle = FlatStyle.Standard;
                button2.UseVisualStyleBackColor = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmBestellungDetails frm1 = new frmBestellungDetails(Convert.ToInt32(listView1.SelectedItems[0].Text), Convert.ToInt32(listView1.SelectedItems[0].SubItems[7].Text));
            frm1.Show();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            DialogResult question = DialogResult.No;
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.A)
                question = MessageBox.Show("Sind sie Sicher möchten Alle Daten Löschen", "!", MessageBoxButtons.YesNo);
            if (question == DialogResult.Yes)
                DeleteRecords();
        }

        private void DeleteRecords()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                //  MessageBox.Show(item.SubItems[0].Text);
                int rechnungNr = Convert.ToInt32(item.SubItems[0].Text);
                string sql1 = "DELETE FROM dbbari.abbrechnung Where idrechnung=" + rechnungNr + ";"; // Delete string the Rechnung from Table Abrechnung
                string sql2 = "DELETE FROM dbbari.bestellung Where RechnungNr=" + rechnungNr + ";"; // Delete string  the Rechnung from Table Bestellung
                if (conn.State.ToString() == "Closed")
                    conn.Open();
                cmd = new MySqlCommand(sql1, conn);

                try
                {
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ein fehler ist aufgetraten/n" + ex.ToString());
                }
            }
            PerformListFill(dateTimePicker1.Value.ToShortDateString());

            MessageBox.Show("Alle Bestellung Sind Gelöscht");
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            rollNo = 0;
        }

        private void listView1_LostFocus(object sender, EventArgs e)
        {
            rollNo = 0;
            /*
            MySqlCommand cmd1 = new MySqlCommand(); ;
            cmd1.Connection = conn;

            cmd.Parameters.Clear();

            try
            {
                cmd1.CommandText = "UPDATE dbbari.abbrechnung SET Fahrer=?catch WHERE idrechnung=" + Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text) + ";";
                cmd1.Prepare();
                cmd1.Parameters.Add("catch", MySqlDbType.VarChar).Value = catchName;
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            MessageBox.Show("Lost Focus");*/
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lvObj = new PrintListView(listView1);
            lvObj.TitleHeadFont = new Font("Arial", 14, FontStyle.Bold);
            lvObj.TitleHead_Text = "U M S A T Z J O U R N A L";
            lvObj.TitleFont = new Font("Arial", 14, FontStyle.Bold);
            lvObj.Title_Text = "Auftragübersicht für " + datestring;
            lvObj.Date_Text = "Datum : " + System.DateTime.Now.ToShortDateString() + ", Zeit :" + System.DateTime.Now.ToShortTimeString();
            lvObj.PrintDateFont = new Font("Arial", 12, FontStyle.Italic);
            lvObj.summe_Text = textBox1.Text;
            if (objChooseView == null)
                lvObj.PrintDatetext = "Alle Sätze";
            else
                lvObj.PrintDatetext = objChooseView.selectedFahrer;
            //lvObj.FitToPage = true;
            //   lvObj.summesatz = textBox1.Text;
            //    lvObj.printsumme = true;
            //    lvObj.Visible = false;
            //   lvObj.setToA5();
            int a5index = 0;
            System.Drawing.Printing.PaperSize pkSize;
            for (int i = 0; i < lvObj.PrinterSettings.PaperSizes.Count; i++)
            {
                pkSize = lvObj.PrinterSettings.PaperSizes[i];
                if (pkSize.PaperName.ToString() == "A5")
                {
                    a5index = i;
                    break;
                }
            }
            if (lvObj.PrinterSettings.PaperSizes[a5index].PaperName == "A5")
                lvObj.DefaultPageSettings.PaperSize = lvObj.PrinterSettings.PaperSizes[a5index];

            lvObj.Print();

            //   MessageBox.Show(ex.Message.ToString()+"Fek");
            /*clNr = new ColumnHeader();
            clNr.Text = "Nr";
           clNr.Width = 50;
           clTelefone = new ColumnHeader();
            clTelefone.Text = "Telefon";
            clTelefone.Width = 132;
            clAddress = new ColumnHeader();
            clAddress.Text = "KNr - K.Name - Addresse";
            clAddress.Width = 267;
            clDatum = new ColumnHeader();
            clDatum.Text = "Datum";
            clDatum.Width = 112;
            clZeit = new ColumnHeader();
            clZeit.Text = "Zeit";
            clZeit.Width = 82;
            clBetrag = new ColumnHeader();
            clBetrag.Text = "Betrag";
            clBetrag.Width = 86;
            clFahrer = new ColumnHeader();
            clFahrer.Text = "Fahrer";
            clFahrer.Width = 108;
            lvObj = new PrintableListView2();
            lvObj.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clNr,clTelefone,clAddress,clDatum,clZeit,clBetrag,clFahrer});
           // string Id,Telefone,address,datum,zeit,betrag,fahrer;
            ListView.ListViewItemCollection coll = listView1.Items;

            foreach (ListViewItem item in coll)
            {
                string[] temp =     {    item.SubItems[7].Text,item.SubItems[1].Text,item.SubItems[2].Text,item.SubItems[3].Text, item.SubItems[4].Text,item.SubItems[5].Text,  item.SubItems[6].Text};
                itemClone = new ListViewItem(temp);
                lvObj.Items.Add(itemClone);
            }

            lvObj.TitleFont = new Font("Arial", 14, FontStyle.Bold);
            lvObj.Title = "U M S A T Z J O U R N A L";
            lvObj.TitleFont2 = new Font("Arial", 14, FontStyle.Bold);
            lvObj.Title2 = "Auftragübersicht für " + datestring;
            lvObj.Title3 = "Datum : " + System.DateTime.Now.ToShortDateString() + ", Zeit :" + System.DateTime.Now.ToShortTimeString();
            lvObj.TitleFont3 = new Font("Arial", 12, FontStyle.Italic);
            lvObj.FitToPage = true;
            lvObj.summesatz = textBox1.Text;
            lvObj.printsumme = true;
            lvObj.Visible = false;
            lvObj.setToA5();

            lvObj.Print();
            //lvObj.PrintPreview();
           // lvObj.setToA4();

            lvObj.Dispose();
           /* ----old-------------
            * ListView.ColumnHeaderCollection abc = listView1.Columns;
            listView1.TitleFont = new Font("Arial", 14, FontStyle.Bold);
            listView1.Title = "U M S A T Z J O U R N A L";
            listView1.TitleFont2 = new Font("Arial", 14, FontStyle.Bold);
            listView1.Title2 = "Auftragübersicht für "+datestring;
            listView1.Title3 = "Datum : " + System.DateTime.Now.ToShortDateString() + ", Zeit :" + System.DateTime.Now.ToShortTimeString();
            listView1.TitleFont3 = new Font("Arial", 12, FontStyle.Italic);
            listView1.FitToPage = true;
            listView1.summesatz =  textBox1.Text;
            listView1.printsumme = true;

           // listView1.Columns.Remove(Fahrer);
          // listView1.Columns.Remove(columnHeader1);
            listView1.PrintPreview();
            // printPreviewDialog1.Document = printDocument1;
           // printPreviewDialog1.ShowDialog();
          //  listView1.Columns.Add(Fahrer);
            //listView1.Columns.Add(columnHeader1);
            listView1.Visible = true;*/
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Sind sie sicher dass Sie möchten Stornieren daten hier gelöscht sind nicht wiederufbar ", "Vorsicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int rechnungNr = 0;
                if (listView1.SelectedItems.Count == 1)
                {
                    rechnungNr = Convert.ToInt32(listView1.SelectedItems[0].Text);
                    string sql1 = "DELETE FROM dbbari.abbrechnung Where idrechnung=" + rechnungNr + ";"; // Delete string the Rechnung from Table Abrechnung
                    string sql2 = "DELETE FROM dbbari.bestellung Where RechnungNr=" + rechnungNr + ";"; // Delete string  the Rechnung from Table Bestellung
                    if (conn.State.ToString() == "Closed")
                        conn.Open();
                    cmd = new MySqlCommand(sql1, conn);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand(sql2, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        if (monat)
                            PerformListFill(dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Year.ToString());
                        else if (Jahr)
                            PerformListFill(Convert.ToInt32(dateTimePicker1.Value.Year.ToString()));
                        else
                            PerformListFill(dateTimePicker1.Value.ToShortDateString());
                        MessageBox.Show("Die Bestellung ist erfolgreich Storniert");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ein fehler ist aufgetraten/n" + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Bitte Selectieren sie ein list element ");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)// Tages übersicht

        {
            PrintTagesUebersicht Tobj = new PrintTagesUebersicht("Tagesübersicht");
            Tobj.Date_Text = dateTimePicker1.Text;
            if (monat)
            {
                Tobj = new PrintTagesUebersicht("Monatsübersicht");
                Tobj.Date_Text = dateTimePicker1.Text;
            }
            Tobj.TitleHead_Text = "Tamarinde";
            Tobj.MwSt7_Text = this.MwSt7_Text;
            Tobj.MwSt19_Text = this.MwSt19_Text;
            Tobj.Hausverkauf_Text = this.Hausverkauf_Text;
            Tobj.DocumentName = "Tagesübersicht";

            Tobj.Ausserhaus_Text = this.Ausserhaus_Text;
            Tobj.Abholer_Text = this.Abholer_Text;
            Tobj.Umsatz_Text = Convert.ToDouble(textBox1.Text);
            Tobj.PrintDocument();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            objChooseView = new chooseView();
            objChooseView.ShowDialog();
            SetToZero();// sset all Mwst stuff to zero
            if (objChooseView.selectedFahrer == "Alle Sätze")
            {
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
            }
            else if (objChooseView.selectedFahrer == "Abholer")
            {
                string datum = dateTimePicker1.Value.ToShortDateString();
                loadList("SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "' and dbbari.Kundendaten.KundenName='" + objChooseView.selectedFahrer + "';");
            }
            else if (objChooseView.selectedFahrer == "Hausverkauf")
            {
                string datum = dateTimePicker1.Value.ToShortDateString();
                loadList("SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "' and dbbari.Kundendaten.KundenName='" + objChooseView.selectedFahrer + "';");
            }
            else
            {
                string datum = dateTimePicker1.Value.ToShortDateString();
                loadList("SELECT dbbari.kundendaten.idkundendaten,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenName,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "' and dbbari.abbrechnung.Fahrer='" + objChooseView.selectedFahrer + "';");
            }
        }

        private void loadList(string sql)
        {
            string datum = dateTimePicker1.Value.ToShortDateString();
            conn.Open();
            listView1.Items.Clear();

            cmd = new MySqlCommand(sql, conn);
            gesamt = 0;
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
                        datestring = datum;
                        string tempKundernNr, tempKundenName, tempRestBetrag;
                        ListViewItem item = new ListViewItem(rdr["idRechnung"].ToString());
                        tempKundernNr = rdr["KundenNr"].ToString();
                        item.SubItems.Add(tempKundernNr);
                        tempKundenName = rdr["idKundenDaten"].ToString() + " - " + rdr["KundenName"].ToString() + " - " + rdr["Strasse"].ToString() + "." + rdr["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(rdr["Datum"].ToString());
                        item.SubItems.Add(rdr["Zeit"].ToString());
                        tempRestBetrag = rdr["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(rdr["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(rdr["BestellNr"].ToString());
                        // --------------- Für Tages Übersicht-------------------
                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(rdr["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(rdr["Mwst7"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString() + "Here");
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
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dateSelection frmdate = new dateSelection(dateTimePicker1.Value, dateTimePicker1.Value.AddDays(-1 * (dateTimePicker1.Value.Day - 1)));
            frmdate.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            chooseView objChooseView = new chooseView();
            objChooseView.ShowDialog();
            SetToZero();// sset all Mwst stuff to zero
            if (objChooseView.selectedFahrer == "Alle Sätze")
            {
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
            }
            else if (objChooseView.selectedFahrer == "Abholer")
            {
                string datum = dateTimePicker1.Value.ToShortDateString();
                loadList("SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "' and dbbari.Kundendaten.KundenName='" + objChooseView.selectedFahrer + "';");
            }
            else if (objChooseView.selectedFahrer == "Hausverkauf")
            {
                string datum = dateTimePicker1.Value.ToShortDateString();
                loadList("SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "' and dbbari.Kundendaten.KundenName='" + objChooseView.selectedFahrer + "';");
            }
            else
            {
                string datum = dateTimePicker1.Value.ToShortDateString();
                loadList("SELECT dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr FROM dbbari.Kundendaten,dbbari.abbrechnung where dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum='" + datum + "' and dbbari.abbrechnung.Fahrer='" + objChooseView.selectedFahrer + "';");
            }

            FahrerAbrechnung obj = new FahrerAbrechnung(listView1.Items, gesamt);
            //
            obj.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // gesamt_umsatz = Convert.ToDouble(textBox1.Text);
        }
    }
}