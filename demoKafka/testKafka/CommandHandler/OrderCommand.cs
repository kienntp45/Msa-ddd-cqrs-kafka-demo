using MediatR;

namespace testKafka.CommandHandler
{
    public class OrderCommand : IRequest<OrderHandler>
    {
        public int MyProperty { get; set; }
    }
}
