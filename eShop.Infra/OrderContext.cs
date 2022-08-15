using eShop.Domain.Aggregates;
using eShop.Domain.SeedWork;
using eShop.Infra.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace eShop.Infra
{
    public class OrderContext:DbContext,IUnitOfWork
    {
        private readonly IMediator _mediator;
        public OrderContext(DbContextOptions<OrderContext> options,IMediator mediator):base(options)
        {
            _mediator=mediator ?? throw new ArgumentNullException(nameof(mediator));

        }
        public DbSet<Order> Orders { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        }

    }
    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var optionBuider = new DbContextOptionsBuilder<OrderContext>().UseSqlServer("Data Source=LAPTOP-MTHHT25V\\SQLEXPRESS;Initial Catalog=cqrs;Integrated Security=True");
            return new OrderContext(optionBuider.Options,new NoMediator());
        }
        class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<TResponse>);
            }

            public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<object?>);
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
