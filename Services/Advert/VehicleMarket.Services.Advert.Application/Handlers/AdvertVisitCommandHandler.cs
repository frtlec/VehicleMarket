using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Commands;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Shared.Dtos;
using VehicleMarket.Shared.MessageBrokerDto;

namespace VehicleMarket.Services.Advert.Application.Handlers
{
    public class AdvertVisitCommandHandler : IRequestHandler<AdvertVisitCommand, Response<NoContent>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MassTransit.IPublishEndpoint _publishEndpoint;
        public AdvertVisitCommandHandler(IHttpContextAccessor httpContextAccessor, MassTransit.IPublishEndpoint publishEndpoint)
        {
            _httpContextAccessor = httpContextAccessor;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Response<NoContent>> Handle(AdvertVisitCommand request, CancellationToken cancellationToken)
        {
            IPAddress remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;


            await _publishEndpoint.Publish<AdvertVisitEvent>(
                   new AdvertVisitEvent
                   {
                       AdvertId = request.AdvertId,
                       IpAddress =remoteIpAddress.ToString(),
                       VisitDate = DateTime.Now,
                   });
            return Response<NoContent>.Success();
        }
    }
}
