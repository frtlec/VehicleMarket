using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleMarket.Shared.MessageBrokerDto
{
    public class AdvertVisitEvent
    {
        public required int AdvertId { get; set; }
        public required string IpAddress { get; set; }
        public required DateTime VisitDate { get; set; }
    }
}
