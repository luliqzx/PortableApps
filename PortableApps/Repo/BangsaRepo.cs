using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IBangsaRepo : IBaseTRepo<TBangsa, string>
    {
        void SyncBangsaFromAppInfoMySQL();
    }
    public class BangsaRepo : DefaultRepo<TBangsa, string>, IBangsaRepo
    {
        public override int Create(TBangsa ent)
        {
            throw new NotImplementedException();
        }

        public override int Edit(TBangsa ent)
        {
            throw new NotImplementedException();
        }

        public override int Delete(string ID)
        {
            throw new NotImplementedException();
        }

        public override  IList<TBangsa> GetAll()
        {
            string qry = @"SELECT * FROM TBangsa";
            IList<TBangsa> lstBangsa = sqliteCon.Query<TBangsa>(qry, null).ToList();
            return lstBangsa;
        }

        public override TBangsa GetBy(string ID)
        {
            throw new NotImplementedException();
        }

        public override IList<TBangsa> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, TBangsa oWhereClause)
        {
            throw new NotImplementedException();
        }

        public void SyncBangsaFromAppInfoMySQL()
        {
            int i = 0;
            string qry = @"SELECT DISTINCT(BANGSA) BANGSA FROM APPINFO WHERE IFNULL(BANGSA,'') != ''";
            IList<TBangsa> lstBangsaMySQL = mysqlCon.Query<TBangsa>(qry, null).ToList();

            string qry1 = @"DELETE FROM TBANGSA";
            i = i + sqliteCon.Execute(qry1, null);

            string qry2 = @"INSERT INTO TBANGSA (BANGSA) VALUES (@Bangsa)";
            i = i + sqliteCon.Execute(qry2, lstBangsaMySQL);
        }
    }
}
