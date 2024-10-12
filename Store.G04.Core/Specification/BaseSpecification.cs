﻿using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specification
{
    public class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get ; set ; } = null;
        public List<Expression<Func<TEntity, object>>> Include{ get ; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDescinding { get; set; } = null;
        public int Skip { get ; set; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification(Expression<Func<TEntity,bool>>expression) {
            Criteria = expression;
      


        }
        public BaseSpecification() {

          
        
        }
        public void AddOrderBy(Expression<Func<TEntity,object>> expression)
        {
            OrderBy = expression;
        } public void AddOrderByDescinding(Expression<Func<TEntity,object>> expression)
        {
            OrderByDescinding = expression;
        }
        public void ApplyPagination(int skip, int take) {
        
         IsPaginationEnabled = true;
        
         Skip=skip;
         Take=take;
        
        
        }
      
    
    
    }
}
