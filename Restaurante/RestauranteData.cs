using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace Restaurante
{
    class RestauranteData
    {


        private MySqlConnection readConnection = new MySqlConnection(Globals.connString);
        private MySqlConnection updateConnection = new MySqlConnection(Globals.connString);
        private MySqlCommand command;

        public RestauranteData()
        {

        }
        ~RestauranteData()
        {
            readConnection.Close();
            updateConnection.Close();
        }
        public void closeAllConnection()
        {
            readConnection.Close();
            updateConnection.Close();
        }
        public void openReadConnection()
        {
            readConnection.Open();
        }
        public void closeReadConnection()
        {
            readConnection.Close();
        }
        public void openAllConnection()
        {
            readConnection.Open();
            updateConnection.Open();
        }
        public ConnectionState getReadConnectionState()
        {
            return readConnection.State;
        }

        public ConnectionState getUpdateConnectionState()
        {
            return updateConnection.State;
        }

        /**
         * Deliver all Order Card (Speisekarte) Data Reader
         * 
         * @return MySqlDataReader All data of Speisekarte Table
         */
        public MySqlDataReader getAllCardData()
        {
            string sqlReadCard = "SELECT * From dbbari.Speisekarte ;";
            MySqlDataReader readerSpeiseKarte;
            command = new MySqlCommand(sqlReadCard, readConnection);
            readerSpeiseKarte = command.ExecuteReader();
            return readerSpeiseKarte;
        }
        public MySqlDataReader getAllZutatenData()
        {
            string sqlReadZutaten = "SELECT * From dbbari.zutaten";
            MySqlDataReader readerZutaten;
            command = new MySqlCommand(sqlReadZutaten, readConnection);
            readerZutaten = command.ExecuteReader();
            return readerZutaten;
        }

        public MySqlDataReader getAllStadtPlan()
        {
            string sql = "SELECT * FROM dbbari.stadtplan;";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public MySqlDataReader getKundenDaten(string parameter, string value)
        {
            string sql = "SELECT * FROM dbbari.kundendaten WHERE "+parameter+" = '" + value + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public MySqlDataReader searchKundenDaten(string parameter, string muster)
        {
            string sql = "SELECT * FROM dbbari.kundendaten WHERE "+parameter+" LIKE '" + muster + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public int getKundenCount(string parameter, string value)
        {
            string sql = "SELECT count(*) FROM dbbari.kundendaten Where "+parameter+"='" + value + "';";
            int count = 0;
            openReadConnection();
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                count = Convert.ToInt16(reader[0].ToString());
            }
            closeReadConnection();
            return count;
        }

        public MySqlDataReader getStadtPlan(string parameter, string value)
        {
            string sql = "SELECT * FROM dbbari.stadtplan Where "+parameter+"='" + value + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public bool updateAnfahrtKosten(string KundenNummer, Double Anfahrt)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET Anfahrtkosten=?Anfahrtkosten WHERE idKundendaten=" + KundenNummer + ";";
            command.Prepare();
            command.Parameters.Add("Anfahrtkosten", MySqlDbType.Double).Value = Anfahrt;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateKundenName(string KundenNummer, string KundenName)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET KundenName=?KundenName WHERE idKundendaten=" + KundenNummer + ";"; 
            command.Prepare();
            command.Parameters.Add("KundenName", MySqlDbType.VarChar).Value = KundenName;
            command.ExecuteNonQuery();
            return true;
        }
        public bool updateOrt(string KundenNummer, string Ort)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET Ort=?Ort WHERE idKundendaten=" + KundenNummer + ";";
            command.Prepare();
            command.Parameters.Add("Ort", MySqlDbType.VarChar).Value = Ort;
            command.ExecuteNonQuery();
            return true;
        }
        public bool updatePLZ(string KundenNummer, long PLZ)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET PLZ=?PLZ WHERE idKundendaten=" + KundenNummer + ";"; ;
            command.Prepare();
            command.Parameters.Add("PLZ", MySqlDbType.Int64).Value = PLZ;
            command.ExecuteNonQuery();
            return true;
        }
        public bool updateStrasse(string KundenNummer, string Strasse)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET Strasse=?Strasse WHERE idKundendaten=" + KundenNummer + ";"; ;
            command.Prepare();
            command.Parameters.Add("Strasse", MySqlDbType.VarChar).Value = Strasse;
            command.ExecuteNonQuery();
            return true;
        }
        public bool updateSNo(string KundenNummer, string SNO)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET StrNo=?StrNo WHERE idKundendaten=" + KundenNummer + ";"; ;
            command.Prepare();
            command.Parameters.Add("StrNo", MySqlDbType.VarChar).Value = SNO;
            command.ExecuteNonQuery();
            return true;
        }
        public bool updateKundenNummer(string KundenNummer, string KundenNr)
        {
            updateConnection.Open();
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET KundenNr=?KundenNr WHERE idKundendaten=" + KundenNummer + ";"; ;
            command.Prepare();
            command.Parameters.Add("KundenNr", MySqlDbType.VarChar).Value = KundenNr;
            command.ExecuteNonQuery();
            updateConnection.Close();
            return true;
        }
        public bool updateZusatz(string KundenNummer, string zusatz)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET zusatz=?zusatz WHERE idKundendaten=" + KundenNummer + ";"; ;
            command.Prepare();
            command.Parameters.Add("zusatz", MySqlDbType.VarChar).Value = zusatz;
            command.ExecuteNonQuery();
            return true;
        }
        public bool updateRabatt(string KundenNummer, Double rabatt)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET rabatt=?rabatt WHERE idKundendaten=" + KundenNummer + ";"; ;
            command.Prepare();
            command.Parameters.Add("rabatt", MySqlDbType.Double).Value = rabatt;
            command.ExecuteNonQuery();
            return true;
        }
    }
}