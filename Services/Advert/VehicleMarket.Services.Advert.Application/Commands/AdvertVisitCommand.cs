using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Shared.Dtos;

namespace VehicleMarket.Services.Advert.Application.Commands
{
    public class AdvertVisitCommand : IRequest<Response<NoContent>>
    {
        public int AdvertId { get; set; }
    }
}
