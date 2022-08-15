using eShop.Domain.SeedWork;

namespace eShop.Domain.Aggregates
{
    public interface IOrderResponsitory :IResponsitory<Order>
    {
        Order Add(Order order);

       

        Task<Order> GetByIdAsync(int orderId);
       
    }
}
