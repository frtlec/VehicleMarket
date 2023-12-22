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
        private  string TableName;
        public DapperBaseRepository(IDbConnection connection, string tableName)
        {
            _connection = connection;
            TableName = tableName;
        }
        private (string Columns,string Parameters) GetProperties(bool ignoreIdentity=true)
        {

            var properties = typeof(T).GetProperties();
            var propertyNames = new List<string>();
            var propertyNamesWithAtChar = new List<string>();

            foreach (var prop in properties)
            {
                if (ignoreIdentity && prop.Name==nameof(EntityBase.Id))
                {
                    continue;
                }
                  
                propertyNames.Add(prop.Name);
                propertyNamesWithAtChar.Add("@" + prop.Name);
            }

            return (string.Join(',', propertyNames), string.Join(',', propertyNamesWithAtChar));
        }
        public async Task Add(T item)
        {
            _connection.Open();
            try
            {
                var getProperties = GetProperties(true);
                string queryRaw = $"INSERT INTO {TableName} ({getProperties.Columns}) values({getProperties.Parameters})";

                await _connection.ExecuteAsync(queryRaw, item);
            }
            catch (Exception ex)
            {

                throw;
            }
            _connection.Close();
        }

        public async Task AddRange(List<T> items)
        {
            _connection.Open();
            string queryRaw = $"INSERT INTO {TableName} ({GetProperties(true).Columns}) values({GetProperties(true).Parameters})";

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
