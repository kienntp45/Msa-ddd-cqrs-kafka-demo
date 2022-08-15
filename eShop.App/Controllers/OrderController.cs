using eShop.App.Application.Command;
using eShop.App.Application.Queries;
using eShop.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShop.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderQueries _orderQueries;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IMediator mediator,IOrderQueries orderQueriesm,ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _orderQueries = orderQueriesm;
            _logger = logger;
        }
        [HttpGet("GetById/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<Order> GetOrderAsync([FromRoute]int id)
        {
            return await _orderQueries.GetOrderAsync(id);
        }
        [HttpPost("Post")]
        public async Task<IActionResult> AddOrder([FromBody]CreateOrderCommand createOrderCommand)
        {
            return Ok(await _mediator.Send(createOrderCommand));
        }

    }
}
