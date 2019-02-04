using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class LoginDTO
    {
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; set; }
        public string LinkedinEmail { get; set; }
        public string GoogleEmail { get; set; }
    }
}
