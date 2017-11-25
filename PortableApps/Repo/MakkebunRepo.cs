using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IMakkebunRepo : IBaseTRepo<makkebun, int>
    {

    }
    public class MakkebunRepo : CommonRepo, IMakkebunRepo
    {


        public int Create(makkebun ent)
        {
            int i = 0;
            string qry = @"INSERT INTO makkebun (id_makkebun,	appinfo_id,	addr1,	addr2,	addr3,	daerah,	dun,	parlimen,	poskod,	negeri,	nolot,	hakmiliktanah,	tncr
                            ,	luasmatang,	tebang,	tarikhtebang,	nolesen,	syarattanah,	pemilikan,	pengurusan,	luaslesen,	catatan,	created,	createdby) 
                            VALUES (@id_makkebun,	@appinfo_id,	@addr1,	@addr2,	@addr3,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri,	@nolot,	@hakmiliktanah,	@tncr
                            ,	@luasmatang,	@tebang,	@tarikhtebang,	@nolesen,	@syarattanah,	@pemilikan,	@pengurusan,	@luaslesen,	@catatan,	@created
                            ,	@createdby) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(makkebun ent)
        {
            int i = 0;
            string qry = @"UPDATE id_makkebun=@id_makkebun,	appinfo_id=@appinfo_id,	addr1=@addr1,	addr2=@addr2,	addr3=@addr3,	daerah=@daerah,	dun=@dun
                                ,	parlimen=@parlimen,	poskod=@poskod,	negeri=@negeri,	nolot=@nolot,	hakmiliktanah=@hakmiliktanah,	tncr=@tncr,	luasmatang=@luasmatang
                                ,	tebang=@tebang,	tarikhtebang=@tarikhtebang,	nolesen=@nolesen,	syarattanah=@syarattanah,	pemilikan=@pemilikan,	pengurusan=@pengurusan
                                ,	luaslesen=@luaslesen,	catatan=@catatan,	created=@created,	createdby=@createdby
                                 WHERE id_makkebun=@id_makkebun";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE from makkebun  WHERE id_makkebun=@id_makkebun";
            i = sqliteCon.Execute(qry, new { id_makkebun = ID });
            return i;
        }

        public IList<makkebun> GetAll()
        {
            string qry = @"SELECT * FROM makkebun";
            IList<makkebun> lstEnt = sqliteCon.Query<makkebun>(qry, null).ToList();
            return lstEnt;
        }

        public makkebun GetBy(int ID)
        {
            string qry = @"SELECT * FROM makkebun WHERE id_makkebun=@id_makkebun";
            makkebun ent = sqliteCon.Query<makkebun>(qry, new { id_makkebun = ID }).FirstOrDefault();
            return ent;
        }


        public IList<makkebun> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, makkebun whareClause = null)
        {
            throw new NotImplementedException();
        }
    }
}
