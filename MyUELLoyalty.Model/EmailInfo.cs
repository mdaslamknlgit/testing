using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class EmailInfo
    {
        public int emaillistId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }
        public string Industry { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }


        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string Photourl { get; set; }
        public string Profileid { get; set; }
        public int ListId { get; set; }
        public int TypeId { get; set; }
        public string Degree { get; set; }
        public string Connectiontype { get; set; }
        public string CompanyURL { get; set; }

        public string Status { get; set; }

        public DateTime LastUpdated { get; set; }

        public int CampaignListId { get; set; }

        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string SequenceType { get; set; }
        public int CampaignSeqId { get; set; }
    }
}
