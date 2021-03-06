﻿using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IVariablesRepo : IBaseTRepo<variables, string>
    {

        IList<variables> GetVariableByType(string type);
        variables GetNegeri(string v, string negeri);
    }
    public class VariablesRepo : DefaultRepo<variables, string>, IVariablesRepo
    {
        public override int Create(variables ent)
        {
            int i = 0;
            string qry = @"INSERT INTO variables (code,	value,	type,	parent) VALUES (@code,	@value,	@type,	@parent) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public override int Edit(variables ent)
        {
            int i = 0;
            string qry = @"UPDATE variables SET code=@code,	value=@value,	type=@type,	parent=@parent WHERE code=@code";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public override int Delete(string ID)
        {
            int i = 0;
            string qry = @"DELETE from variables  WHERE code=@code";
            i = sqliteCon.Execute(qry, new { code = ID });
            return i;
        }

        public override IList<variables> GetAll()
        {
            string qry = @"SELECT * FROM variables";
            IList<variables> lstEnt = sqliteCon.Query<variables>(qry, null).ToList();
            return lstEnt;
        }

        public override variables GetBy(string ID)
        {
            string qry = @"SELECT * FROM variables WHERE code=@code";
            variables ent = sqliteCon.Query<variables>(qry, new { code = ID }).FirstOrDefault();
            return ent;
        }

        public override IList<variables> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, variables whareClause = null)
        {
            throw new NotImplementedException();
        }

        public IList<variables> GetVariableByType(string type)
        {
            string qry = @"SELECT * FROM variables WHERE type COLLATE NOCASE =@type ";
            IList<variables> lstEnt = sqliteCon.Query<variables>(qry, new { type }).ToList();
            return lstEnt;
        }

        public variables GetNegeri(string type, string code)
        {
            string qry = @"SELECT * FROM variables WHERE code=@code and type collate nocase = @type";
            variables ent = sqliteCon.Query<variables>(qry, new { code, type }).FirstOrDefault();
            return ent;
        }
    }
}
