using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;
using VehicleMarket.Services.Advert.Domain.Core;

namespace VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper
{
    public abstract class DapperBaseRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly IDbConnection _connection;
        private readonly string TableName;
        public DapperBaseRepository(IDbConnection connection,string tableName)
        {
            _connection = connection;
            TableName= tableName;
        }
        public async Task Add(T item)
        {
            _connection.Open();
            string queryRaw = $"INSERT INTO {TableName} (MemberId,CityId,CityName,TownId,TownName,ModelId,ModelName,Year,Price,Title,Date,CategoryId,Category,KM,Color,Gear,Fuel,FirstPhoto,SecondPhoto,UserInfo,UserPhone,Text) values(@MemberId,@CityId,@CityName,@TownId,@TownName,@ModelId,@ModelName,@Year,@Price,@Title,@Date,@CategoryId,@Category,@KM,@Color,@Gear,@Fuel,@FirstPhoto,@SecondPhoto,@UserInfo,@UserPhone,@Text)";

            await _connection.ExecuteAsync(queryRaw, item);
            _connection.Close();
        }

        public async Task AddRange(List<T> items)
        {
            _connection.Open();
            string queryRaw = $"INSERT INTO {TableName} (MemberId,CityId,CityName,TownId,TownName,ModelId,ModelName,Year,Price,Title,Date,CategoryId,Category,KM,Color,Gear,Fuel,FirstPhoto,SecondPhoto,UserInfo,UserPhone,Text) values(@MemberId,@CityId,@CityName,@TownId,@TownName,@ModelId,@ModelName,@Year,@Price,@Title,@Date,@CategoryId,@Category,@KM,@Color,@Gear,@Fuel,@FirstPhoto,@SecondPhoto,@UserInfo,@UserPhone,@Text)";

            await _connection.ExecuteAsync(queryRaw, items);
            _connection.Close();
        }

        public Task Delete(int key)
        {
            throw new NotImplementedException();
        }


        public async Task<T> Get(int key)
        {
            _connection.Open();
            var result = await _connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = key });
            _connection.Close();
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            _connection.Open();
            var result = await _connection.QueryAsync<T>($"SELECT * FROM {TableName}");
            _connection.Close();
            return result.ToList();
        }

        public Task Update(T item)
        {
            throw new NotImplementedException();
        }

    }
}
