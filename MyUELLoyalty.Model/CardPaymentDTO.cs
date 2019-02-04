using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class CardPaymentDTO
    {
        public int id { get; set; }
        public string transactionId { get; set; }
        public string PaymentId { get; set; }
        public int subscriptionId { get; set; }
        public double totalamount { get; set; }
        //public double netAmount { get; set; }
        //public double feeamount { get; set; }
        public string customerId { get; set; }
        public DateTime createdDate { get; set; }
        public int tenantId { get; set; }
        public string currencyType { get; set; }
        public string statementdescriptor { get; set; }
        public string description { get; set; }
        public string transactionstatus { get; set; }
        public string cardId { get; set; }
        public string carddetailid { get; set; }
    }
}
