using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertVisitsModels;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper.Constants;

namespace VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper
{
    public class AdvertVisitsRepository: DapperBaseRepository<AdvertVisits> ,IAdvertVisitsRepository
    {
        private readonly IDbConnection _connection;
        private const string TableName = TableNames.AdvertVitis;

        public AdvertVisitsRepository(IDbConnection connection) : base(connection, TableName)
        {
            _connection = connection;
        }
       
    }
}
