using MySql.Data.MySqlClient;
using System;

//TODO Momentan keine Nutzung
namespace Restaurante
{
    internal class KundeHolder
    {
        #region Property Variables

        private double AnfahrtKosten;

        private double Gesamt;

        private string KundenAddresse;

        private string KundenHinweis;

        /// <summary>
        /// Property variable kundendaten
        /// </summary>
        /// <remarks></remarks>
        private string KundenName;

        private string KundenOrt;
        private string KundenPLZ;
        private string KundenTelefone;
        private double MwstAnfahrt;
        private double Rabatt;

        #endregion Property Variables

        #region Class Variable

        private MySqlConnection conn = new MySqlConnection(connStr);
        private RestauranteData rData;

        #endregion Class Variable

        #region class Properties

        public double sAnfahrtKosten
        {
            get { return AnfahrtKosten; }
            set { AnfahrtKosten = value; }
        }

        public double sGesamt
        {
            get { return Gesamt; }
            set { Gesamt = value; }
        }

        public string sKundenAddresse
        {
            get { return KundenAddresse; }
            set { KundenAddresse = value; }
        }

        public string sKundenHinweis
        {
            get { return sKundenHinweis; }
            set { sKundenHinweis = value; }
        }

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

        public string sKundenTelefone
        {
            get { return KundenTelefone; }
            set { KundenTelefone = value; }
        }

        public double sMwstAnfahrt
        {
            get { return MwstAnfahrt; }
            set { MwstAnfahrt = value; }
        }

        public double sRabatt
        {
            get { return Rabatt; }
            set { Rabatt = value; }
        }

        #endregion class Properties

        #region Class Constructor

        public KundeHolder()
        {
            rData = new RestauranteData();
            KundenName = string.Empty;
            KundenOrt = string.Empty;
            KundenPLZ = string.Empty;
            KundenAddresse = string.Empty;
            KundenTelefone = string.Empty;
            KundenHinweis = string.Empty;
            AnfahrtKosten = 0;
            MwstAnfahrt = 0;
            Gesamt = 0;
            Rabatt = 0;
        }

        #endregion Class Constructor

        #region functions

        /// <summary>
        /// Functions for Kundendaten
        /// </summary>
        private static string connStr = Globals.connString;

        private void loadKundendaten(int kundenid)
        {
            rData.openReadConnection();
            MySqlDataReader reader = rData.getDataReader("kundendaten", "idKundendaten", kundenid.ToString());

            if (reader.Read())
            {
                KundenName = reader["KundenName"].ToString();
                KundenOrt = reader["ort"].ToString();
                KundenPLZ = reader["PLZ"].ToString();
                KundenAddresse = reader["strasse"].ToString() + " ." + reader["StrNo"].ToString();
                KundenTelefone = reader["kundennr"].ToString();
                KundenHinweis = reader["zusatz"].ToString();
                AnfahrtKosten = Convert.ToDouble(reader["AnfahrtKosten"].ToString());
                MwstAnfahrt = AnfahrtKosten * 0.19;
                // Add anfahrt kosten in total
                Gesamt = AnfahrtKosten;
                // Mwst of Anfahrt in Mwst Total
                Rabatt = Convert.ToDouble(reader["Rabatt"].ToString());
            }
            reader.Close();
            rData.closeReadConnection();
        }

        #endregion functions
    }
}