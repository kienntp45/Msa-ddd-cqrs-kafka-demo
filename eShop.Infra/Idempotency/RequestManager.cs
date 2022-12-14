using eShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Infra.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly OrderContext _context;
        public RequestManager(OrderContext context)
        {
            _context=context;
        }
        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var exists = await ExistAsync(id);
            var request = exists ?
           throw new OrderingDomainException($"Request with {id} already exists") :
           new ClientRequest()
           {
               Id = id,
               Name = typeof(T).Name,
               Time = DateTime.UtcNow
           };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            var result = await _context.FindAsync<ClientRequest>(id);
            return result != null;
        }
    }
}
