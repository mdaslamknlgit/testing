using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class MessageConversationDTO
    {
        public int Id { get; set; }
        public string Toid { get; set; }
        public string Fromid { get; set; }
        public string Message { get; set; }
        public string ConversationDate { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string Status { get; set; }
        public string MsgFrom { get; set; }
        public string Lastmsgtime { get; set; }
        public int loopcount { get; set; }
        public DateTime Createddate { get; set; }

        public string Result { get; set; }
        public int EmailListId { get; set; }

    }
}
