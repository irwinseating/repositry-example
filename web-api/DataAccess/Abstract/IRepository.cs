using System;
using System.Linq;

namespace web_api.DataAccess.Abstract
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> Query();
        T GetById(int id);
    }
}