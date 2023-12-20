using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.Core;

namespace VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels
{
    public class TownModel : ValueObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public TownModel(int townId, string townName)
        {
            this.Id = townId;
            this.Name = townName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id; 
            yield return Name;
        }
    }
}
