using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class SMTPSettings
    {
        public string   SMTPHostname { get; set; }
        public int      SMTPPortnumber { get; set; }
        public string   SMTPAuthUsername { get; set; }
        public string   SMTPAuthPassword { get; set; }
        public string   SMTPSecurity { get; set; }
        public string   SMTPAuthentication { get; set; }

        public int      Id { get; set; }
        public string   HostName { get; set; }
        public string DisplayName { get; set; }
        public int Port { get; set; }
        public string   Security { get; set; }
        public string   Authentication { get; set; }
        public string   UserName { get; set; }
        public string   UserPassword { get; set; }
        public int TenantId { get; set; }
        public DateTime LastUpdate { get; set; }


    }
}
