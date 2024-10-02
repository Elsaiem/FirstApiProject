using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.RepositoreContract
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
       Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Tkey id);

        Task AddAsync(TEntity entity);


       void Update(TEntity entity); 
       void Delete(TEntity entity);    


    }

}
