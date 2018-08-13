using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;
namespace Restaurante
{
    public partial class frmMain : Form
    {
           public double Anfahrtfrei=20;
    
        public frmMain()
        {
            InitializeComponent();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnAuftragErfassen_Click(object sender, EventArgs e)
        {
            this.Hide();
            NeuAuftrag MyAuftrag = new NeuAuftrag();// AuftragErfassen MyAuftrag = new AuftragErfassen();
            MyAuftrag.ShowDialog();
            MyAuftrag.Close();
            this.Show();
            

        }
        private void frmMain_FormClosing(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown+=new KeyEventHandler(frmMain_KeyDown);
            // Go direct to Auftragerfassen
            btnAuftragErfassen.PerformClick();
        }
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnKundenDaten.PerformClick();

            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSpeiseKarte.PerformClick();

            }
            else if (e.KeyCode == Keys.F3)
            {
                btnAuftragErfassen.PerformClick(); 

            }
            else if (e.KeyCode == Keys.F4)
            {
                btnTagesabrechnung.PerformClick();

            }
            else if (e.KeyCode == Keys.F5)
            {
                btnMitarbeiter.PerformClick();

            }
            else if (e.KeyCode == Keys.F6)
            {
                btnSontiges.PerformClick();

            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnBeenden.PerformClick();

            }
            
        }
        private void btnKundenDaten_Click(object sender, EventArgs e)
        {

            this.Hide();
            frmKundenDaten frmKundenDaten_instance = new frmKundenDaten();
            frmKundenDaten_instance.ShowDialog();
            frmKundenDaten_instance.Close();
                this.Show();
            
        }

        private void btnSpeiseKarte_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSpeiseKarte frmSpeiseKarte_instance = new frmSpeiseKarte();
            frmSpeiseKarte_instance.ShowDialog();
            frmSpeiseKarte_instance.Close();
            this.Show();
        }

        private void btnTagesabrechnung_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAbrechnung myAbrechnung = new frmAbrechnung();
            myAbrechnung.ShowDialog();
            myAbrechnung.Close();
            this.Show();
        }

        private void btnMitarbeiter_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMitarbeiter myMitarbeiter = new frmMitarbeiter();
            myMitarbeiter.ShowDialog();
            myMitarbeiter.Close();
            this.Show();

        }

        private void btnSontiges_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmShowList myReservierung = new frmShowList(1,"Artikel Liste");
            myReservierung.ShowDialog();
            myReservierung.Close();
            this.Show();
           
        }
        public void DatabaseBackup(string ExeLocation, string DBName,string Path)
        {
            try
            {
                string tmestr = "";
                tmestr = DBName + "-" + DateTime.Now.ToShortDateString() + ".sql";
                tmestr = tmestr.Replace("/", "-");
                tmestr = Path +"\\" + tmestr;
                try
                {
                    StreamWriter file = new StreamWriter(tmestr);
                    ProcessStartInfo proc = new ProcessStartInfo();
                    string cmd = string.Format(@"-u{0} -p{1} -h{2} {3}", "root", "mrklf598", "localhost", DBName);
                    proc.FileName = ExeLocation;
                    proc.RedirectStandardInput = false;
                    proc.RedirectStandardOutput = true;
                    proc.Arguments = cmd;
                    proc.UseShellExecute = false;
                    Process p = Process.Start(proc);
                    string res;
                    res = p.StandardOutput.ReadToEnd();
                    file.WriteLine(res);

                    p.WaitForExit();
                    file.Close();
                    MessageBox.Show("Backup Completed");
                }
                catch (Exception ex)
                {
                  
                    MessageBox.Show(ex.Message);
                }
               
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Tag = "Hello";
            folderBrowserDialog1.Description = "Bitte wählen sie platz für datensicherung?";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;


                DatabaseBackup("C:/Program Files/MySQL/MySQL Server 5.5/bin/mysqldump.exe", "dbbari", folderPath);
            }
        }

      
    }
}
