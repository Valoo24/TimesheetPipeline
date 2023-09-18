using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity GetById(TKey id);
    }
}