using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableApps.Repo
{
    public interface IBaseTRepo<T, TID>
    {
        int Create(T ent);
        int Edit(T ent);
        int Delete(TID ID);
        IList<T> GetAll();
        T GetBy(TID ID);
        IList<T> PagedList(int page, int rows, string sidx, string sodx, out int rowCount, T oWhereClause);
    }
}
