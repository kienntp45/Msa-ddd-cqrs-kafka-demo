using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.SeedWork
{
    public interface IResponsitory<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
