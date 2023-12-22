using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Application.Dtos;
using VehicleMarket.Services.Advert.Application.Queries;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;

namespace VehicleMarket.Services.Advert.Application.Helpers.Mappers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdvertModel, GetAllAdvertsByFilterQueryResult.Item>();
            CreateMap<AdvertModel, GetAdvertByIdQueryResult>();



        }
    }
}
