using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context=context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            #region Method to get ProductBrandId,ProductTypeId
/*            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>)await context.Set<Product>().Include(p => p.ProductType).ToListAsync();
            }*/
            #endregion
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
            => await context.Set<T>().FindAsync(id);

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
            => await context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            =>context.Set<T>().Update(entity);

        public void Delete(T entity)
            => context.Set<T>().Remove(entity);

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }


    }
}
