using System;
using System.Threading.Tasks;
using S3.Common.RabbitMq;
using S3.Common.Mvc;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using S3.ApiGateway.Schools.Commands;

namespace S3.Api.Controllers
{
    public class SchoolsController : BaseController
    {
        public SchoolsController(IBusPublisher busPublisher, ITracer tracer) : base(busPublisher, tracer)
        {
        }

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] BrowseSchoolsQuery query)
        //    => Collection(await _ordersService.BrowseAsync(query.Bind(q => q.CustomerId, UserId)));

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //    => Single(IsAdmin ? await _ordersService.GetAsync(id) : await _ordersService.GetAsync(UserId, id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateSchoolCommand command)
            => await SendAsync(command.BindId(c => c.Id).Bind(c => c.CustomerId, UserId),
                resourceId: command.Id, resource: "orders");

        //[HttpPost("{id}/approve")]
        //[AdminAuth]
        //public async Task<IActionResult> Complete(Guid id, ApproveOrder command)
        //    => await SendAsync(command.Bind(c => c.Id, id),
        //        resourceId: command.Id, resource: "orders");

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //    => await SendAsync(new CancelOrder(id, UserId));
    }
}