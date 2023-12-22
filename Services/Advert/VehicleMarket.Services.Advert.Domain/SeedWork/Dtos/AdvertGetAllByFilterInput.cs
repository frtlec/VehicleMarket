using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleMarket.Services.Advert.Domain.SeedWork.Dtos
{
    public class AdvertGetAllByFilterInput
    {
        public int? CategoryId { get; set; }
        public decimal? BeginPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public string? Gear { get; set; }
        public string? Fuel { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }

        public List<(string ColumnName, SortDirective Directive)> Sort { get; set; }

    }
    public enum SortDirective
    {
        ASC,
        DESC,
    }
}
