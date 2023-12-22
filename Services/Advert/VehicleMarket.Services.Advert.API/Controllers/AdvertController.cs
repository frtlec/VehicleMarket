using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using VehicleMarket.Services.Advert.Application.Commands;
using VehicleMarket.Services.Advert.Application.Dtos;
using VehicleMarket.Services.Advert.Application.Queries;
using VehicleMarket.Shared.ControllerBases;

namespace VehicleMarket.Services.Advert.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdvertController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public AdvertController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, type:typeof(GetAdvertByIdQueryResult))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest,type:typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError,type:typeof(string))]
        public async Task<IActionResult> Get([FromQuery] GetAdvertByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(GetAllAdvertsByFilterQueryResult))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(string))]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllAdvertsByFilterQuery query)
        {
            var response = await _mediator.Send(query);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(string))]
        public async Task<IActionResult> Visit([FromBody] AdvertVisitCommand query)
        {
            var response = await _mediator.Send(query);
            return CreateActionResultInstance(response);
        }


    }
}
