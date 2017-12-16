using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using PortableApps.Model.DTO;
using MySql.Data.MySqlClient;
using System.Data;

namespace PortableApps.Repo
{
    public interface IAppInfoRepo : IBaseTRepo<appinfo, int>
    {
        IList<appinfoDTO> PagedListDTO(int page, int rows, string sidx, string sodx, out int rowCount, appinfo oWhereClause = null);
        int GetMaxAppInfoBy(string refno);
        int GetMaxAppInfoBy();
        int CreateMySQL(appinfo ent, IDbTransaction mySqlTrans = null);
        int GetMaxRefNoMySQL(string refno, IDbTransaction mySqlTrans = null);
        int GetMaxRefNoMySQL(IDbTransaction mySqlTrans = null);
        int UpdateSync(appinfo appinfoSqlite);

        void ResetMySQLConnection(bool action = false);

        IList<appinfo> GetAllWithoutSync();
        IList<appinfo> GetListMySQL(int page, int pagesize, string sidx, string sord, appinfo oWhereClause, out int rowcount);
    }
    public class AppInfoRepo : DefaultRepo<appinfo, int>, IAppInfoRepo
    {
        ISqLiteBaseRepository SqLiteBaseRepository = new SqLiteBaseRepository();

        public override int Create(appinfo ent)
        {
            int i = 0;
            //using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            //{
            // TSSPK
            //string qry = @"INSERT INTO appinfo (id,	refno,	nama,	type_id,	icno,	nolesen,	bangsa,	addr1,	addr2,	addr3,	bandar,	daerah,	dun,	parlimen,	poskod,	negeri,	hometel,	officetel,	hptel,	faks,	email,	kelompok,	created,	createdby,	appdate,	semak_tapak,	keputusan,	sts_bck,	status,	date_approved,	approved_by,	sop)
            //                    VALUES
            //                (@id,	@refno,	@nama,	@type_id,	@icno,	@nolesen,	@bangsa,	@addr1,	@addr2,	@addr3,	@bandar,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri,	@hometel,	@officetel,	@hptel,	@faks,	@email,	@kelompok,	@created,	@createdby,	@appdate,	@semak_tapak,	@keputusan,	@sts_bck,	@status,	@date_approved,	@approved_by,	@sop)
            //            ";
            // TBSPK
            string qry = @"INSERT INTO appinfo (id,	refno,	nama
                                    ,	icno,	nolesen,	bangsa,	addr1,	addr2,	addr3,	bandar,	daerah,	dun,	parlimen,	poskod,	negeri,	hometel,	officetel,	hptel
                                    ,	faks,	email,	created,	createdby,	appdate
                                    )
                                VALUES
                            (@id,	@refno,	@nama,	@icno,	@nolesen,	@bangsa,	@addr1,	@addr2,	@addr3,	@bandar,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri
                                ,	@hometel,	@officetel,	@hptel,	@faks,	@email,	@created,	@createdby,	@appdate
                                )
                        ";
            i = sqliteCon.Execute(qry, ent);
            //}
            return i;
        }

        public int CreateMySQL(appinfo ent, IDbTransaction mySqlTrans = null)
        {
            int i = 0;
            //using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            //{
            //string qry = @"INSERT INTO appinfo (id,	refno,	nama,	type_id,	icno,	nolesen,	bangsa,	addr1,	addr2,	addr3,	bandar,	daerah,	dun,	parlimen,	poskod,	negeri,	hometel,	officetel,	hptel,	faks,	email,	kelompok,	created,	createdby,	appdate,	semak_tapak,	keputusan,	sts_bck,	status,	date_approved,	approved_by,	sop)
            //                    VALUES
            //                (@id,	@newrefno,	@nama,	@type_id,	@icno,	@nolesen,	@bangsa,	@addr1,	@addr2,	@addr3,	@bandar,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri,	@hometel,	@officetel,	@hptel,	@faks,	@email,	@kelompok,	@created,	@createdby,	@appdate,	@semak_tapak,	@keputusan,	@sts_bck,	@status,	@date_approved,	@approved_by,	@sop)
            //            ";
            string qry = @"INSERT INTO appinfo (id,	refno,	nama
                                    ,	icno,	nolesen,	bangsa,	addr1,	addr2,	addr3,	bandar,	daerah,	dun,	parlimen,	poskod,	negeri,	hometel,	officetel
                                    ,	hptel,	faks,	email,	created,	createdby,	appdate
                                    )
                                VALUES
                            (@id,	@newrefno,	@nama
                                    ,	@icno,	@nolesen,	@bangsa,	@addr1,	@addr2,	@addr3,	@bandar,	@daerah,	@dun,	@parlimen,	@poskod,	@negeri,	@hometel
                                    ,	@officetel,	@hptel,	@faks,	@email,	@created,	@createdby,	@appdate
                                    )
                        ";
            i = mysqlCon.Execute(qry, ent, mySqlTrans);
            //}
            return i;
        }

        public override int Edit(appinfo ent)
        {
            int i = 0;
            //string qry = @"UPDATE appinfo SET
            //                    refno=@refno,
            //                    nama=@nama,
            //                    type_id=@type_id,
            //                    icno=@icno,
            //                    nolesen=@nolesen,
            //                    bangsa=@bangsa,
            //                    addr1=@addr1,
            //                    addr2=@addr2,
            //                    addr3=@addr3,
            //                    bandar=@bandar,
            //                    daerah=@daerah,
            //                    dun=@dun,
            //                    parlimen=@parlimen,
            //                    poskod=@poskod,
            //                    negeri=@negeri,
            //                    hometel=@hometel,
            //                    officetel=@officetel,
            //                    hptel=@hptel,
            //                    faks=@faks,
            //                    email=@email,
            //                    kelompok=@kelompok,
            //                    created=@created,
            //                    createdby=@createdby,
            //                    appdate=@appdate,
            //                    semak_tapak=@semak_tapak,
            //                    keputusan=@keputusan,
            //                    sts_bck=@sts_bck,
            //                    status=@status,
            //                    date_approved=@date_approved,
            //                    approved_by=@approved_by,
            //                    sop=@sop
            //                    WHERE id=@id
            //                ";
            // TB&TS SPK
            string qry = @"UPDATE appinfo SET
                                refno=@refno,
                                nama=@nama,
                                icno=@icno,
                                nolesen=@nolesen,
                                bangsa=@bangsa,
                                addr1=@addr1,
                                addr2=@addr2,
                                addr3=@addr3,
                                bandar=@bandar,
                                daerah=@daerah,
                                dun=@dun,
                                parlimen=@parlimen,
                                poskod=@poskod,
                                negeri=@negeri,
                                hometel=@hometel,
                                officetel=@officetel,
                                hptel=@hptel,
                                faks=@faks,
                                email=@email,
                                created=@created,
                                createdby=@createdby,
                                appdate=@appdate
                                WHERE id=@id
                            ";
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                i = cnn.Execute(qry, ent);
            }
            return i;
        }

        public override int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE FROM appinfo  WHERE id=@id";
            i = sqliteCon.Execute(qry, new { id = ID });
            qry = @"DELETE FROM makkebun  WHERE appinfo_id=@id";
            i = i + sqliteCon.Execute(qry, new { id = ID });
            qry = @"DELETE FROM semak_tapak  WHERE appinfo_id=@id";
            i = i + sqliteCon.Execute(qry, new { id = ID });
            return i;
        }

        public override IList<appinfo> GetAll()
        {
            string qry = @"SELECT * FROM appinfo";
            IList<appinfo> lst = new List<appinfo>();
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                lst = cnn.Query<appinfo>(qry, null).ToList();
            }
            return lst;
        }

        public override appinfo GetBy(int ID)
        {
            string qry = @"SELECT * FROM appinfo WHERE ID=@ID";
            appinfo appinfo = null;
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                appinfo = cnn.Query<appinfo>(qry, new { ID }).FirstOrDefault();
            }
            return appinfo;
        }

        public override IList<appinfo> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, appinfo oWhereClause = null)
        {
            string whereClause = "";
            string operators = "";
            if (!object.Equals(null, oWhereClause))
            {
                if (oWhereClause.id > 0)
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " id=@id ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.bangsa))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " bangsa like '%'+@bangsa+'%'  ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.negeri))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " negeri like '%'+@negeri+'%'   ";
                }
            }

            //string qry = string.Format(@"SELECT id, nama, icno, negeri, nolesen, refno, appdate, keputusan FROM appinfo 
            //                                {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sodx, (page - 1) * rows, rows);
            string qry = string.Format(@"SELECT id, nama, icno, negeri, nolesen, refno, appdate FROM appinfo 
                                            {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sodx, (page - 1) * rows, rows);
            string qryCtn = string.Format(@"SELECT COUNT(*) FROM appinfo {0}", whereClause);

            IList<appinfo> lstEnt = sqliteCon.Query<appinfo>(qry, null).ToList();
            rowCount = sqliteCon.Query<int>(qryCtn, null).FirstOrDefault();
            return lstEnt;
        }

        public IList<appinfoDTO> PagedListDTO(int page, int rows, string sidx, string sodx, out int rowCount, appinfo oWhereClause = null)
        {
            string whereClause = "";
            string operators = "";
            if (!object.Equals(null, oWhereClause))
            {
                if (oWhereClause.id > 0)
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " id=@id ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.bangsa))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " bangsa = @bangsa ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.negeri))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " negeri = @negeri ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.daerah))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " daerah = @daerah ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.dun))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " dun = @dun   ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.refno))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " refno like '%'||@refno||'%'   ";
                }
                if (oWhereClause.parlimen > 0)
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " parlimen =@parlimen ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.icno))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " icno like '%'||@icno||'%'   ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.nolesen))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " nolesen like '%'||@nolesen||'%'   ";
                }
                if (oWhereClause.appdate != null && !string.IsNullOrEmpty(oWhereClause.appdate.Trim()))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " appdate =@appdate   ";
                }
            }

            operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
            whereClause = whereClause + operators + " type COLLATE NOCASE ='Negeri' ";

            IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
            VariableSetting VariableSetting = VariableSettingRepo.GetBy("HiddenDataSync");

            if (VariableSetting != null && Convert.ToBoolean(VariableSetting.Value))
            {
                operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                whereClause = whereClause + operators + " syncdate is null ";
            }

            //string qry = string.Format(@"SELECT nama, icno, value negeri, nolesen, refno, appdate, created, createdby, id, newrefno, syncdate
            //                                -- , keputusan 
            //                                FROM appinfo join variables
            //                                ON negeri = code
            //                                {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sodx, (page - 1) * rows, rows);
            string qry = string.Format(@"SELECT nama, icno, value negeri, nolesen, refno, appdate, created, createdby, id, newrefno, syncdate
                                            FROM appinfo join variables
                                            ON negeri = code
                                            {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sodx, (page - 1) * rows, rows);
            string qryCtn = string.Format(@"SELECT COUNT(*) FROM appinfo join variables
                                            ON negeri = code {0}", whereClause);

            IList<appinfoDTO> lstEnt = sqliteCon.Query<appinfoDTO>(qry, oWhereClause).ToList();
            rowCount = sqliteCon.Query<int>(qryCtn, oWhereClause).FirstOrDefault();
            return lstEnt;
        }

        public int GetMaxAppInfoBy(string refno)
        {
            int i = 0;
            string qry = @"SELECT MAX(refno) refno from appinfo where refno like @refno || '%' COLLATE NOCASE";
            appinfo appinfo = sqliteCon.Query<appinfo>(qry, new { refno }).FirstOrDefault();
            if (appinfo != null && !string.IsNullOrEmpty(appinfo.refno))
            {
                string[] nowrefno = appinfo.refno.Split('/');
                try
                {
                    i = Convert.ToInt32(nowrefno[2]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return i + 1;
        }

        public int GetMaxRefNoMySQL(string refno, IDbTransaction mySqlTrans = null)
        {
            int i = 0;
            string qry = @"SELECT MAX(refno) refno from appinfo where refno like CONCAT(@refno, '%') LOCK IN SHARE MODE";
            appinfo appinfo = mysqlCon.Query<appinfo>(qry, new { refno }, mySqlTrans).FirstOrDefault();
            if (appinfo != null && !string.IsNullOrEmpty(appinfo.refno))
            {
                string[] nowrefno = appinfo.refno.Split('/');
                try
                {
                    i = Convert.ToInt32(nowrefno[2]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return i + 1;
        }

        public int UpdateSync(appinfo appinfoSqlite)
        {
            int i = 0;
            string qry = @"UPDATE appinfo SET
                                newrefno=@newrefno,
                                syncdate=@syncdate
                                WHERE id=@id
                            ";
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                i = cnn.Execute(qry, appinfoSqlite);
            }
            return i;
        }

        public IList<appinfo> GetAllWithoutSync()
        {
            string qry = @"SELECT * FROM appinfo where IFNULL(newrefno,'')='' and syncdate is null";
            IList<appinfo> lst = new List<appinfo>();
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                lst = cnn.Query<appinfo>(qry, null).ToList();
            }
            return lst;
        }

        public IList<appinfo> GetListMySQL(int page, int pagesize, string sidx, string sord, appinfo oWhereClause, out int rowcount)

        {
            string whereClause = "";
            string operators = "";
            if (!object.Equals(null, oWhereClause))
            {
                if (oWhereClause.id > 0)
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " id=@id ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.bangsa))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " bangsa like '%'+@bangsa+'%'  ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.negeri))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " negeri like '%'+@negeri+'%'   ";
                }
            }

            //string qry = string.Format(@"SELECT id, nama, icno, negeri, nolesen, refno, appdate, keputusan FROM appinfo 
            //                                {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sord, (page - 1) * pagesize, pagesize);
            string qry = string.Format(@"SELECT id, nama, icno, negeri, nolesen, refno, appdate FROM appinfo 
                                            {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sord, (page - 1) * pagesize, pagesize);
            string qryCtn = string.Format(@"SELECT COUNT(*) FROM appinfo {0}", whereClause);

            IList<appinfo> lstEnt = mysqlCon.Query<appinfo>(qry, null).ToList();
            rowcount = mysqlCon.Query<int>(qryCtn, null).FirstOrDefault();
            return lstEnt;
        }

        public int GetMaxRefNoMySQL(IDbTransaction mySqlTrans = null)
        {
            int i = 0;
            //string qry = @"SELECT max(cast(SUBSTRING_INDEX(refno, '/', -1) as signed)) lastNumber from appinfo";
            string qry = @"SELECT max(cast(SUBSTRING_INDEX(refno, '/', -1) as signed)) lastNumber from appinfo";
            try
            {
                i = mysqlCon.Query<int>(qry, null, mySqlTrans).FirstOrDefault();
            }
            catch (Exception)
            {
            }
            return i + 1;
        }

        public void ResetMySQLConnection(bool action = false)
        {
            if (action)
            {
                ResetMySQLConnection();
            }
        }

        public int GetMaxAppInfoBy()
        {
            int i = 0;
            //string qry = @"SELECT max(SUBSTRING_INDEX(refno, '/', -1)) lastNumber from appinfo";
            //string qry = @"SELECT  substr(refno, INSTR(refno, '/')+1),substr(substr(refno, INSTR(refno, '/')+1), INSTR(substr(refno, INSTR(refno, '/')+1), '/')+1) from appinfo";
            string qry = @"SELECT  max(cast(substr(substr(refno, INSTR(refno, '/')+1), INSTR(substr(refno, INSTR(refno, '/')+1), '/')+1) as int)) from appinfo";
            try
            {
                i = sqliteCon.Query<int>(qry, null).FirstOrDefault();
            }
            catch (Exception)
            {
            }
            return i + 1;
        }
    }
}
