using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NasiaAutoTestsApp
{
    public class DataBase
    {
        private string _database;
        private string _localhost;
        private string _userId;
        private string _password;

        public string request;
        public bool available;
        public string code;

        public List<string> ids;

        public MySqlConnection _connection;

        private MySqlDataReader _reader;


        public DataBase(string database, string localhost, string userId, string password)
        {
            _database = database;
            _localhost = localhost;
            _userId = userId;
            _password = password;
        }

        //добавить соединение MySQL
        public void AddMySQLConnection()
        {
            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();
            connBuilder.Add("Server", _database);
            connBuilder.Add("DataBase", _localhost);
            connBuilder.Add("Uid", _userId);
            connBuilder.Add("pwd", _password);

            _connection = new MySqlConnection(connBuilder.ConnectionString);
        }

        //отправляем команду в БД
        public void GetComand()
        {
            MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = request;
            cmd.CommandType = CommandType.Text;
            _reader = cmd.ExecuteReader();
        }

        public void DeleteUser(string number)
        {
            AddMySQLConnection();
            //OpenConnection();
            MySqlCommand command = new MySqlCommand("delete_user_by_phone;", _connection);
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add(new MySqlParameter("out_var","" ));
            command.Parameters.Add(new MySqlParameter("out_var", MySqlDbType.VarChar)).Direction =
                ParameterDirection.Output;
            command.Parameters.Add(new MySqlParameter("user_phone_number", $"998{number}"));
            _connection.Open();
            command.ExecuteScalar();
        }

        //из строки бд получаем отп-код
        public void StringBDInCode()
        {
            while (_reader.Read())
            {
                code = _reader.GetString(0);
            }
            _reader.Close();
        }

        private void BuyerAvailability()
        {
            while (_reader.Read())
            {
                if (_reader.IsDBNull(0))
                {
                    available = false;
                    return;
                }
                available = true;
            }
            
        }
        
        //открываем соединение
        public void OpenConnection()
        {
            AddMySQLConnection();
            _connection.Open();
        }
    
        //закрываем соединение
        public void CloseConection()
        {
            _connection.Close();
        }

        //получить ОТП-код
        public void GetOTP()
        {
            OpenConnection();
            GetComand();
            StringBDInCode();
            CloseConection();
        }

        public void GetIds()
        {
            ids = new List<string>();
            while (_reader.Read())
            {
                code = _reader.GetString(0);
                ids.Add(code);
            }
            _reader.Close();
            ids.Reverse();
        }
        public void SetStatusBuyer(string buyer, int status)
        {
            OpenConnection();
            request = $"update users set status = {status} where phone = '998{buyer}';";
            GetComand();
            CloseConection();
        }

        public void SetLimitBuyer(string buyer, int limit)
        {
            OpenConnection();
            request = $"UPDATE buyer_settings SET `balance` = {limit} WHERE user_id = (SELECT id FROM users WHERE phone = '998{buyer}');";
            GetComand();
            CloseConection();
        }

        public void CheckStatusBuyer(string buyer)
        {
            OpenConnection();
            request = $"select id from users WHERE phone = '998{buyer}';";
            GetComand();
            BuyerAvailability();
            CloseConection();
        }

        public void GetContractId(string buyer)
        {
            OpenConnection();
            request = $"SELECT id FROM contracts WHERE user_id =(SELECT id FROM users WHERE phone = '998{buyer}');";
            GetComand();
            GetOTP();
            CloseConection();
        }

        public void GetContractsId(string buyer)
        {
            OpenConnection();
            request = $"SELECT id FROM contracts WHERE user_id =(SELECT id FROM users WHERE phone = '998{buyer}');";
            GetComand();
            GetIds();
            CloseConection();
        }
        public void GetContractsIdByStatus(string buyer,int status)
        {
            OpenConnection();
            request = $"SELECT id FROM contracts WHERE user_id = (SELECT id FROM users WHERE phone = '998{buyer}') and `status`={status};";
            GetComand();
            GetIds();
            CloseConection();
        }
        
                
    }
}