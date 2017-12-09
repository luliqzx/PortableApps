using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;

namespace PortableApps.Repo
{
    public interface ISemakTapakRepo : IBaseTRepo<semak_tapak, int>
    {
        semak_tapak GetBy(int appinfo_id, int makkebun_id, int? semak_tapak_id);
        semak_tapak GetBy(int appinfo_id, int makkebun_id);
        int CreateMySQL(semak_tapak semak_tapakSqlite, IDbTransaction sqlTrans = null);
        semak_tapak GetLastSemakTapakBy(int id, int? newmakkebun_id);
        int UpdateSync(semak_tapak semak_tapakSqlite);
    }
    public class SemakTapakRepo : DefaultRepo<semak_tapak, int>, ISemakTapakRepo
    {
        public override int Create(semak_tapak ent)
        {
            int i = 0;
            string qry = @"INSERT INTO semak_tapak (
                                        -- id,	
                                        makkebun_id, appinfo_id,	kaedah,	bantuan,	jenis_tanah,	kecerunan,	jentera,	ganoderma,	peratusan_serangan,	umr_pokok_tua,	hasil,	bil_pokok_tua,	tarikh_lawat,	ptk_lawat,	luas,	catatan,	created,	createdby,	lampiran) 
                            VALUES (
                               -- @id,	
                                @makkebun_id,	@appinfo_id,	@kaedah,	@bantuan,	@jenis_tanah,	@kecerunan,	@jentera,	@ganoderma,	@peratusan_serangan,	@umr_pokok_tua,	@hasil,	@bil_pokok_tua,	@tarikh_lawat,	@ptk_lawat,	@luas,	@catatan,	@created,	@createdby,	@lampiran) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public override int Edit(semak_tapak ent)
        {
            int i = 0;
            string qry = @"UPDATE SEMAK_TAPAK SET id=@id,	makkebun_id=@makkebun_id,	appinfo_id=@appinfo_id,	kaedah=@kaedah,	bantuan=@bantuan,	jenis_tanah=@jenis_tanah,	kecerunan=@kecerunan,	jentera=@jentera,	ganoderma=@ganoderma,	peratusan_serangan=@peratusan_serangan,	umr_pokok_tua=@umr_pokok_tua,	hasil=@hasil,	bil_pokok_tua=@bil_pokok_tua,	tarikh_lawat=@tarikh_lawat,	ptk_lawat=@ptk_lawat,	luas=@luas,	catatan=@catatan,	created=@created,	createdby=@createdby,	lampiran=@lampiran
                                WHERE id=@id";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public override int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE from semak_tapak  WHERE id=@id";
            i = sqliteCon.Execute(qry, new { id_semak_tapak = ID });
            return i;
        }

        public override IList<semak_tapak> GetAll()
        {
            string qry = @"SELECT * FROM semak_tapak";
            IList<semak_tapak> lstEnt = sqliteCon.Query<semak_tapak>(qry, null).ToList();
            return lstEnt;
        }

        public override semak_tapak GetBy(int ID)
        {
            string qry = @"SELECT * FROM semak_tapak WHERE id=@id";
            semak_tapak ent = sqliteCon.Query<semak_tapak>(qry, new { id = ID }).FirstOrDefault();
            return ent;
        }

        public override IList<semak_tapak> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, semak_tapak oWhareClause = null)
        {
            throw new NotImplementedException();
        }

        public semak_tapak GetBy(int appinfo_id, int makkebun_id, int? semak_tapak_id)
        {
            string qry = @"SELECT * FROM semak_tapak WHERE id=@semak_tapak_id and appinfo_id=@appinfo_id and makkebun_id=@makkebun_id";
            semak_tapak ent = sqliteCon.Query<semak_tapak>(qry, new { appinfo_id, makkebun_id, semak_tapak_id }).FirstOrDefault();
            return ent;
        }

        public semak_tapak GetBy(int appinfo_id, int makkebun_id)
        {
            string qry = @"SELECT * FROM semak_tapak WHERE appinfo_id=@appinfo_id and makkebun_id=@makkebun_id";
            semak_tapak ent = sqliteCon.Query<semak_tapak>(qry, new { appinfo_id, makkebun_id }).FirstOrDefault();
            return ent;
        }

        public int CreateMySQL(semak_tapak semak_tapakSqlite, IDbTransaction sqlTrans = null)
        {
            int i = 0;
            string qry = @"INSERT INTO semak_tapak (
                                        -- id,	
                                        makkebun_id, appinfo_id,	kaedah,	bantuan,	jenis_tanah,	kecerunan,	jentera,	ganoderma,	peratusan_serangan,	umr_pokok_tua,	hasil,	bil_pokok_tua,	tarikh_lawat,	ptk_lawat,	luas,	catatan,	created,	createdby,	lampiran) 
                            VALUES (
                               -- @id,	
                                @newmakkebun_id,	@appinfo_id,	@kaedah,	@bantuan,	@jenis_tanah,	@kecerunan,	@jentera,	@ganoderma,	@peratusan_serangan,	@umr_pokok_tua,	@hasil,	@bil_pokok_tua,	@tarikh_lawat,	@ptk_lawat,	@luas,	@catatan,	@created,	@createdby,	@lampiran) ";
            i = mysqlCon.Execute(qry, semak_tapakSqlite, transaction: sqlTrans);
            return i;
        }

        public semak_tapak GetLastSemakTapakBy(int appinfo_id, int? newmakkebun_id)
        {
            string qry = @"SELECT * FROM semak_tapak WHERE ID=(SELECT MAX(ID) FROM semak_tapak WHERE appinfo_id=@appinfo_id and makkebun_id=@newmakkebun_id)";
            semak_tapak ent = mysqlCon.Query<semak_tapak>(qry, new { appinfo_id, newmakkebun_id }).FirstOrDefault();
            return ent;
        }

        public int UpdateSync(semak_tapak semak_tapakSqlite)
        {
            int i = 0;
            string qry = @"UPDATE SEMAK_TAPAK 
                                SET newid=@newid,	newmakkebun_id=@newmakkebun_id
                                    , syncdate=@syncdate
                                WHERE id=@id";
            i = sqliteCon.Execute(qry, semak_tapakSqlite);
            return i;
        }
    }
}
