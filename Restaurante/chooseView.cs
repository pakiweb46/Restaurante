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
        private RestauranteData rData;

        public chooseView()
        {
            InitializeComponent();
            rData = new RestauranteData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void chooseView_Load(object sender, EventArgs e)
        {
            conn.Open();
            listView1.Items.Clear();
            listView1.Items.Add("Alle Sätze");
            listView1.Items.Add("Hausverkauf");
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("mitarbeiter", "Tatigkeit", "Fahrer");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(reader["MitarbeiterName"].ToString());

                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            reader.Close();
            rData.closeReadConnection();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection item = listView1.SelectedItems;
            selectedFahrer = item[0].Text;

            this.Hide();
        }
    }
}