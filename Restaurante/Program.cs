using System;
using System.Windows.Forms;

namespace Restaurante
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmMain start_instance = new frmMain();
            //        datenTransfer start_instance = new datenTransfer();

            Application.Run(start_instance);
        }
    }

    public static class Globals
    {
        public const string SERVER = "localhost";
        public const string DBUSER = "root";
        public const string DBPASS = "mrklf598";
        public const string DBNAME = "dbbari";
        public const string DBPORT = "3306";

        public const string connString = "server=" + SERVER + ";user=" + DBUSER + ";database="
                                         + DBNAME + ";port=" + DBPORT + ";password=" + DBPASS + ";";

        public const string TITLE_NAME = "ZUR GRILLPFANNE";
        public const string LINE1_ADDRESS = "Grubenstr. 7 - 18055 Rostock ";
        public const string LINE2_TELE = " Tel. : 0381 21055633";
        public const string LINE3_TELE2 = "";
        public const string LINE4_OPENTIME = "";

        public const string TITLE_NAME_B = "ZUR GRILLPFANNE";
        public const string LINE1_ADDRESS_B = "Grubenstr. 7 - 18055 Rostock ";
        public const string LINE2_TELE_B = " Tel. : 0381 21055633";
        public const string LINE3_TELE2_B = "";
        public const string LINE4_OPENTIME_B = "";

        private enum KUNDENDATEN
        {//to delete
            idKundendaten,
            KundenNr,
            Anrede,
            KundenName,
            Strasse,
            StrNo,
            Zusatz,
            PLZ,
            Ort,
            Anfahrtkosten,
            Rabatt
        }
    }
}

// TODO
// Database Initiliazation routine if no database is there
// MWST AND RABATT CALCULATION
// Storno Rechnung beim stornieren eine Bestellung muss auch eine Rechnug erstellt werden.
// LICENSE STRATIGY
// Printklasse Zenteralizieren
// Data Klasse einbauen
// MENU MAKER also used as to add pfand to artikel as artikel is simple artikel e.g Mittagsmenu 