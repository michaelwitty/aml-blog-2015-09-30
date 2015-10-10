using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aml.SinglePageApplicationMVC.Lib
{
    public interface IRepository<TEntity, in TKey>
    {
        TEntity Get(TKey id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Save();
        IEnumerable<TEntity> FindAll();
    }
}
