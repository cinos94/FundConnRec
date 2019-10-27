using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundConnRec.API.Repositories.Interfaces
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> Get(long id);
        Task Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        Task Delete(TEntity entity);
    }
}
