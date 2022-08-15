using MediatR;

namespace eShop.App.Application.Command
{
    public class CreateOrderCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public string Addresss { get; set; }
        public CreateOrderCommand(string name, string addresss)
        {
            Name = name;
            Addresss = addresss;
        }
        public CreateOrderCommand()
        {

        }
    }
}
