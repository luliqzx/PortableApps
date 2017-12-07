using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IDaerahRepo : IBaseTRepo<tdaerah, int>
    {
        IList<tdaerah> GetDaerahBy(string negeri);
        tdaerah GetBy(string kod_daerah);
    }
    public class DaerahRepo : CommonRepo, IDaerahRepo
    {
        public int Create(tdaerah ent)
        {
            int i = 0;
            string qry = @"INSERT INTO daerah (id, kod_negeri, kod_daerah, daerah, daerah_spe) 
                            VALUES (@id, @kod_negeri, @kod_daerah, @daerah, @daerah_spe) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(tdaerah ent)
        {
            int i = 0;
            string qry = @"";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Delete(int ID)
        {
            int i = 0;
            string qry = @"";
            i = sqliteCon.Execute(qry, new { CSName = ID });
            return i;
        }

        public IList<tdaerah> GetAll()
        {
            string qry = @"SELECT * FROM daerah";
            IList<tdaerah> lstEnt = sqliteCon.Query<tdaerah>(qry, null).ToList();
            return lstEnt;
        }

        public tdaerah GetBy(int ID)
        {
            string qry = @"SELECT * FROM daerah WHERE id=@ID";
            tdaerah ent = sqliteCon.Query<tdaerah>(qry, new { ID }).FirstOrDefault();
            return ent;
        }

        public IList<tdaerah> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, tdaerah whareClause = null)
        {
            //int PageSize = page * rows;
            string qry = @"SELECT * FROM daerah LIMIT @page, @PageSize ";
            string cntQry = @"SELECT COUNT(*) FROM daerah";
            rowCount = sqliteCon.Query<int>(cntQry, null).FirstOrDefault();
            IList<tdaerah> lstEnt = sqliteCon.Query<tdaerah>(qry, new { page = page, PageSize = rows }).ToList();
            return lstEnt;
        }

        public IList<tdaerah> GetDaerahBy(string negeri)
        {
            string qry = @"SELECT * FROM daerah WHERE kod_negeri=@negeri";
            IList<tdaerah> ent = sqliteCon.Query<tdaerah>(qry, new { negeri }).ToList();
            return ent;
        }

        public tdaerah GetBy(string kod_daerah)
        {
            string qry = @"SELECT * FROM daerah WHERE kod_daerah=@kod_daerah";
            tdaerah ent = sqliteCon.Query<tdaerah>(qry, new { kod_daerah }).FirstOrDefault();
            return ent;
        }
    }
}
