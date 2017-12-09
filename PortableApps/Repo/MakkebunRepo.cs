using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using PortableApps.Model.DTO;
using System.Data;

namespace PortableApps.Repo
{
    public interface IMakkebunRepo : IBaseTRepo<makkebun, int>
    {
        IList<makkebunDTO> PagedListDTO(int pageIndex, int pageSize, string sidx, string sord, out int totalRecords, makkebun oWhereClause);
        IList<makkebunDTO> GetAllAppInfoBy(int appinfo_id);
        IList<makkebun> GetAllByAppInfo(int id);
        int CreateMySQL(makkebun makkebunSqlite, IDbTransaction sqlTrans = null);
        makkebun GetLastMakkebunBy(int appinfo_id);
        int UpdateSync(makkebun makkebunSqlite);
        IList<makkebun> GetAllToSync();
    }
    public class MakkebunRepo : DefaultRepo<makkebun, int>, IMakkebunRepo
    {
        public override int Create(makkebun ent)
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

        public override int Edit(makkebun ent)
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

        public override int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE from makkebun  WHERE id_makkebun=@id_makkebun";
            i = sqliteCon.Execute(qry, new { id_makkebun = ID });
            qry = @"DELETE from semak_tapak  WHERE makkebun_id=@id_makkebun";
            i = i + sqliteCon.Execute(qry, new { id_makkebun = ID });
            return i;
        }

        public override IList<makkebun> GetAll()
        {
            string qry = @"SELECT * FROM makkebun";
            IList<makkebun> lstEnt = sqliteCon.Query<makkebun>(qry, null).ToList();
            return lstEnt;
        }

        public override makkebun GetBy(int ID)
        {
            string qry = @"SELECT * FROM makkebun WHERE id_makkebun=@id_makkebun";
            makkebun ent = sqliteCon.Query<makkebun>(qry, new { id_makkebun = ID }).FirstOrDefault();
            return ent;
        }

        public override IList<makkebun> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, makkebun whareClause = null)
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
                    whereClause = whereClause + operators + " a.appinfo_id=@appinfo_id ";
                }
            }

            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            VariableSetting VariableSetting = VariableSettingRepo.GetBy("AlreadySyncDisplay");

            if (VariableSetting != null && Convert.ToBoolean(VariableSetting.Value))
            {
                operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                whereClause = whereClause + operators + " c.syncdate is null ";
            }

            string qry = string.Format(@"SELECT a.*, IFNULL(b.tarikh_lawat, 'BELUM LAWAT') tarikh_lawat, b.id semak_tapak_id FROM MAKKEBUN a LEFT JOIN SEMAK_TAPAK b
                                            ON a.id_makkebun = b.makkebun_id AND a.appinfo_id = b.appinfo_id join appinfo c on a.appinfo_id = c.id
                                            {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sord, (pageIndex - 1) * pageSize, pageSize);
            string qryCtn = string.Format(@"SELECT COUNT(a.id_makkebun)  FROM MAKKEBUN a LEFT JOIN SEMAK_TAPAK b
                                            ON a.id_makkebun = b.makkebun_id AND a.appinfo_id = b.appinfo_id join appinfo c on a.appinfo_id = c.id {0}", whereClause);

            IList<makkebunDTO> lstEnt = sqliteCon.Query<makkebunDTO>(qry, oWhereClause).ToList();
            totalRecords = sqliteCon.Query<int>(qryCtn, oWhereClause).FirstOrDefault();
            return lstEnt;
        }

        public IList<makkebunDTO> GetAllAppInfoBy(int appinfo_id)
        {
            string qry = @"SELECT a.*, IFNULL(b.tarikh_lawat, 'BELUM LAWAT') tarikh_lawat, b.id semak_tapak_id FROM MAKKEBUN a LEFT JOIN SEMAK_TAPAK b
                                            ON a.id_makkebun = b.makkebun_id AND a.appinfo_id = b.appinfo_id WHERE a.appinfo_id=@appinfo_id";
            IList<makkebunDTO> ent = sqliteCon.Query<makkebunDTO>(qry, new { appinfo_id }).ToList();
            return ent;
        }

        public IList<makkebun> GetAllByAppInfo(int id)
        {
            string qry = @"SELECT * FROM makkebun where appinfo_id=@appinfo_id";
            IList<makkebun> lstEnt = sqliteCon.Query<makkebun>(qry, new { appinfo_id = id }).ToList();
            return lstEnt;
        }

        public int CreateMySQL(makkebun makkebunSqlite, IDbTransaction sqlTrans = null)
        {
            int i = 0;
            string qry = @"INSERT INTO makkebun (
                                appinfo_id,	addr1,	addr2,	addr3,	daerah,	dun,	parlimen,	poskod,	negeri,	nolot,	hakmiliktanah,	tncr
                            ,	luasmatang,	tebang,	tarikhtebang,	nolesen,	syarattanah,	pemilikan,	pengurusan,	luaslesen,	catatan,	created,	createdby) 
                            VALUES (
                                @appinfo_id,	@addr1,	@addr2,	@addr3,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri,	@nolot,	@hakmiliktanah,	@tncr
                            ,	@luasmatang,	@tebang,	@tarikhtebang,	@nolesen,	@syarattanah,	@pemilikan,	@pengurusan,	@luaslesen,	@catatan,	@created
                            ,	@createdby) ";
            i = mysqlCon.Execute(qry, makkebunSqlite, transaction: sqlTrans);
            return i;
        }

        public makkebun GetLastMakkebunBy(int appinfo_id)
        {
            string qry = @"SELECT * FROM makkebun WHERE id_makkebun=(SELECT MAX(id_makkebun) FROM makkebun WHERE appinfo_id=@appinfo_id)";
            makkebun ent = mysqlCon.Query<makkebun>(qry, new { appinfo_id = appinfo_id }).FirstOrDefault();
            return ent;
        }

        public int UpdateSync(makkebun makkebunSqlite)
        {
            int i = 0;
            string qry = @"UPDATE makkebun SET 
                                    newid_makkebun=@newid_makkebun,	
                                    syncdate=@syncdate	
                                 WHERE id_makkebun=@id_makkebun and appinfo_id=@appinfo_id";
            i = sqliteCon.Execute(qry, makkebunSqlite);
            return i;
        }

        public IList<makkebun> GetAllToSync()
        {
            string qry = @"SELECT * FROM makkebun WHERE syncdate is null";
            IList<makkebun> lstEnt = sqliteCon.Query<makkebun>(qry, null).ToList();
            return lstEnt;
        }
    }
}
