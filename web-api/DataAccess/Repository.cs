using System;
using System.Linq;
using web_api.DataAccess.Abstract;
using web_api.Models;

namespace web_api.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;

        public Repository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }


        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
    }
}