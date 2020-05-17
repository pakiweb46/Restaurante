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

        public int getCount(string Table)
        {
            string sql = "SELECT count(*) FROM dbbari." + Table + ";";
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

        public MySqlDataReader getDataReader(string Table, string Parameter, string Value, string filter = "*")
        {
            string sql = "SELECT " + filter + " FROM dbbari." + Table + " Where " + Parameter + "='" + Value + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public MySqlDataReader getDataReader(string Table, string filter = "*")
        {
            string sql = "SELECT " + filter + " FROM dbbari." + Table + ";";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public MySqlDataReader getDataReaderJoin(string Table1, string Table2, string joinString, string filter = "*")
        {
            string sql = "SELECT " + filter + " FROM " + Table1 + "," + Table2 + " where " + joinString + ";";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public MySqlDataReader searchDaten(string Table, string parameter, string muster, string filter = "*")
        {
            string sql = "SELECT " + filter + " FROM dbbari." + Table + " WHERE " + parameter + " LIKE '" + muster + "';";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            return reader;
        }

        public int getMin(string Table, string parameter)
        {
            int min = -1;
            openReadConnection();
            string sql = "SELECT Min(" + parameter + ") FROM dbbari." + Table + ";";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                min = Convert.ToInt32(reader[0].ToString());
            }
            closeReadConnection();
            return min;
        }

        public int getMin(string Table, string MaxPara, string para, string val, string op = "=")
        {
            int min = -1;
            string singleQoute = "";
            if (op == "=")
                singleQoute = "'";
            openReadConnection();
            string sql = "SELECT Min(" + MaxPara + ") FROM dbbari." + Table + " WHERE " + para + op + singleQoute + val + singleQoute + ";";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                min = Convert.ToInt32(reader[0].ToString());
            }
            closeReadConnection();
            return min;
        }

        public int getMax(string Table, string parameter)
        {
            int min = -1;
            openReadConnection();
            string sql = "SELECT Max(" + parameter + ") FROM dbbari." + Table + ";";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                min = Convert.ToInt32(reader[0].ToString());
            }
            closeReadConnection();
            return min;
        }

        public int getMax(string Table, string MaxPara, string para, string val, string op = "=")
        {
            int min = -1;
            string singleQoute = "";
            if (op == "=")
                singleQoute = "'";
            openReadConnection();
            string sql = "SELECT Max(" + MaxPara + ") FROM dbbari." + Table + " WHERE " + para + op + singleQoute + val + singleQoute + ";";
            MySqlDataReader reader;
            command = new MySqlCommand(sql, readConnection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                min = Convert.ToInt32(reader[0].ToString());
            }
            closeReadConnection();
            return min;
        }

        public bool updateSingleData(string Table, string variable, string value, string parameter, string searchValue)
        {
            updateConnection.Open();
            command = new MySqlCommand();
            command.Connection = updateConnection;
            command.CommandText = "UPDATE dbbari." + Table + " SET " + variable + "='" + value + "' WHERE " + parameter + "= '" + searchValue + "'; ";
            command.ExecuteNonQuery();
            updateConnection.Close();
            return true;
        }

        public bool updateData(string Table, string[] variable, string[] value, string parameter, string searchValue)
        {
            updateConnection.Open();
            command = new MySqlCommand();
            command.Connection = updateConnection;
            string sql = "UPDATE dbbari." + Table + " SET ";
            for (int i = 0; i < variable.Length; i++)
            {
                sql += variable[i] + "='" + value[i] + "',";
            }
            sql = sql.Substring(0, sql.Length - 1) + " WHERE " + parameter + " = '" + searchValue + "'; ";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            updateConnection.Close();
            return true;
        }
    }
}