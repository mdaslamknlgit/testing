using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class RegisterFormDTO
    {
        public int RegisterId { get; set; }
        public int userTypeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        
        public int TenantID { get; set; }
        public int RoleID { get; set; }
        public bool IsFirstTime { get; set; }
        public string Company { get; set; }

        public string TenantName { get; set; }
        public int Subscriptionid { get; set; }
        public string ccNo { get; set; }
        public string SecurityCode { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsAgreed { get; set; }








    }
}
