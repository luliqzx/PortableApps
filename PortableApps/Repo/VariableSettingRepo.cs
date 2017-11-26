using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IVariableSettingRepo : IBaseTRepo<VariableSetting, string>
    {
        void RecreateVariableSettingTable();
    }
    public class VariableSettingRepo : CommonRepo, IVariableSettingRepo
    {
        public int Create(VariableSetting ent)
        {
            int i = 0;
            string qry = @"INSERT INTO VariableSetting (Key, Value, Description, CanModify, IsEncrypt) VALUES (@Key, @Value, @Description, @CanModify, @IsEncrypt) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(VariableSetting ent)
        {
            throw new NotImplementedException();
        }

        public int Delete(string ID)
        {
            int i = 0;
            string qry = @"DELETE FROM VariableSetting WHERE Key=@Key";
            i = sqliteCon.Execute(qry, new { Key = ID });
            return i;
        }

        public IList<VariableSetting> GetAll()
        {
            throw new NotImplementedException();
        }

        public VariableSetting GetBy(string ID)
        {
            string qry = @"SELECT * FROM VariableSetting WHERE KEY=@Key";
            VariableSetting VariableSetting = sqliteCon.Query<VariableSetting>(qry, new { Key = ID }).FirstOrDefault();
            return VariableSetting;
        }

        public IList<VariableSetting> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, VariableSetting oWhereClause)
        {
            string whereClause = "";
            string operators = "";
            if (!object.Equals(null, oWhereClause))
            {
                if (!string.IsNullOrEmpty(oWhereClause.Key))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " Key=@Key ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.Value))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " Value like '%'+@Value+'%'  ";
                }
                if (!string.IsNullOrEmpty(oWhereClause.Description))
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " Description like '%'+@Description+'%'   ";
                }
                if (oWhereClause.CanModify != null)
                {
                    operators = whereClause.StartsWith("WHERE") ? " AND " : "WHERE";
                    whereClause = whereClause + operators + " CanModify=@CanModify ";
                }
            }

            string qry = string.Format(@"SELECT * FROM VariableSetting {0} ORDER BY {1} {2} LIMIT {3}, {4}", whereClause, sidx, sodx, (page - 1) * rows, rows);
            string qryCtn = string.Format(@"SELECT COUNT(*) FROM VariableSetting {0}", whereClause);

            IList<VariableSetting> lstVariableSetting = sqliteCon.Query<VariableSetting>(qry, null).ToList();
            rowCount = sqliteCon.Query<int>(qryCtn, null).FirstOrDefault();
            return lstVariableSetting;
        }

        public void RecreateVariableSettingTable()
        {
            string qry = @"
                            CREATE TABLE IF NOT EXISTS VariableSetting
                            (
                                Key varchar(50) NOT NULL PRIMARY KEY,
                                Value varchar(250) NOT NULL,
                                Description varchar(250),
                                CanModify int NOT NULL,
                                IsEncrypt bit NOT NULL
                            )
                            
                             DELETE FROM VariableSetting

                            INSERT INTO VariableSetting VALUES ('passPhrase', '#1UWLyP1','', 0, 0)
                            INSERT INTO VariableSetting VALUES ('saltValue', 's@1tValue','', 0, 0)
                            INSERT INTO VariableSetting VALUES ('hashAlgorithm', 'SHA1','', 0, 0)
                            INSERT INTO VariableSetting VALUES ('passwordIterations', '2','', 0, 0)
                            INSERT INTO VariableSetting VALUES ('initVector', '@1B2c3D4e5F6g7H8','', 0, 0)
                            INSERT INTO VariableSetting VALUES ('keySize', '256','', 0, 0)
                            INSERT INTO VariableSetting VALUES ('Status', 'Development','', 0, 0)
                    ";
            sqliteCon.Execute(qry, null);
        }
    }
}
