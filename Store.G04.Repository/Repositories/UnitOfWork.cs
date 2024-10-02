using Store.G04.Core;
using Store.G04.Core.Entities;
using Store.G04.Core.RepositoreContract;
using Store.G04.Repository.Data.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly StoreDbContext _Context;
        private Hashtable _repository;
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _Context = storeDbContext;
            _repository =new Hashtable();
        }

      

        public async Task<int> CompleteAsync()=>await _Context.SaveChangesAsync();
        

        public  IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            if (!_repository.ContainsKey(typeof(TEntity).Name))
            {

                var repository = new GenericRepository<TEntity, TKey>(_Context);
                _repository.Add(typeof(TEntity).Name, repository);
            }

            return  _repository[typeof(TEntity).Name] as IGenericRepository<TEntity, TKey>;

        }
    }
}
