using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class AdvertRepository : DapperBaseRepository<AdvertModel>,IAdvertRepository
    {
        private readonly IDbConnection _connection;
        private const string TableName = TableNames.Adverts;

        public AdvertRepository(IDbConnection connection):base(connection,TableName) 
        {
            _connection = connection;
        }
        public async Task<List<AdvertModel>> GetAllByFilter(AdvertGetAllByFilterInput filter)
        {
            _connection.Open();

            string queryRaw = $"SELECT * FROM {TableName} WHERE 1=1";

            if (string.IsNullOrEmpty(filter.Gear)==false)
                queryRaw += "and Gear=@Gear ";
            if (string.IsNullOrEmpty(filter.Fuel) == false)
                queryRaw += "and Fuel=@Fuel ";
            if (filter.Price.HasValue)
                queryRaw += "and Price=@Price";
            if (filter.CategoryId.HasValue)
                queryRaw += "and CategoryId=@CategoryId";

            queryRaw += SortingQueryRaw(filter.Sort);
            queryRaw += GetPaginitionQueryRaw(filter.Page);

            var result = await _connection.QueryAsync<AdvertModel>(queryRaw, new { filter.CategoryId, filter.Price, filter.Gear, filter.Fuel});

            _connection.Close();
            return result.ToList();
        }


        private string SortingQueryRaw(List<(string ColumnName, SortDirective Directive)> columns)
        {
            return "ORDER BY" + string.Join(',', columns.Select(f => $"{f.ColumnName} {f.Directive}").ToArray());
        }
        private string GetPaginitionQueryRaw(int? page)
        {
            int _page = page.HasValue ? page.Value : 0;
            int pageSize = 10;

            int skip = pageSize * _page;
            int take = pageSize;
            return $"OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
        }

        public async Task BulkInsert(List<AdvertModel> items)
        {
            _connection.Open();

            var grouped = items.GroupBy(f => f.Title);
            var fi = grouped.Where(f => f.Count() > 1);
            var insertItems = grouped.Select(f => f.First()).ToList();
            string mergeSqlQuery = $"INSERT INTO public.{TableName} (memberid,cityid,cityname,townid,townname,modelid,modelname,year,price,title,date,categoryid,categoryname,km,color,gear,fuel,firstphoto,secondphoto,userinfo,userphone,text) values(@MemberId,@CityId,@CityName,@TownId,@TownName,@ModelId,@ModelName,@Year,@Price,@Title,@Date,@CategoryId,@CategoryName,@KM,@Color,@Gear,@Fuel,@FirstPhoto,@SecondPhoto,@UserInfo,@UserPhone,@Text) ON CONFLICT (Title) DO NOTHING;";
            await _connection.ExecuteAsync(mergeSqlQuery, insertItems);
            _connection.Close();
        }
    }
}
