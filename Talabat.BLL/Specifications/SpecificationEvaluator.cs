using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            //build Query
            var query = inputQuery; //_context.Set<Product>();
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria); //p => p.Id == 1

            if(spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            //before this line
            //context.Set<Product>()
            //add includes to query
            query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
            //after this line
            //context.Set<Product>().Include(p=>p.productBrand).Include(p=>p.productType)
            return query;
        }
    }
}
