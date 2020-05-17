using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Restaurante
{
    internal class RestauranteData
    {
        private MySqlCommand command;
        private MySqlConnection readConnection = new MySqlConnection(Globals.connString);
        private MySqlConnection updateConnection = new MySqlConnection(Globals.connString);

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

        public MySqlDataReader getAllData(string Table)
        {
            string sqlReadCard = "SELECT * From dbbari." + Table + ";";
            MySqlDataReader readerSpeiseKarte;
            command = new MySqlCommand(sqlReadCard, readConnection);
            readerSpeiseKarte = command.ExecuteReader();
            return readerSpeiseKarte;
        }

        public int getCount(string Table, string parameter, string value)
        {
            string sql = "SELECT count(*) FROM dbbari." + Table + " Where " + parameter + "='" + value + "';";
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

        public MySqlDataReader getDataReader(string Table, string Parameter, string Value)
        {
            string sql = "SELECT * FROM dbbari." + Table + " Where " + Parameter + "='" + Value + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public ConnectionState getReadConnectionState()
        {
            return readConnection.State;
        }

        public ConnectionState getUpdateConnectionState()
        {
            return updateConnection.State;
        }

        public void openAllConnection()
        {
            readConnection.Open();
            updateConnection.Open();
        }

        public void openReadConnection()
        {
            readConnection.Open();
        }

        public void closeReadConnection()
        {
            readConnection.Close();
        }

        public MySqlDataReader searchDaten(string Table, string parameter, string muster)
        {
            string sql = "SELECT * FROM dbbari." + Table + " WHERE " + parameter + " LIKE '" + muster + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public bool updateSingleData(string Table, string variable, string value, string parameter, string searchValue)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari." + Table + " SET " + variable + "=" + value + " WHERE " + parameter + "= " + searchValue + "; ";
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateAnfahrtKosten(string KundenID, Double Anfahrt)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET Anfahrtkosten=?Anfahrtkosten WHERE idKundendaten=" + KundenID + ";";
            command.Prepare();
            command.Parameters.Add("Anfahrtkosten", MySqlDbType.Double).Value = Anfahrt;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateKundenName(string KundenID, string KundenName)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET KundenName=?KundenName WHERE idKundendaten=" + KundenID + ";";
            command.Prepare();
            command.Parameters.Add("KundenName", MySqlDbType.VarChar).Value = KundenName;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateKundenNummer(string KundenID, string KundenNr)
        {
            updateConnection.Open();
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET KundenNr=?KundenNr WHERE idKundendaten=" + KundenID + ";"; ;
            command.Prepare();
            command.Parameters.Add("KundenNr", MySqlDbType.VarChar).Value = KundenNr;
            command.ExecuteNonQuery();
            updateConnection.Close();
            return true;
        }

        public bool updateOrt(string KundenID, string Ort)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET Ort=?Ort WHERE idKundendaten=" + KundenID + ";";
            command.Prepare();
            command.Parameters.Add("Ort", MySqlDbType.VarChar).Value = Ort;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updatePLZ(string KundenID, long PLZ)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET PLZ=?PLZ WHERE idKundendaten=" + KundenID + ";"; ;
            command.Prepare();
            command.Parameters.Add("PLZ", MySqlDbType.Int64).Value = PLZ;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateRabatt(string KundenID, Double rabatt)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET rabatt=?rabatt WHERE idKundendaten=" + KundenID + ";"; ;
            command.Prepare();
            command.Parameters.Add("rabatt", MySqlDbType.Double).Value = rabatt;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateSNo(string KundenID, string SNO)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET StrNo=?StrNo WHERE idKundendaten=" + KundenID + ";"; ;
            command.Prepare();
            command.Parameters.Add("StrNo", MySqlDbType.VarChar).Value = SNO;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateStrasse(string KundenID, string Strasse)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET Strasse=?Strasse WHERE idKundendaten=" + KundenID + ";"; ;
            command.Prepare();
            command.Parameters.Add("Strasse", MySqlDbType.VarChar).Value = Strasse;
            command.ExecuteNonQuery();
            return true;
        }

        public bool updateZusatz(string KundenID, string zusatz)
        {
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.Parameters.Clear();
            command.CommandText = "UPDATE dbbari.kundendaten SET zusatz=?zusatz WHERE idKundendaten=" + KundenID + ";"; ;
            command.Prepare();
            command.Parameters.Add("zusatz", MySqlDbType.VarChar).Value = zusatz;
            command.ExecuteNonQuery();
            return true;
        }
    }
}