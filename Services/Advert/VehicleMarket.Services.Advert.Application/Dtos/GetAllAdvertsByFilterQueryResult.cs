using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleMarket.Services.Advert.Application.Dtos
{
    public class GetAllAdvertsByFilterQueryResult
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public List<Item> Adverts { get; set; } = new();

        public class Item
        {
            public int Id { get; set; }
            public string ModelName { get; set; }
            public string Category { get; set; }
            public int Year { get; set; }
            public decimal Price { get; set; }
            public string Title { get; set; }
            public DateTime Date { get; set; }
            public string KM { get; set; }
            public string Color { get; set; }
            public string Gear { get; set; }
            public string FirstPhoto { get; set; }
        }
    }
}
