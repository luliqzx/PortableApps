using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using PortableApps.Model.DTO;

namespace PortableApps.Repo
{
    public interface IMakkebunRepo : IBaseTRepo<makkebun, int>
    {
        IList<makkebunDTO> PagedListDTO(int pageIndex, int pageSize, string sidx, string sord, out int totalRecords, makkebun oWhereClause);
    }
    public class MakkebunRepo : CommonRepo, IMakkebunRepo
    {
        public int Create(makkebun ent)
        {
            int i = 0;
            string qry = @"INSERT INTO makkebun (
                                --id_makkebun,	
                                appinfo_id,	addr1,	addr2,	addr3,	daerah,	dun,	parlimen,	poskod,	negeri,	nolot,	hakmiliktanah,	tncr
                            ,	luasmatang,	tebang,	tarikhtebang,	nolesen,	syarattanah,	pemilikan,	pengurusan,	luaslesen,	catatan,	created,	createdby) 
                            VALUES (
                                -- @id_makkebun,	
                                @appinfo_id,	@addr1,	@addr2,	@addr3,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri,	@nolot,	@hakmiliktanah,	@tncr
                            ,	@luasmatang,	@tebang,	@tarikhtebang,	@nolesen,	@syarattanah,	@pemilikan,	@pengurusan,	@luaslesen,	@catatan,	@created
                            ,	@createdby) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(makkebun ent)
        {
            int i = 0;
            string qry = @"UPDATE makkebun SET 
                                    id_makkebun=@id_makkebun,	
                                    appinfo_id=@appinfo_id,	addr1=@addr1,	addr2=@addr2,	addr3=@addr3,	daerah=@daerah,	dun=@dun
                                ,	parlimen=@parlimen,	poskod=@poskod,	negeri=@negeri,	nolot=@nolot,	hakmiliktanah=@hakmiliktanah,	tncr=@tncr,	luasmatang=@luasmatang
                                ,	tebang=@tebang,	tarikhtebang=@tarikhtebang,	nolesen=@nolesen,	syarattanah=@syarattanah,	pemilikan=@pemilikan,	pengurusan=@pengurusan
                                ,	luaslesen=@luaslesen,	catatan=@catatan,	created=@created,	createdby=@createdby
                                 WHERE id_makkebun=@id_makkebun and appinfo_id=@appinfo_id";
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

        public IList<makkebunDTO> PagedListDTO(int pageIndex, int pageSize, string sidx, string sord, out int totalRecords, makkebun oWhereClause)
        {
            string whereClause = "";
            string operators = "";
            if (!object.Equals(null, oWhereClause))
            {
                if (oWhereClause.appinfo_id > 0)
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " appinfo_id=@appinfo_id ";
                }
            }

            string qry = string.Format(@"SELECT * FROM MAKKEBUN
                                            {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sord, (pageIndex - 1) * pageSize, pageSize);
            string qryCtn = string.Format(@"SELECT COUNT(*) FROM MAKKEBUN {0}", whereClause);

            IList<makkebunDTO> lstEnt = sqliteCon.Query<makkebunDTO>(qry, oWhereClause).ToList();
            totalRecords = sqliteCon.Query<int>(qryCtn, oWhereClause).FirstOrDefault();
            return lstEnt;
        }
    }
}
