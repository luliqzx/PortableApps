using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;
using PortableApps.Common;
using System.Data;

namespace PortableApps.Repo
{
    public class CommonRepo
    {
        public string DbFile
        {
            get { return Environment.CurrentDirectory + "\\PortableAppDB.sqlite"; }
        }

        private IDbConnection MySQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public IDbConnection sqliteCon { get { return MySQLiteConnection(); } }

        private IDbConnection MySQLConnectin()
        {
            IDbConnection MySqlConnection = new MySqlConnection();
            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            VariableSetting VariableSetting = VariableSettingRepo.GetBy("Status");
            if (VariableSetting != null)
            {
                if (VariableSetting.Value == "Production")
                {
                    VariableSetting VariableSettingConStr = VariableSettingRepo.GetBy("MySQLConn");
                    //MySqlConnection = new MySqlConnection("Server=128.199.195.92;Database=tsspk1511;Uid=oeuser3;Pwd=oe321;");
                    MySqlConnection = new MySqlConnection(WFUtils.Decrypt(VariableSettingConStr.Value));
                }
                else
                {
                    MySqlConnection = new MySqlConnection("Server=127.0.0.1;Database=tsspk1511;Uid=root;Pwd=;");
                }
            }
            else
            {
                MySqlConnection = new MySqlConnection("Server=127.0.0.1;Database=tsspk1511;Uid=root;Pwd=;");
            }
            return MySqlConnection;
        }

        public IDbConnection mysqlCon { get { return MySQLConnectin(); } }
    }
}
