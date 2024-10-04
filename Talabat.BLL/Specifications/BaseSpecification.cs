using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria {  get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); //initial value 
        public Expression<Func<T, object>> OrderBy {  get; set; }
        public Expression<Func<T, object>> OrderByDescending {  get; set; }
        public int Take {  get; set; }
        public int Skip {  get; set; }
        public bool IsPaginationEnabled {  get; set; }

        public BaseSpecification(Expression<Func<T, bool>> criteria) //to get values of criteria
        { 
            this.Criteria = criteria;
        }
        
        public BaseSpecification() { } //for ProductWithTypeAndBrandSpecification class to run

        public void AddInclude(Expression<Func<T,object>> Include) //to fill list of includes
        {
            Includes.Add(Include);
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy) //set values of orderBy
        {
            OrderBy = orderBy;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled=true;
            Skip = skip;
            Take = take;
        }


        
    }
}
