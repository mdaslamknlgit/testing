using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class SetupProcessDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<string> SuccessTable { get; set; }
        public List<string> ErrorTable { get; set; }

        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
