using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class dateSelection : Form
    {
        public dateSelection()
        {
            InitializeComponent();
        }
        public dateSelection(DateTime endDatum,DateTime startDate)
        {
            InitializeComponent();
                dateTimePicker1.Value = startDate;
                dateTimePicker2.Value = endDatum;
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                DateTime startDate = dateTimePicker1.Value;
                DateTime endDate = dateTimePicker2.Value;
                Print_List m_previewDlg = new Print_List(startDate, endDate);
                m_previewDlg.Title_Text = "Gesamte Summe pro Tag";
                m_previewDlg.Title_Text2 = "Datum :" + System.DateTime.Today.ToShortDateString() + " Zeit :" + System.DateTime.Now.ToShortTimeString();
                m_previewDlg.Title_Text3 = "Datum von " + startDate + " bis " + endDate;
                m_previewDlg.DocumentName = "List";
                m_previewDlg.Print();
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
