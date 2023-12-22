using VehicleMarket.Services.Advert.Domain.Core;

namespace VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertModels
{
    public class Model : ValueObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Model()
        {
                
        }
        public Model(int modelId, string modelName)
        {
            this.Id = modelId;
            this.Name = modelName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }
}
