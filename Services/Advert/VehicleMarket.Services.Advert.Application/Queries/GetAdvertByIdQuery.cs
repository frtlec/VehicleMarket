
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Dtos;
using VehicleMarket.Shared.Dtos;

namespace VehicleMarket.Services.Advert.Application.Queries
{
    public class GetAdvertByIdQuery : IRequest<Response<GetAdvertByIdQueryResult>>
    {
        public int Id { get; set; }
    }
}
