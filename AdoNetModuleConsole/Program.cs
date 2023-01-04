using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            var manager = new Manager();

            manager.Connect();

            manager.ShowData();

            Console.WriteLine("Введите логин для удаления:");

            var count = manager.DeleteUserByLogin(Console.ReadLine());


            Console.WriteLine("Количество удаленных строк " + count);

            manager.ShowData();

            manager.Disconnect();


            Console.ReadKey();
        }
    }
}