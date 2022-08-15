using eShop.Domain.Aggregates;

namespace eShop.App.Application.Queries
{
    public interface IOrderQueries
    {
        Task<Order> GetOrderAsync(int id);
    }
}
