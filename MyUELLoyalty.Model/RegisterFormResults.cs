using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class RegisterFormResults
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public string PaymentId { get; set; }
        public string TransactionId { get; set; }

        public SubscriptionDTO  Subscription { get; set; }
        public CardPaymentDTO CardPayment { get; set; }
        public UserDTO User { get; set; }

        public RegisterFormDTO RegisterForm { get; set; }
    }
}
