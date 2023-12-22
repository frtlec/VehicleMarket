using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Domain.Core;

namespace VehicleMarket.Services.Advert.Domain.AggregateModels.AdvertVisitsModels
{
    public class AdvertVisits : EntityBase, IAggregateRoot
    {
        public int AdvertId { get; private set; }
        public string IpAddress { get; private set; }
        public DateTime VisitDate { get; private set; }
        public AdvertVisits()
        {
        }
        public AdvertVisits(int advertId,string ipaddress,DateTime visitDate)
        {
            this.AdvertId = advertId;
            this.IpAddress = ipaddress;
            this.VisitDate = visitDate;
        }

    }
}
