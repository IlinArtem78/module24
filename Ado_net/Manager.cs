using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ado_net
{
    public class Manager
    {
        private MainConnector connector;
        private DbExecutor db;
        private Table userTable;
        public Manager()
        {
            connector = new MainConnector();
            Console.OutputEncoding = Encoding.Unicode;

            userTable = new Table();
            userTable.Name = "NetworkUser";
            userTable.ImportantField = "Login";
            userTable.Fields.Add("Id");
            userTable.Fields.Add("Login");
            userTable.Fields.Add("Name");

        }

        public void Connect()
        {
            var result = connector.ConnectAsync();

            var data = new DataTable();

            if (result.Result)
            {
                Console.WriteLine("Подключено успешно!");

                db = new DbExecutor(connector);
            }
            else
            {
                Console.WriteLine("Ошибка подключения"); 
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Отключаем БД!");
            connector.DisconnectAsync();
        }

        public void ShowData()
        {
            var tablename = "NetworkUser";
            Console.WriteLine("Ввыберите тип чтения для данных 1 - отсоединенная модель, 2 - присоединенная модель");
            byte numData = byte.Parse(Console.ReadLine());

            var data = new DataTable();
            //инстукция по выбору типа модели 
            switch (numData)
            {
                case 1:
                    Console.WriteLine("Получаем данные таблицы " + userTable.Name);
                    data = db.SelectAll(userTable.Name);
                    Console.WriteLine("Количество строк в " + userTable.Name + ": " + data.Rows.Count);


                    foreach (DataColumn column in data.Columns)
                    {
                        Console.Write($"{column.ColumnName}\t");
                    }
                    Console.WriteLine();
                    foreach (DataRow row in data.Rows)
                    {

                        var cells = row.ItemArray;
                        foreach (var cell in cells)
                        {
                            Console.Write($"{cell}\t");
                        }
                        Console.WriteLine();
                    }
                    Disconnect();
                    

                    break;
                case 2:
                    var reader = db.SelectAllCommandReader(userTable.Name);
                    Console.WriteLine("Получаем данные таблицы " + userTable.Name);

                    Console.WriteLine("Количество столбцов " + userTable.Name + ": " + reader.FieldCount);


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

                    while (reader.Read())
                    {
                        for (int i = 0; i < columnList.Count; i++)
                        {
                            var value = reader[columnList[i]];
                            Console.Write($"{value}\t");
                        }

                        Console.WriteLine();
                    }

                    break;


            }

        }
        //удаление пользователья по логину
        public int DeleteUserByLogin(string value)
        {
            return db.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
        }
        //добавления пользователя
        public void AddUser(string name, string login)
        {
            db.ExecProcedureAdding(name, login);
        }

        //
        public int UpdateUserByLogin(string value, string newvalue)
        {
            return db.UpdateByColumn(userTable.Name, userTable.ImportantField, value, userTable.Fields[2], newvalue);
        }


    }
}
