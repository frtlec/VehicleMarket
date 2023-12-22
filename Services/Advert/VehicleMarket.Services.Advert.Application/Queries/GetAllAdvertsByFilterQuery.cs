using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Dtos;
using VehicleMarket.Services.Advert.Domain.SeedWork.Dtos;
using VehicleMarket.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace VehicleMarket.Services.Advert.Application.Queries
{
    public class GetAllAdvertsByFilterQuery : IRequest<Response<GetAllAdvertsByFilterQueryResult>>
    {
        [MaxLength]
        public int? CategoryId { get; set; }
        public decimal? BeginPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public string? Gear { get; set; }
        public string? Fuel { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public List<(string ColumnName, SortDirective Directive)>? Sort { get; set; }
    }
}
