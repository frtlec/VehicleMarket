using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.Core;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;
using VehicleMarket.Services.Advert.Domain.SeedWork.Dtos;

namespace VehicleMarket.Services.Advert.Domain.SeedWork.Repository
{
    public interface IAdvertRepository:IRepository<AdvertModel>
    {
        Task<List<AdvertModel>> GetAllByFilter(AdvertGetAllByFilterInput filter);
        Task BulkInsert(List<AdvertModel> items);
    }
}
