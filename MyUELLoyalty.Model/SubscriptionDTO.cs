using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class SubscriptionDTO : EntityBase
    {
        public int SubscriptionID { get; set; }
        public string SubscriptionName { get; set; }
        public int SubscriptionDays { get; set; }
        public string SubscriptionType { get; set; }
        public decimal Amount { get; set; }
        public int noOfUser { set; get; }
        public int creditLimit { set; get; }
    }
}
