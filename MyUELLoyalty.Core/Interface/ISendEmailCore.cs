using MyUELLoyalty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Core.Interface
{
    public interface ISendEmailCore
    {
        ResultReponse SendEmail(string toAddress, string hostName, int port, string fromAddress, string fromPassword, string Subject, string Body, UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO);
        bool SendEmailWithAttachment(string toAddress, string hostName, int port, string fromAddress, string fromPassword, string Subject, UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO);
        bool BulkEmails(List<string> toAddress, string fromAddress, string fromPassword, string Subject, UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO);
        bool LinkedInConnectionSendEmail(UserInfo userInfo, string Subject, string MessageBody, int Totalconnection);
    }
}
