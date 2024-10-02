using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities;
using Store.G04.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository
{
    public class SpecifcationEvaluator<TEntity,Tkey>where TEntity : BaseEntity<Tkey>
    {
     public static IQueryable<TEntity>  GeneratQuery(IQueryable<TEntity>inputQuery,ISpecification<TEntity,Tkey> spec)
        {
            var query = inputQuery;
            if(spec.Criteria is not null)
            {
                query=query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null)
            {
              query=  query.OrderBy(spec.OrderBy);
            }
            
            if(spec.OrderByDescinding is not null)
            {
                query= query.OrderByDescending(spec.OrderByDescinding);
            }
            if (spec.IsPaginationEnabled)
            {
                query= query.Skip(spec.Skip).Take(spec.Take);


            }


       
            query=spec.Include.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));




            return query;
        }
        




        




    }
}
