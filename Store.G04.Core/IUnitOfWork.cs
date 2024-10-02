using Store.G04.Core.Entities;
using Store.G04.Core.RepositoreContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core
{
    public interface IUnitOfWork
    {
      Task<int>  CompleteAsync();

        //create Repository<T>and Returns
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where  TEntity:BaseEntity<TKey>;





    }
}
