using CsvHelper;
using Dapper;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Services.Advert.Infrastructure.Data.Model;
using VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper.Constants;

namespace VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper
{
    public interface IAdvertRelationDatabaseBuilder
    {
        public void Run();
    }
    public class AdvertRelationDatabaseBuilder : IAdvertRelationDatabaseBuilder
    {
        private readonly IDbConnection _conn;
        private readonly IAdvertRepository _advertRepository;

        public AdvertRelationDatabaseBuilder(IDbConnection conn, IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
            _conn = conn;
        }
        public void Run()
        {
            BuildAdvertTable();
        }
        public void BuildAdvertTable()
        {
            _conn.Open();
            string hasTableQuery = $"SELECT EXISTS ( SELECT 1 FROM information_schema.tables  WHERE table_schema = 'public' AND table_name = @TableName);";
            bool hasTable = _conn.QueryFirst<bool>(hasTableQuery, new { TableName = TableNames.Adverts });
            if (hasTable == false)
            {
                string createCommandRaw = "" +
                     "CREATE TABLE " + TableNames.Adverts + "(" +
                     "Id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY," +
                     "MemberId       INTEGER  NOT NULL  ," +
                     "CityId         INTEGER  NOT NULL  ," +
                     "CityName       VARCHAR(14) NOT NULL  ," +
                     "TownId         INTEGER  NOT NULL  ," +
                     "TownName       VARCHAR(13) NOT NULL  ," +
                     "ModelId        INTEGER  NOT NULL  ," +
                     "ModelName      VARCHAR(59) NOT NULL  ," +
                     "Year           INTEGER  NOT NULL  ," +
                     "Price          INTEGER  NOT NULL  ," +
                     "Title          VARCHAR(250) UNIQUE NOT NULL  ," +
                     "Date           VARCHAR(23) NOT NULL  ," +
                     "CategoryId     INTEGER  NOT NULL  ," +
                     "CategoryName   VARCHAR(88) NOT NULL  ," +
                     "KM             INTEGER  NOT NULL  ," +
                     "Color          VARCHAR(15)  ," +
                     "Gear           VARCHAR(13) NOT NULL  ," +
                     "Fuel           VARCHAR(12) NOT NULL  ," +
                     "FirstPhoto     VARCHAR(135) NOT NULL  ," +
                     "SecondPhoto    VARCHAR(135) NOT NULL  ," +
                     "UserInfo       VARCHAR(19) NOT NULL  ," +
                     "UserPhone      VARCHAR(10)   ," +
                     "Text           TEXT);";
                int ct = _conn.Execute(createCommandRaw);
            }
            _conn.Close();
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string libraryFolder = Path.GetDirectoryName(assemblyLocation);
            string csvFileName = "Adverts.csv";

            string csvFilePath = Path.Combine(libraryFolder, "Data", csvFileName);
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<AdvertsCsvModel>().Select(f=>new AdvertModel(f.memberId,f.year,f.price,f.title,f.date,f.km,f.color,f.gear,f.fuel,f.firstPhoto,f.secondPhoto,f.userInfo,f.userPhone,f.text,new CategoryModel(f.categoryId,f.category),new TownModel(f.townId,f.TownName),new Model(f.modelId,f.modelName),new City(f.cityId))).ToList();

                try
                {

                    _advertRepository.BulkInsert(records);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }



        }

    }
}
