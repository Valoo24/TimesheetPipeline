using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Interfaces
{
    public interface IWriterService<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public TKey Add(TEntity entity);
        public TKey Update(TEntity entity);
    }
}