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
    public partial class FahrerAbrechnung : Form
    {
        private double gesamt;
        private PrintableListView2.ListViewItemCollection itemscol;
        public FahrerAbrechnung()
        {
            InitializeComponent();
        }
        public FahrerAbrechnung(PrintableListView2.ListViewItemCollection listview1, double gesamt)
        {
            InitializeComponent();
            this.itemscol = listview1;
            this.gesamt = gesamt;
        }

        private void FahrerAbrechnung_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.Value = System.DateTime.Now;
            textBox1.Text = "5,50";
            textBox2.Text = itemscol.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Übersicht Druck Neue Klasse 
            AbrechnungDruck obj = new AbrechnungDruck();
            obj.AnzahlBestellungen_Text = itemscol.Count;
            obj.Umsatz_Text = gesamt;
            obj.Title_Text = System.DateTime.Now.ToShortDateString()+" "+System.DateTime.Now.ToShortTimeString();
            obj.TitleHead_Text = "Fahreh Abrechnung";
            obj.AnzahlStunden_Text = Convert.ToInt32(dateTimePicker2.Value.Hour-dateTimePicker1.Value.Hour);
            obj.Stundenlohn_Text = Convert.ToDouble(textBox1.Text);
            obj.PrintDocument();
        }


    }
}
