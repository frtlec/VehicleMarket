
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels;

namespace VehicleMarket.Services.Advert.Infrastructure.Data.Model
{
    public class AdvertsCsvModel
    {
        public int id { get; set; }
        public int memberId { get; set; }
        public int cityId { get; set; }
        public string CityName { get; set; }
        public int townId { get; set; }
        public string TownName { get; set; }
        public int modelId { get; set; }
        public string modelName { get; set; }
        public int year { get; set; }
        public decimal price { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public int categoryId { get; set; }
        public string category { get; set; }
        public double km { get; set; }
        public string color { get; set; }
        public string gear { get; set; }
        public string fuel { get; set; }
        public string firstPhoto { get; set; }
        public string secondPhoto { get; set; }
        public string userInfo { get; set; }
        public string userPhone { get; set; }
        public string text { get; set; }
    }
}
