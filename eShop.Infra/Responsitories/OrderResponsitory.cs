using eShop.Domain.Aggregates;
using eShop.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Infra.Responsitories
{
    public class OrderResponsitory : IOrderResponsitory
    {
        private readonly OrderContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public OrderResponsitory(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public Order Add(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }


        public async Task<Order> GetByIdAsync(int orderId)
        {
          var order = await _context.Orders.Include(p=>p.Addresss).FirstOrDefaultAsync(p=>p.Id ==orderId);
            if (order ==null)
            {
                order = _context.Orders.Local.FirstOrDefault(p => p.Id == orderId);
            }
            return order;
        }
    }
}
