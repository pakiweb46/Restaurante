﻿using MySql.Data.MySqlClient;
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

        private void btnAbbruch_Click(object sender, EventArgs e)
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
            listView1.Items.Clear();
            rData.openReadConnection();
            string filter = "dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno, dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr";
            string joint = "dbbari.abbrechnung.idKundendaten = dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum = '" + datum + "'";
            MySqlDataReader reader = rData.getDataReaderJoin("dbbari.Kundendaten", "dbbari.abbrechnung", joint, filter);
            gesamt = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        datestring = datum;
                        string tempKundernNr, tempKundenName, tempRestBetrag;
                        ListViewItem item = new ListViewItem(reader["idRechnung"].ToString());
                        tempKundernNr = reader["KundenNr"].ToString();
                        item.SubItems.Add(tempKundernNr);
                        tempKundenName = reader["idKundenDaten"].ToString() + " - " + reader["KundenName"].ToString() + " - " + reader["Strasse"].ToString() + "." + reader["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(reader["Datum"].ToString());
                        item.SubItems.Add(reader["Zeit"].ToString());
                        tempRestBetrag = reader["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(reader["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(reader["BestellNr"].ToString());

                        // --------------- Für Tages Übersicht-------------------
                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(reader["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(reader["Mwst7"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void PerformListFill(string monat, string Jahr)
        {
            string datum = monat + "." + Jahr;
            //MessageBox.Show(datum);
            rData.openReadConnection();
            string filter = "dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr";
            string joint = "dbbari.abbrechnung.idKundendaten = dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum like '%" + datum + "'";
            MySqlDataReader reader = rData.getDataReaderJoin("dbbari.Kundendaten", "dbbari.abbrechnung", joint, filter);
            listView1.Items.Clear();
            gesamt = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        datestring = datum;
                        string tempKundenName, tempRestBetrag, tempKundenNr;
                        ListViewItem item = new ListViewItem(reader["idRechnung"].ToString());
                        tempKundenNr = reader["KundenNr"].ToString();
                        item.SubItems.Add(tempKundenNr);
                        tempKundenName = reader["idKundenDaten"].ToString() + " - " + reader["KundenName"].ToString() + " - " + reader["Strasse"].ToString() + "." + reader["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(reader["Datum"].ToString());
                        item.SubItems.Add(reader["Zeit"].ToString());
                        tempRestBetrag = reader["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(reader["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(reader["BestellNr"].ToString());
                        // --------------- Für Monats Übersicht-------------------

                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(reader["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(reader["Mwst7"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void PerformListFill(int Jahr)
        {
            string datum = Jahr.ToString();
            //MessageBox.Show(datum);
            rData.openReadConnection();
            string filter = "dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr";
            string joint = "dbbari.abbrechnung.idKundendaten=dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum like '%" + datum + "'";
            MySqlDataReader reader = rData.getDataReaderJoin("dbbari.Kundendaten", "dbbari.abbrechnung", joint, filter);

            listView1.Items.Clear();
            gesamt = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        datestring = Jahr.ToString();
                        string tempKundenName, tempRestBetrag, tempKundenNr;
                        ListViewItem item = new ListViewItem(reader["idRechnung"].ToString());
                        tempKundenNr = reader["KundenNr"].ToString();
                        item.SubItems.Add(tempKundenNr);
                        tempKundenName = reader["idKundenDaten"].ToString() + " - " + reader["KundenName"].ToString() + " - " + reader["Strasse"].ToString() + "." + reader["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(reader["Datum"].ToString());
                        item.SubItems.Add(reader["Zeit"].ToString());
                        tempRestBetrag = reader["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(reader["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(reader["BestellNr"].ToString());
                        // --------------- Für Monats Übersicht-------------------

                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(reader["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(reader["Mwst7"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void frmAbrechnung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                btnStorno.PerformClick();// Storno
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDrucken.PerformClick();//Drucken
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnAbbruch.PerformClick();// Abbruch
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
            rData.updateSingleData("abbrechnung", "Fahrer", catchName, "idrechnung", listView1.SelectedItems[0].SubItems[0].Text);
            return catchName;
        }

        private void btnMonatAbrechnung_Click(object sender, EventArgs e)
        {
            if (btnMonatAbrechnung.FlatStyle == FlatStyle.Standard && Jahr == false)
            {
                btnMonatAbrechnung.FlatStyle = FlatStyle.Flat;
                btnMonatAbrechnung.BackColor = Color.LightGreen;
                monat = true;
                btnTagessicht.Text = "Monatsübersicht";
                PerformListFill(dateTimePicker1.Value.Month.ToString(), dateTimePicker1.Value.Year.ToString());
            }
            else if (monat)
            {
                monat = false;
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
                btnTagessicht.Text = "Tagesübersicht";
                btnMonatAbrechnung.FlatStyle = FlatStyle.Standard;
                btnMonatAbrechnung.UseVisualStyleBackColor = true;
            }
        }

        private void btnJahresAbrechnung_Click(object sender, EventArgs e)
        {
            if (btnJahresAbrechnung.FlatStyle == FlatStyle.Standard && monat == false)
            {
                btnJahresAbrechnung.FlatStyle = FlatStyle.Flat;
                btnJahresAbrechnung.BackColor = Color.LightGreen;
                Jahr = true;
                btnTagessicht.Text = "Jahresübersicht";
                PerformListFill(Convert.ToInt32(dateTimePicker1.Value.Year.ToString()));
            }
            else if (Jahr)
            {
                Jahr = false;
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
                btnTagessicht.Text = "Tagesübersicht";
                btnJahresAbrechnung.FlatStyle = FlatStyle.Standard;
                btnJahresAbrechnung.UseVisualStyleBackColor = true;
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
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnDrucken_Click(object sender, EventArgs e)
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
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
        }

        private void btnStorno_Click(object sender, EventArgs e)
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

        private void btnTagessicht_Click(object sender, EventArgs e)// Tages übersicht

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

        private void btnUbersicht_Click(object sender, EventArgs e)
        {
            objChooseView = new chooseView();
            objChooseView.ShowDialog();
            SetToZero();// sset all Mwst stuff to zero
            if (objChooseView.selectedFahrer == "Alle Sätze")
            {
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
            }
            else if (objChooseView.selectedFahrer == "Abholer" || objChooseView.selectedFahrer == "Hausverkauf")
            {
                loadList(false, objChooseView.selectedFahrer);
            }
            else
            {
                loadList(true, objChooseView.selectedFahrer);
            }
        }

        private void loadList(bool Fahrer, string selected)
        {
            string datum = dateTimePicker1.Value.ToShortDateString();
            rData.openReadConnection();
            string subfilter = "and dbbari.Kundendaten.KundenName = '";
            if (Fahrer)
                subfilter = "and dbbari.abbrechnung.Fahrer='";
            string filter = "dbbari.kundendaten.idkundendaten,dbbari.Kundendaten.KundenName,dbbari.kundendaten.strasse,dbbari.kundendaten.strno,dbbari.Kundendaten.KundenNr,dbbari.abbrechnung.idrechnung,dbbari.abbrechnung.datum,dbbari.abbrechnung.Zeit,dbbari.abbrechnung.RestBetrag,dbbari.abbrechnung.Fahrer,dbbari.abbrechnung.Mwst19,dbbari.abbrechnung.Mwst7,dbbari.abbrechnung.BestellNr";
            string joint = "dbbari.abbrechnung.idKundendaten = dbbari.Kundendaten.idKundendaten and dbbari.abbrechnung.datum = '" + datum + "' " + subfilter + selected + "'";
            MySqlDataReader reader = rData.getDataReaderJoin("dbbari.Kundendaten", "dbbari.abbrechnung", joint, filter);

            listView1.Items.Clear();
            gesamt = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        datestring = datum;
                        string tempKundernNr, tempKundenName, tempRestBetrag;
                        ListViewItem item = new ListViewItem(reader["idRechnung"].ToString());
                        tempKundernNr = reader["KundenNr"].ToString();
                        item.SubItems.Add(tempKundernNr);
                        tempKundenName = reader["idKundenDaten"].ToString() + " - " + reader["KundenName"].ToString() + " - " + reader["Strasse"].ToString() + "." + reader["Strno"].ToString();
                        item.SubItems.Add(tempKundenName);
                        item.SubItems.Add(reader["Datum"].ToString());
                        item.SubItems.Add(reader["Zeit"].ToString());
                        tempRestBetrag = reader["RestBetrag"].ToString();
                        item.SubItems.Add(tempRestBetrag);
                        item.SubItems.Add(reader["Fahrer"].ToString());
                        gesamt += Convert.ToDouble(tempRestBetrag);
                        listView1.Items.Add(item);
                        item.SubItems.Add(reader["BestellNr"].ToString());
                        // --------------- Für Tages Übersicht-------------------
                        if (tempKundenName == "Abholer")
                            Abholer_Text += Convert.ToDouble(tempRestBetrag);
                        else if (tempKundenName == "Hausverkauf")
                            Hausverkauf_Text += Convert.ToDouble(tempRestBetrag);
                        else
                            Ausserhaus_Text += Convert.ToDouble(tempRestBetrag);
                        MwSt19_Text += Convert.ToDouble(reader["Mwst19"].ToString());
                        MwSt7_Text += Convert.ToDouble(reader["Mwst7"].ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
            textBox1.Text = String.Format("{0:0.00}", gesamt).ToString();
            textBox2.Text = listView1.Items.Count.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void btnBerichte_Click(object sender, EventArgs e)
        {
            dateSelection frmdate = new dateSelection(dateTimePicker1.Value, dateTimePicker1.Value.AddDays(-1 * (dateTimePicker1.Value.Day - 1)));
            frmdate.ShowDialog();
        }

        private void btnFAbrechnung_Click(object sender, EventArgs e)
        {
            chooseView objChooseView = new chooseView();
            objChooseView.ShowDialog();
            SetToZero();// sset all Mwst stuff to zero
            if (objChooseView.selectedFahrer == "Alle Sätze")
            {
                PerformListFill(dateTimePicker1.Value.ToShortDateString());
            }
            else if (objChooseView.selectedFahrer == "Abholer" || objChooseView.selectedFahrer == "Hausverkauf")
            {
                loadList(false, objChooseView.selectedFahrer);
            }
            else
            {
                loadList(true, objChooseView.selectedFahrer);
            }

            FahrerAbrechnung obj = new FahrerAbrechnung(listView1.Items, gesamt);
            obj.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // gesamt_umsatz = Convert.ToDouble(textBox1.Text);
        }
    }
}