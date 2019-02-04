using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class TenantDTO
    {
        public int ID { get; set; }
        public string TenantName { get; set; }
        public string LinkedInEmail { get; set; }
        public string LinkedPassword { get; set; }
        public string DatabaseName { get; set; }
        public SubscriptionDTO Subscription { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

        public int noOfUsers { set; get; }
        public int creditLimit { set; get; }
    }
}
