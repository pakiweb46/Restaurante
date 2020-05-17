using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class chooseView : Form
    {
        private static string connStr = Globals.connString;
        private MySqlConnection conn = new MySqlConnection(connStr);
        public string selectedFahrer;

        public chooseView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private MySqlCommand cmd;
        private MySqlDataReader rdr;

        private void chooseView_Load(object sender, EventArgs e)
        {
            conn.Open();
            listView1.Items.Clear();
            listView1.Items.Add("Alle Sätze");
            listView1.Items.Add("Hausverkauf");

            string sql = "SELECT * From dbbari.mitarbeiter WHERE Tatigkeit='Fahrer' ";

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
                        ListViewItem item = new ListViewItem(rdr["MitarbeiterName"].ToString());

                        listView1.Items.Add(item);
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
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection item = listView1.SelectedItems;
            selectedFahrer = item[0].Text;

            this.Hide();
        }
    }
}