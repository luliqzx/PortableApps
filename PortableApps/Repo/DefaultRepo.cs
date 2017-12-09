using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PortableApps.Repo
{
    public class DefaultRepo<T, TID> : CommonRepo, IBaseTRepo<T, TID>
    {
        public virtual int Create(T ent)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(TID ID)
        {
            throw new NotImplementedException();
        }

        public virtual int Edit(T ent)
        {
            throw new NotImplementedException();
        }

        public virtual IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T GetBy(TID ID)
        {
            throw new NotImplementedException();
        }

        public virtual void OpenMySQLDB()
        {
            if (mysqlCon.State != ConnectionState.Open)
            {
                mysqlCon.Open();
            }
        }

        public virtual void CloseMySQLDB()
        {
            if (mysqlCon.State != ConnectionState.Closed)
            {
                mysqlCon.Close();
            }
        }

        public virtual IList<T> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, T oWhereClause)
        {
            throw new NotImplementedException();
        }

        public virtual IDbTransaction MySQLBeginTransaction(IsolationLevel isoLev = IsolationLevel.ReadCommitted)
        {
            return mysqlCon.BeginTransaction(isoLev);
        }
    }
}
