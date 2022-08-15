using Confluent.Kafka;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var conf = new ProducerConfig { BootstrapServers = "localhost:9092" };

                Action<DeliveryReport<Null, string>> handler = r =>
                    Console.WriteLine(!r.Error.IsError
                        ? $"Delivered message to {r.TopicPartitionOffset}"
                        : $"Delivery Error: {r.Error.Reason}");

                using (var p = new ProducerBuilder<Null, string>(conf).Build())
                {
                    p.Produce("my-topic", new Message<Null, string> { Value = "123324" }, handler);
                    // wait for up to 10 seconds for any inflight messages to be delivered.
                    p.Flush(TimeSpan.FromSeconds(10));
                }
            }
        }
    }

    public class Order{
        public int Id { get; set; }
        public string NameMyProperty { get; set; }
    }
}