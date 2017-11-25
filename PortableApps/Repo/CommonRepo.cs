using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PortableApps.Repo
{
    public class CommonRepo
    {
        public string DbFile
        {
            get { return Environment.CurrentDirectory + "\\PortableAppDB.sqlite"; }
        }

        public SQLiteConnection MySQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public SQLiteConnection sqliteCon { get { return MySQLiteConnection(); } }
    }
}
