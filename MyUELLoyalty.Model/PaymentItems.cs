using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class PaymentItems
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int PaymentId { get; set; }
        public int PurchaseUserId { get; set; }
        public int NoOfUsers { get; set; }

    }
}
