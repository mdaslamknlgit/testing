using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class ResultMessagesReponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public IList<MessageConversationDTO> MyMessages { get; set; }
    }
}
