using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
  public  class InvoiceDTO
    {
        
        public string Name { get; set; }
        public string ItemName { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Subscription { get; set; }
        public int NoOfDays { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int NetDays { get; set; }

        public string Description { get; set; }
    }
}
