using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Linq;

namespace FileStoreToDbStore
{
    public class MySqlDbStore : IDbStore, IFiles
    {
        private MySqlCommand _sqlCommand;
        private MySqlConnection _mySqlConnection;
        private string[] _lines;
        public void ConnectToDatabase(string name)
        {
            try
            {
                var credentialsString = "SERVER = 127.0.0.1; PORT = 3306; USER Id = root; PASSWORD = root";
                _mySqlConnection = new MySqlConnection(credentialsString);
                _mySqlConnection.Open();
                string query = "CREATE DATABASE IF NOT EXISTS " + name;
                ExecuteSqlCommand(query);
                ExecuteSqlCommand("use " + name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void InsertData()
        {
            string[] keys = _lines[0].Split("|");
            var createQueryString = "create table if not exists hello(" + String.Join(" varchar(200),", keys) + " varchar(200) )";
            ExecuteSqlCommand(createQueryString);
            var lengthOfLines = _lines.Length;
            var valuesArray = new string[lengthOfLines];
            for (int index = 1; index < lengthOfLines; index++)
            {
                string[] values = _lines[index].Split("|");
                values = values.Select(x => x.Replace("\'", "")).ToArray();
                valuesArray[index - 1] = "(" + "'" + String.Join("\',\'", values) + "'" + ")";
            }
            var insertQueryString = " insert into hello ( " + String.Join(",", keys) + ") values  " +  String.Join(",", valuesArray);
            insertQueryString = insertQueryString.Remove(insertQueryString.Length - 1, 1);
            ExecuteSqlCommand(insertQueryString);
        }
        public void ExecuteSqlCommand(string command)
        {
            try
            {
                _sqlCommand = new MySqlCommand(command, _mySqlConnection);
                _sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void ReadFile(string path)
        {
            _lines = File.ReadAllLines(path);
        }
    }

}
