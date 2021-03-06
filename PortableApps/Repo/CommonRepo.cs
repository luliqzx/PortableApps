﻿using MySql.Data.MySqlClient;
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
        #region SQLite
        public string DbFile
        {
            get { return Environment.CurrentDirectory + "\\PortableAppDB.sqlite"; }
        }

        private static IDbConnection _sqliteCon { get; set; }

        private IDbConnection MySQLiteConnection()
        {
            try
            {
                if (_sqliteCon == null)
                {
                    _sqliteCon = new SQLiteConnection("Data Source=" + DbFile);
                }
            }
            catch (Exception)
            {
                _sqliteCon = null;
            }

            return _sqliteCon;
        }

        public IDbConnection sqliteCon { get { return MySQLiteConnection(); } }

        #endregion

        #region MySQL
        private IDbConnection MySQLConnectin()
        {
            try
            {
                if (_mysqlCon == null)
                {
                    IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
                    VariableSetting VariableSetting = VariableSettingRepo.GetBy("Status");
                    if (VariableSetting != null)
                    {
                        if (!string.IsNullOrEmpty(VariableSetting.Value) && VariableSetting.Value.ToUpper() == "PRODUCTION")
                        {
                            VariableSetting VariableSettingConStr = VariableSettingRepo.GetBy("MySQLConn");
                            _mysqlCon = new MySqlConnection(WFUtils.Decrypt(VariableSettingConStr.Value));
                        }
                        else
                        {
                            _mysqlCon = new MySqlConnection("Server=127.0.0.1;Database=tsspk1511;Uid=root;Pwd=;");
                        }
                    }
                    else
                    {
                        _mysqlCon = new MySqlConnection("Server=127.0.0.1;Database=tsspk1511;Uid=root;Pwd=;");
                    }
                }
                if (_mysqlCon.State != ConnectionState.Open)
                {
                    _mysqlCon.Open();
                }
            }
            catch (Exception)
            {
                _mysqlCon = null;
            }

            return _mysqlCon;
        }

        private static IDbConnection _mysqlCon { get; set; }

        public IDbConnection mysqlCon { get { return MySQLConnectin(); } }

        public void ResetMySQLConnection()
        {
            _mysqlCon = null;
        }
        #endregion
    }
}
