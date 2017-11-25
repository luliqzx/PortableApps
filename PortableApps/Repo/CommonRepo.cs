using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public class CommonRepo
    {
        public string DbFile
        {
            get { return Environment.CurrentDirectory + "\\PortableAppDB.sqlite"; }
        }

        private SQLiteConnection MySQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public SQLiteConnection sqliteCon { get { return MySQLiteConnection(); } }

        private MySqlConnection MySQLConnectin()
        {
            MySqlConnection MySqlConnection = new MySqlConnection();
            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            VariableSetting VariableSetting = VariableSettingRepo.GetBy("Status");
            if (VariableSetting != null)
            {
                if (VariableSetting.Key == "Production")
                {
                    VariableSetting VariableSettingConStr = VariableSettingRepo.GetBy("MySQLConn");
                    MySqlConnection = new MySqlConnection("Server=128.199.195.92;Database=tsspk1511;Uid=oeuser3;Pwd=oe321;");
                }
                else
                {
                    MySqlConnection = new MySqlConnection("Server=127.0.0.1;Database=tsspk1511;Uid=root;Pwd=;");
                }
            }
            return MySqlConnection;
        }

        public MySqlConnection mysqlCon { get { return MySQLConnectin(); } }
    }
}
