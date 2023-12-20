using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.Core;

namespace VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels
{
    public class CategoryModel : ValueObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public CategoryModel(int categoryId, string categoryName)
        {
            Id = categoryId;
            Name = categoryName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }

    }
}
