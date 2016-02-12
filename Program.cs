// заносим в бд файлы логина из txt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading;
//ехала
class Program
{
    static void Main(string[] args)
    {

        for (;;)
        {
            Thread.Sleep(10000);
            const string connectionString = "Server=localhost; Uid=root; Pwd=123;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("USE test_db;", connection);
            command.ExecuteNonQuery();
            command.CommandText = "DROP TABLE IF EXISTS `authorization`;";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE authorization (id INT(4) PRIMARY KEY AUTO_INCREMENT, value TEXT);";
            command.ExecuteNonQuery();
            string textFromFile = File.ReadAllText(@"C:\Login.txt", Encoding.Default);
            command.CommandText = "INSERT INTO authorization(`value`) VALUES (@param)";
            MySqlParameter param = new MySqlParameter("@param", MySqlDbType.Text);
            param.Value = textFromFile;
            command.Parameters.Add(param);
            command.ExecuteNonQuery();
            command.CommandText = "SELECT value FROM authorization WHERE id=1";
            string textFromDb = (string)command.ExecuteScalar();
            Console.WriteLine(textFromDb);
            connection.Close();
            Console.WriteLine("Готово");
            
        }
        
    }
}

//Hi, Big Max. It's my commit