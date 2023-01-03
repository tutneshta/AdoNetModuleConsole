using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoNetLib.Configurations;
using Microsoft.Data.SqlClient;

namespace AdoNetLib
{
    public class MainConnector
    {
        public SqlConnection connection;

        public async Task<bool> ConnectAsync()
        {
            bool result;

            try
            {
                connection = new SqlConnection(ConnectionString.MsSqlConnection);
                await connection.OpenAsync();

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void DisconnectAsync()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                return connection;
            }
            else
            {
                throw new Exception("Подключение уже закрыто!");
            }
        }
    }
}