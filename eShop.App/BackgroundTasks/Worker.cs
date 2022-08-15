 
using Confluent.Kafka;
using eShop.App.Application.Command;
using eShop.Domain.Aggregates;
using MediatR;
using Newtonsoft.Json;

namespace eShop.App.BackgroundTasks
{
    public class Worker 
    {
        private readonly ProducerConfig _config;
        private readonly IMediator _mediator;
        private readonly string _topic = "test";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";
        public Worker(ProducerConfig config,IMediator mediator)
        {
            _config = config;
            _config.BootstrapServers = "localhost:9092";
            _mediator = mediator;
        }

        public async void Producer(Order input)
        {
            string serializedEmployee = JsonConvert.SerializeObject(input);
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = serializedEmployee });
                producer.Flush(TimeSpan.FromSeconds(5));
            }
        }
        public Order Comsumer()
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(_topic);
                    var cancelToken = new CancellationTokenSource();
                    //cancelToken.Cancel();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);
                        var orderRequest = JsonConvert.DeserializeObject<Order>(consumer.Message.Value);
                        var OrderCommand = new CreateOrderCommand(orderRequest.Name,orderRequest.Addresss);
                        _mediator.Send(OrderCommand);
                        return orderRequest;
                    }
                        //consumerBuilder.Commit();
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return null;


        }



    }
   
}
