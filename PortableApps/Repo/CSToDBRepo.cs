using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface ICSToDBRepo : IBaseTRepo<CSToDB, string>
    {
    }
    public class CSToDBRepo : CommonRepo, ICSToDBRepo
    {
        public int Create(CSToDB ent)
        {
            int i = 0;
            string qry = @"INSERT INTO CSToDB (CSName	,ConnString) VALUES (@CSName, @ConnString) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(CSToDB ent)
        {
            int i = 0;
            string qry = @"UPDATE CSToDB SET CSName=@CSName,	ConnString=@ConnString WHERE CSName=@CSName";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Delete(string ID)
        {
            int i = 0;
            string qry = @"DELETE from CSToDB  WHERE CSName=@CSName";
            i = sqliteCon.Execute(qry, new { CSName = ID });
            return i;
        }

        public IList<CSToDB> GetAll()
        {
            string qry = @"SELECT * FROM CSToDB";
            IList<CSToDB> lstEnt = sqliteCon.Query<CSToDB>(qry, null).ToList();
            return lstEnt;
        }

        public CSToDB GetBy(string ID)
        {
            string qry = @"SELECT * FROM CSToDB WHERE CSName=@CSName";
            CSToDB ent = sqliteCon.Query<CSToDB>(qry, new { CSName = ID }).FirstOrDefault();
            return ent;
        }


        public IList<CSToDB> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, CSToDB whareClause = null)
        {
            int PageSize = page * rows;
            string qry = @"SELECT * FROM CSToDB LIMIT @PageSize ";
            string cntQry = @"SELECT COUNT(*) FROM CSToDB";
            rowCount = sqliteCon.Query<int>(cntQry, null).FirstOrDefault();
            IList<CSToDB> lstEnt = sqliteCon.Query<CSToDB>(qry, new { PageSize }).ToList();
            return lstEnt;
        }
    }
}
