using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories; //non-Generic
        private readonly StoreContext _context;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }
        

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if( _repositories == null )
                _repositories = new Hashtable(); //if null->give it initial value

            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return (IGenericRepository<TEntity>)_repositories[type]; //explicit casting
        }
    }
}
