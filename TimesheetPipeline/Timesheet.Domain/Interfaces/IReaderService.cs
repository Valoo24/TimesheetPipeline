using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Interfaces
{
    public interface IReaderService<TEntity, Tkey> where TEntity : IEntity<Tkey>
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity GetById (Tkey id);
    }
}