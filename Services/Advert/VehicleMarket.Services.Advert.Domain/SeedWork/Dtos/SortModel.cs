using System.Text.Json.Serialization;

namespace VehicleMarket.Services.Advert.Domain.SeedWork.Dtos
{
    public class SortModel
    {
        public string ColumnName { get; set; }
        public SortDirective Directive { get; set; }
        public enum SortDirective
        {
            ASC,
            DESC,
        }
    }
}
