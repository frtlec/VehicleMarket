using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;
using VehicleMarket.Services.Advert.Domain.SeedWork.Dtos;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper.Constants;

namespace VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper
{
    public class AdvertRepository : DapperBaseRepository<AdvertModel>, IAdvertRepository
    {
        private readonly IDbConnection _connection;
        private const string TableName = TableNames.Advert;

        public AdvertRepository(IDbConnection connection) : base(connection, TableName)
        {
            _connection = connection;
        }
   
        public async Task<(List<AdvertModel> Items, int Total)> GetAllByFilter(AdvertGetAllByFilterInput filter)
        {
            _connection.Open();

            string queryRaw = $"SELECT {TableName}.*," +
                    $"{TableNames.AdvertCategories}.id as catid,{TableNames.AdvertCategories}.name, " +
                    $"{TableNames.VehicleModels}.id as modelid,{TableNames.VehicleModels}.name " +
                $"FROM {TableName}  " +
                     $"inner join {TableNames.AdvertCategories}  on {TableName}.CategoryId={TableNames.AdvertCategories}.Id " +
                     $"inner join {TableNames.VehicleModels}  on {TableName}.ModelId={TableNames.VehicleModels}.Id " +
                     $"WHERE 1=1 ";
            string countQueryRaw = $"SELECT COUNT(id) FROM {TableName}  WHERE 1=1 ";
            string condition = string.Empty;
            if (string.IsNullOrEmpty(filter.Gear) == false)
                condition += "and Gear=@Gear ";
            if (string.IsNullOrEmpty(filter.Fuel) == false)
                condition += "and Fuel=@Fuel ";
            if (filter.BeginPrice.HasValue)
                condition += "and Price>=@BeginPrice ";
            if (filter.EndPrice.HasValue)
                condition += "and Price<=@EndPrice ";
            if (filter.CategoryId.HasValue)
                condition += "and CategoryId=@CategoryId ";

            queryRaw += condition;
            countQueryRaw += condition;
            int countResult = await _connection.QueryFirstAsync<int>(countQueryRaw, new { filter.CategoryId, filter.BeginPrice, filter.EndPrice, filter.Gear, filter.Fuel });

            if (filter.Sort != null && filter.Sort.Count>0)
            {
                queryRaw += "ORDER BY " + string.Join(',', filter.Sort.Select(f => $"{f.ColumnName} {f.Directive}").ToArray());
            }
            else
            {
                queryRaw += $"ORDER BY {TableName}.id desc ";
            }

            queryRaw += filter.Take.HasValue == false ? string.Empty : $" OFFSET {filter.Skip} ROWS FETCH NEXT {filter.Take} ROWS ONLY ";

            var adverts = await _connection.QueryAsync<AdvertModel, CategoryModel,Model, AdvertModel>(queryRaw, (advert, category,model) =>
            {
                advert.SetCategory(category.Id, category.Name);
                advert.SetModel(model.Id, model.Name);
                return advert;
            }, splitOn: $"catid,modelid", param: new { filter.CategoryId, filter.BeginPrice, filter.EndPrice, filter.Gear, filter.Fuel });


            _connection.Close();
            return (adverts.ToList(), countResult);
        }

        public async Task<AdvertModel> GetById(int id)
        {
            _connection.Open();

            string queryRaw = $"SELECT {TableName}.*," +
                $"{TableNames.AdvertCategories}.id as catid,{TableNames.AdvertCategories}.name as catname," +
                $"{TableNames.Towns}.id as townId,{TableNames.Towns}.name as townName, " +
                $"{TableNames.VehicleModels}.id as modelId,{TableNames.VehicleModels}.name as modelName " +
                $" FROM {TableName}  " +
                    $"inner join {TableNames.AdvertCategories} on {TableName}.CategoryId={TableNames.AdvertCategories}.Id " +
                    $"inner join {TableNames.Towns} on {TableName}.TownId={TableNames.Towns}.Id " +
                    $"inner join {TableNames.VehicleModels} on {TableName}.ModelId={TableNames.VehicleModels}.Id ";

            queryRaw += $"where {TableName}.id=@id LIMIT 1";


            var result = (await _connection.QueryAsync<AdvertModel, CategoryModel, TownModel, Model, AdvertModel>(queryRaw, (advert, category, town, model) =>
            {
                advert.SetCategory(category.Id, category.Name);
                advert.SetTown(category.Id, category.Name);
                advert.SetModel(category.Id, category.Name);
                // Diğer eşleştirmeleri yapabilirsiniz
                return advert;
            }, splitOn: $"catid, townId, modelId", param: new { Id = id }));
            _connection.Close();
            return result.FirstOrDefault();
        }



    }
}
