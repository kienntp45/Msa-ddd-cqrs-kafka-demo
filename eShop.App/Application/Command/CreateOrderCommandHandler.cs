using eShop.Domain.Aggregates;
using MediatR;

namespace eShop.App.Application.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderResponsitory _orderRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        public CreateOrderCommandHandler(IOrderResponsitory orderRepository, IMediator mediator, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Order - Order: {@Order}", request);
            var order = new Order(request.Name,request.Addresss);
            _orderRepository.Add(order);
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

}
