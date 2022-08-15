using eShop.Domain.Aggregates;
using MediatR;

namespace eShop.App.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderResponsitory _orderRepository;
        private readonly IMediator _mediator;
      
        public OrderQueries(IOrderResponsitory orderResponsitory,IMediator mediator)
        {
            _orderRepository = orderResponsitory;
            _mediator = mediator;
        }
        public async Task<Order> GetOrderAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }
    }
}
