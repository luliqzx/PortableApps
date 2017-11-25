﻿using PortableApps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace PortableApps.Repo
{
    public interface IParlimenRepo : IBaseTRepo<parlimen, int>
    {

    }
    public class ParlimenRepo : CommonRepo, IParlimenRepo
    {


        public int Create(parlimen ent)
        {
            int i = 0;
            string qry = @"INSERT INTO parlimen (id	,negeri,	kawasan) VALUES (@id	,@negeri,	@kawasan) ";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Edit(parlimen ent)
        {
            int i = 0;
            string qry = @"UPDATE parlimen SET id=@id,	negeri=@negeri,	kawasan=@kawasan WHERE id=@id";
            i = sqliteCon.Execute(qry, ent);
            return i;
        }

        public int Delete(int ID)
        {
            int i = 0;
            string qry = @"DELETE from parlimen  WHERE id=@id";
            i = sqliteCon.Execute(qry, new { id = ID });
            return i;
        }

        public IList<parlimen> GetAll()
        {
            string qry = @"SELECT * FROM parlimen";
            IList<parlimen> lstEnt = sqliteCon.Query<parlimen>(qry, null).ToList();
            return lstEnt;
        }

        public parlimen GetBy(int ID)
        {
            string qry = @"SELECT * FROM parlimen WHERE id=@id";
            parlimen ent = sqliteCon.Query<parlimen>(qry, new { id = ID }).FirstOrDefault();
            return ent;
        }


        public IList<parlimen> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, parlimen whareClause = null)
        {
            throw new NotImplementedException();
        }
    }
}
