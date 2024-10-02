using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities;
using Store.G04.Core.RepositoreContract;
using Store.G04.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public StoreDbContext _Context { get; }

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _Context = storeDbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>) await  _Context.Products.Include(P=>P.Brand).Include(P=>P.Type).ToListAsync();
            }

               return await _Context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetAsync(Tkey id)

        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _Context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P=>P.Id==id as int?)as TEntity;
            }

            return   await  _Context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
          await _Context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity) 
        {
             _Context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _Context.Set<TEntity>().Remove(entity);
        }

      

       

       
    }
}
