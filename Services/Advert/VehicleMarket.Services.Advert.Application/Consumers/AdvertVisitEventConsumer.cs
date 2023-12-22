using MassTransit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Shared.Dtos;
using VehicleMarket.Shared.MessageBrokerDto;

namespace VehicleMarket.Services.Advert.Application.Consumers
{
    public class AdvertVisitEventConsumer : IConsumer<AdvertVisitEvent>
    {
        private readonly IAdvertVisitsRepository _advertVisitsRepository;
        public AdvertVisitEventConsumer(IAdvertVisitsRepository advertVisitsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _advertVisitsRepository = advertVisitsRepository;
        }
        public async Task Consume(ConsumeContext<AdvertVisitEvent> context)
        {
            await _advertVisitsRepository.Add(new Domain.AggregateModels.AdvertVisitsModels.AdvertVisits(context.Message.AdvertId, context.Message?.IpAddress.ToString(), visitDate: context.Message.VisitDate));
        }
    }
}
