using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoNetLib;
using Microsoft.Data.SqlClient;

namespace AdoNetModuleConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connector = new MainConnector();


            var result = connector.ConnectAsync();

            if (result.Result)
            {
                Console.WriteLine("Подключено успешно!");

                //var db = new DbExecutor(connector);

                var tablename = "NetworkUser";

                Console.WriteLine("Получаем данные таблицы " + tablename);

                //var data = new DataTable();
                //data = db.SelectAll(tablename);

                using (var reader = SelectAllCommandReader(tablename))
                {
                    WievData2(reader);
                }


                Console.WriteLine("Отключаем БД!");

                //connector.DisconnectAsync();


                //WievDate(tablename, data);
            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }


            SqlDataReader SelectAllCommandReader(string table)
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select * from " + table,
                    Connection = connector.GetConnection(),
                };

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return reader;
                }

                return null;
            }


            Console.ReadKey();
        }

        private static void WievData2(SqlDataReader reader)
        {
            if (reader.HasRows) // если есть данные
            {
                var columnList = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    columnList.Add(name);
                }

                for (int i = 0; i < columnList.Count; i++)
                {
                    Console.Write($"{columnList[i]}\t");
                }

                Console.WriteLine();

                while (reader.Read()) // построчно считываем данные
                {
                    var id = reader["id"];
                    var name = reader.GetValue(1);
                    var age = reader.GetValue(2);

                    Console.WriteLine($"{id} \t {name} \t {age}");
                }
            }
        }

        private static void WievDate(string tablename, DataTable data)
        {
            Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);

            foreach (DataColumn dataColumn in data.Columns)
            {
                Console.Write($"{dataColumn.ColumnName}\t\t");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in data.Rows)
            {
                var cell = dataRow.ItemArray;

                foreach (var VARIABLE in cell)
                {
                    Console.Write($"{VARIABLE}\t\t");
                }

                Console.WriteLine();
            }
        }
    }
}