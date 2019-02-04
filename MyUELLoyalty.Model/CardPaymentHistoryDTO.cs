using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class CardPaymentHistoryDTO
    {
        public int id { get; set; }
        public string cardType { get; set; }
        public string carddetailid { get; set; }
        public string customerdetailid { get; set; }
        public int unattemptedCharges { get; set; }
        public string cardCountry { get; set; }
        public string cardBrand { get; set; }
        public int LastDigits { get; set; }
        public string currencytype { get; set; }
        public string transactionId { get; set; }
        public int subscriptionId { get; set; }
        public string subscriptionName { get; set; }
        public string subscriptionDays { get; set; }
        public string amount { get; set; }
        public string subscriptionType { get; set; }
        public double totalamount { get; set; }
        public string transactionstatus { get; set; }
        public string PaymentId { get; set; }
        public DateTime createdDate { get; set; }
        public int tenantId { get; set; }
        public string tenantName { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string PostalCode { get; set; }

        public string ItemName { get; set; }

    }
}
