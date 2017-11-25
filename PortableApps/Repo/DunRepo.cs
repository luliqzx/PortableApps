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

    }
    public class DunRepo : CommonRepo, IDunRepo
    {


        public int Create(dun ent)
        {
            int i = 0;
            string qry = @"INSERT INTO dun (id,	kod_negeri,	kod_dun,	dun_desc) VALUES (@id,	@kod_negeri,	@kod_dun,	@dun_desc) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(dun ent)
        {
            int i = 0;
            string qry = @"UPDATE dun SET kod_negeri=@kod_negeri,	kod_dun=@kod_dun,	dun_desc=@dun_desc  WHERE ID=@id";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE from dun  WHERE ID=@id";
            i = sqliteCon.Execute(qry, ID);
            return i;
        }

        public IList<dun> GetAll()
        {
            string qry = @"SELECT * FROM DUN";
            IList<dun> lstEnt = sqliteCon.Query<dun>(qry, null).ToList();
            return lstEnt;
        }

        public dun GetBy(int ID)
        {
            string qry = @"SELECT * FROM DUN WHERE ID=@ID";
            dun ent = sqliteCon.Query<dun>(qry, ID).FirstOrDefault();
            return ent;
        }


        public IList<dun> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, dun whareClause = null)
        {
            throw new NotImplementedException();
        }
    }
}
