using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testkafka1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Interface Class;
        public ValuesController(Interface @class)
        {
            Class = @class;
        }

        [HttpPost("post")]
        public async void Producer([FromBody]Order input)
        {
            Class.Producer(input);
        }
        [HttpGet("get")]
        public Order Comsumer()
        {
            return Class.Comsumer();
        }
    }
}
