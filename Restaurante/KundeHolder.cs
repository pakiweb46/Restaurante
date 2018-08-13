using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using LieferDienst;
namespace Restaurante
{
    class KundeHolder
    {
        #region  Property Variables
        /// <summary>
        /// Property variable kundendaten
        /// </summary>
        /// <remarks></remarks>
        private string KundenName;
        private string KundenOrt;
        private string KundenPLZ;
        private string KundenAddresse;
        private string KundenTelefone;
        private string KundenHinweis;
        private double AnfahrtKosten;
        private double MwstAnfahrt;
        private double Gesamt;
        private double Rabatt;
        #endregion
        #region Class Variable
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlCommand cmd;
        MySqlDataReader rdr;
        #endregion
        #region class Properties
        public string sKundenName
        {
            get { return KundenName; }
            set { KundenName = value; }
        }
        public string sKundenOrt
        {
            get { return KundenOrt; }
            set { KundenOrt = value; }
        }
        public string sKundenPLZ
        {
            get { return KundenPLZ; }
            set { KundenPLZ = value; }
        }
        public string sKundenAddresse
        {
            get { return KundenAddresse; }
            set { KundenAddresse = value; }
        }
        public string sKundenTelefone
        {
            get { return KundenTelefone; }
            set { KundenTelefone = value; }
        }
        public string sKundenHinweis
        {
            get { return sKundenHinweis; }
            set { sKundenHinweis = value; }
        }
        public double sAnfahrtKosten
        {
            get { return AnfahrtKosten; }
            set { AnfahrtKosten = value; }
        }
        public double sMwstAnfahrt
        {
            get { return MwstAnfahrt; }
            set { MwstAnfahrt = value; }
        }
        public double sGesamt
        {
            get { return Gesamt; }
            set { Gesamt = value; }
        }
        public double sRabatt
        {
            get { return Rabatt; }
            set { Rabatt = value; }
        }
        #endregion
        #region Class Constructor
        public KundeHolder()
        {
            KundenName=string.Empty;
            KundenOrt=string.Empty;
            KundenPLZ=string.Empty;
            KundenAddresse=string.Empty;
            KundenTelefone=string.Empty;
            KundenHinweis=string.Empty;
            AnfahrtKosten=0;
            MwstAnfahrt=0;
            Gesamt=0;
            Rabatt=0;
        }

        #endregion

        #region functions
        /// <summary>
        /// Functions for Kundendaten
        /// </summary>
        static string connStr = Class1.connString;
        private void loadKundendaten(int kundenid)
        {
            

            if (conn.State.ToString() == "Closed")
                conn.Open();
            string sql = "SELECT * FROM dbbari.kundendaten Where idKundendaten=" + kundenid + ";";
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
                KundenName = rdr["KundenName"].ToString();
                KundenOrt = rdr["ort"].ToString();
                KundenPLZ = rdr["PLZ"].ToString();
                KundenAddresse = rdr["strasse"].ToString() + " ." + rdr["StrNo"].ToString();
                KundenTelefone = rdr["kundennr"].ToString();
                KundenHinweis = rdr["zusatz"].ToString();
                AnfahrtKosten = Convert.ToDouble(rdr["AnfahrtKosten"].ToString());
                MwstAnfahrt = AnfahrtKosten * 0.19;
                // Add anfahrt kosten in total
                Gesamt = AnfahrtKosten;
                // Mwst of Anfahrt in Mwst Total
                Rabatt = Convert.ToDouble(rdr["Rabatt"].ToString());
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

        #endregion
    }
}