using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class CardDetailsDTO
    {
        public int Id { get; set; }
        public string ccNo { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string cardType { get; set; }
        public string carddetailid { get; set; }
        public string customerdetailid { get; set; }
        public int unattemptedCharges { get; set; }
        public string cardCountry { get; set; }
        public string cardBrand { get; set; }
        public int LastDigits { get; set; }
        public int subscriptionId { get; set; }        
        public string currencytype { get; set; }
        public DateTime createddate { get; set; }
        public int tenantId { get; set; }

    }
}
