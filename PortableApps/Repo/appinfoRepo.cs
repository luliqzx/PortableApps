using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IAppInfoRepo : IBaseTRepo<appinfo, int>
    {
    }
    public class AppInfoRepo : IAppInfoRepo
    {
        ISqLiteBaseRepository SqLiteBaseRepository = new SqLiteBaseRepository();

        public int Create(appinfo ent)
        {
            int i = 0;
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                string qry = @"INSERT INTO appinfo (id,	refno,	nama,	type_id,	icno,	nolesen,	bangsa,	addr1,	addr2,	addr3,	bandar,	daerah,	dun,	parlimen,	poskod,	negeri,	hometel,	officetel,	hptel,	faks,	email,	kelompok,	created,	createdby,	appdate,	semak_tapak,	keputusan,	sts_bck,	status,	date_approved,	approved_by,	sop)
                                VALUES
                            (@dun,	@parlimen,	@poskod,	@negeri,	@hometel,	@officetel,	@hptel,	@faks,	@email,	@kelompok,	@created,	@createdby,	@appdate,	@semak_tapak,	@keputusan,	@sts_bck,	@status,	@date_approved,	@approved_by,	@sop)
                        ";
                i = cnn.Execute(qry, ent);
            }
            return i;
        }

        public int Edit(appinfo ent)
        {
            int i = 0;
            string qry = @"UPDATE appinfo SET
                                refno=@refno,
                                nama=@nama,
                                type_id=@type_id,
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
                                kelompok=@kelompok,
                                created=@created,
                                createdby=@createdby,
                                appdate=@appdate,
                                semak_tapak=@semak_tapak,
                                keputusan=@keputusan,
                                sts_bck=@sts_bck,
                                status=@status,
                                date_approved=@date_approved,
                                approved_by=@approved_by,
                                sop=@sop
                                WHERE id=@id
                            ";
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                i = cnn.Execute(qry, ent);
            }
            return i;
        }

        public int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE FROM appinfo  WHERE id=@id";
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                i = cnn.Execute(qry, ID);
            }
            return i;
        }

        public IList<appinfo> GetAll()
        {
            string qry = @"SELECT * FROM appinfo";
            IList<appinfo> lst = new List<appinfo>();
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                lst = cnn.Query<appinfo>(qry, null).ToList();
            }
            return lst;
        }

        public appinfo GetBy(int ID)
        {
            string qry = @"SELECT * FROM appinfo WHERE ID=@ID";
            appinfo appinfo = null;
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                appinfo = cnn.Query<appinfo>(qry, ID).FirstOrDefault();
            }
            return appinfo;
        }


        public IList<appinfo> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, appinfo whareClause = null)
        {
            throw new NotImplementedException();
        }
    }
}
