using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IDunRepo : IBaseTRepo<dun, int>
    {

        IList<dun> GetDunBy(string negeri);
    }
    public class DunRepo : DefaultRepo<dun, int>, IDunRepo
    {


        public override int Create(dun ent)
        {
            int i = 0;
            string qry = @"INSERT INTO dun (id,	kod_negeri,	kod_dun,	dun_desc) VALUES (@id,	@kod_negeri,	@kod_dun,	@dun_desc) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public override int Edit(dun ent)
        {
            int i = 0;
            string qry = @"UPDATE dun SET kod_negeri=@kod_negeri,	kod_dun=@kod_dun,	dun_desc=@dun_desc  WHERE ID=@id";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public override int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE from dun  WHERE ID=@id";
            i = sqliteCon.Execute(qry, ID);
            return i;
        }

        public override IList<dun> GetAll()
        {
            string qry = @"SELECT * FROM DUN";
            IList<dun> lstEnt = sqliteCon.Query<dun>(qry, null).ToList();
            return lstEnt;
        }

        public override dun GetBy(int ID)
        {
            string qry = @"SELECT * FROM DUN WHERE ID=@ID";
            dun ent = sqliteCon.Query<dun>(qry, ID).FirstOrDefault();
            return ent;
        }


        public override IList<dun> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, dun whareClause = null)
        {
            throw new NotImplementedException();
        }

        public IList<dun> GetDunBy(string negeri)
        {
            string qry = @"SELECT * FROM dun WHERE kod_negeri=@negeri";
            IList<dun> ent = sqliteCon.Query<dun>(qry, new { negeri }).ToList();
            return ent;
        }
    }
}
