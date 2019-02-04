using MyUELLoyalty.Core.Interface;
using MyUELLoyalty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Core.Services
{
    public class SendEmailCore : ISendEmailCore
    {
        //public ResultReponse SendEmail(string toAddress, string fromAddress, string fromPassword, string Subject, 
        //    UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO)
        //{
        //    bool status = false; string body = null;
        //    ResultReponse MyResultReponse = new ResultReponse();
        //    try
        //    {
        //        if (cardPaymentDTO != null)
        //        {
        //            body = "Welcome to BoundHound" + "\n";
        //            body += "Hi " + userDTO.UserName + "," + "\n";
        //            body += "Username: " + userDTO.UserEmail + "\n";
        //            body += "Password: " + userDTO.UserPassword + "\n";
        //            body += "SubScription: \n" + subscriptionDTO.SubscriptionType + "\n";
        //            body += "TransactionID: \n" + cardPaymentDTO.transactionId + "\n";
        //            body += "PaymentID: \n" + cardPaymentDTO.PaymentId + "\n";
        //        }
        //        else
        //        {
        //            body = "Welcome to BoundHound" + "\n";
        //            body += "Hi " + userDTO.UserName + "," + "\n";
        //            body += "Username: " + userDTO.UserEmail + "\n";
        //            body += "Password: " + userDTO.UserPassword + "\n";
        //            body += "SubScription: \n" + subscriptionDTO.SubscriptionType + "\n";
        //        }
        //        var smtp = new System.Net.Mail.SmtpClient();
        //        {
        //            smtp.Host = "smtp.gmail.com";
        //            smtp.Port = 587;
        //            smtp.EnableSsl = true;
        //            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //            smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
        //            smtp.Timeout = 20000;
        //        }
        //        smtp.Send(fromAddress, toAddress, Subject, body);
        //        status = true;
        //        MyResultReponse.Status = "SUCCESS";
        //        MyResultReponse.StatusCode = "SENDMAIL";
        //        MyResultReponse.Data = "Email sent successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        MyResultReponse.Status = "ERROR";
        //        MyResultReponse.StatusCode = "EMAILERROR";
        //        MyResultReponse.Data = ex.ToString();
        //    }
        //    return MyResultReponse;
        //}


        public ResultReponse SendEmail(string toAddress, string hostName, int port, string fromAddress, string fromPassword, string Subject, string Body,
        UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO)
        {
            bool status = false; string body = null;
            string[] name = userDTO.UserName.Split();
            ResultReponse MyResultReponse = new ResultReponse();
            try
            {
                body = Body;
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    smtp.Host = hostName;
                    smtp.Port = Convert.ToInt32(port);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = Subject;
                mailMessage.Body = body;
                smtp.Send(mailMessage);


                //smtp.Send(fromAddress, toAddress, Subject, body);
                status = true;
                MyResultReponse.Status = "SUCCESS";
                MyResultReponse.StatusCode = "SENDMAIL";
                MyResultReponse.Data = "Email sent successfully";
            }
            catch (Exception ex)
            {
                MyResultReponse.Status = "ERROR";
                MyResultReponse.StatusCode = "EMAILERROR";
                MyResultReponse.Data = ex.ToString();
            }
            return MyResultReponse;
        }

        public ResultReponse SendTestEmail(string server, int port, string toAddress, string fromAddress, string fromPassword, string Subject, string Body)
        {
            bool status = false;
            string body = null;
            int mPort = 0;

            ResultReponse MyResultReponse = new ResultReponse();
            try
            {
                body = Body;
                if (port > 0)
                {
                    mPort = Convert.ToInt32(port);
                }
                else
                {
                    mPort = 587;
                }
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    smtp.Host = server;
                    smtp.Port = mPort;

                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = Subject;
                mailMessage.Body = body;
                smtp.Send(mailMessage);


                //smtp.Send(fromAddress, toAddress, Subject, body);
                status = true;
                MyResultReponse.Status = "SUCCESS";
                MyResultReponse.StatusCode = "SENDMAIL";
                MyResultReponse.Data = "Email sent successfully";
            }
            catch (Exception ex)
            {
                MyResultReponse.Status = "ERROR";
                MyResultReponse.StatusCode = "EMAILERROR";
                MyResultReponse.Data = ex.ToString();
            }
            return MyResultReponse;
        }

        public bool SendEmailWithAttachment(string toAddress, string hostName, int port, string fromAddress, string fromPassword, string Subject, UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO)
        {
            bool status = false; string body = null;
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(toAddress);
                mailMessage.From = new MailAddress(fromAddress);
                mailMessage.Subject = Subject;
                if (cardPaymentDTO != null)
                {
                    body = "Welcome to BoundHound" + "\n";
                    body += "Hi " + userDTO.UserName + "," + "\n";
                    body += "Username: " + userDTO.UserEmail + "\n";
                    body += "Password: " + userDTO.UserPassword + "\n";
                    body += "SubScription: \n" + subscriptionDTO.SubscriptionType + "\n";
                    body += "TransactionID: \n" + cardPaymentDTO.transactionId + "\n";
                    body += "PaymentID: \n" + cardPaymentDTO.PaymentId + "\n";
                }
                else
                {
                    body = "Welcome to BoundHound" + "\n";
                    body += "Hi " + userDTO.UserName + "," + "\n";
                    body += "Username: " + userDTO.UserEmail + "\n";
                    body += "Password: " + userDTO.UserPassword + "\n";
                    body += "SubScription: \n" + subscriptionDTO.SubscriptionType + "\n";
                }

                mailMessage.Body = body;
                Attachment attachment = new System.Net.Mail.Attachment(@"C:\Users\anuj.joshi\Desktop\Test1.txt");
                mailMessage.Attachments.Add(attachment);


                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                smtp.Send(mailMessage);
                status = true;
            }
            catch (Exception ex)
            { }
            return status;
        }

        public bool BulkEmails(List<string> toAddresslist, string fromAddress, string fromPassword, string Subject, UserDTO userDTO, SubscriptionDTO subscriptionDTO, CardPaymentDTO cardPaymentDTO)
        {
            bool status = false; string body = null;
            try
            {
                if (cardPaymentDTO != null)
                {
                    body = "Welcome to BoundHound" + "\n";
                    body += "Hi " + userDTO.UserName + "," + "\n";
                    body += "Username: " + userDTO.UserEmail + "\n";
                    body += "Password: " + userDTO.UserPassword + "\n";
                    body += "SubScription: \n" + subscriptionDTO.SubscriptionType + "\n";
                    body += "TransactionID: \n" + cardPaymentDTO.transactionId + "\n";
                    body += "PaymentID: \n" + cardPaymentDTO.PaymentId + "\n";
                }
                else
                {
                    body = "Welcome to BoundHound" + "\n";
                    body += "Hi " + userDTO.UserName + "," + "\n";
                    body += "Username: " + userDTO.UserEmail + "\n";
                    body += "Password: " + userDTO.UserPassword + "\n";
                    body += "SubScription: \n" + subscriptionDTO.SubscriptionType + "\n";
                }
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                foreach (string toAddress in toAddresslist)
                {
                    smtp.Send(fromAddress, toAddress, Subject, body);
                }
                status = true;
            }
            catch (Exception ex)
            { }
            return status;
        }

        public bool LinkedInConnectionSendEmail(UserInfo userInfo, string Subject, string MessageBody, int Totalconnection)
        {
            bool status = false; string body = null;
            string[] name = userInfo.UserName.Split();
            try
            {

                //body += "Hi " + name[0] + "," + "\n" + "\n";
                //body += "Your Total Connections" + "\n" + "\n";
                //body += "Total Connection: " + Totalconnection + "\n";

                body = MessageBody;

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(userInfo.FromUserEmail, userInfo.FromPassword);

                    smtp.Timeout = 20000;
                }

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(userInfo.userEmail);
                mailMessage.From = new MailAddress(userInfo.FromUserEmail);
                mailMessage.Subject = Subject;
                //mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;

                smtp.Send(mailMessage);
                //smtp.Send(userInfo.FromUserEmail, userInfo.userEmail, Subject, body);

                status = true;

            }
            catch (Exception ex)
            { }

            return status;
        }

        public bool SendEmail_forgetPassword(UserInfo userInfo, string EncryptEmail)
        {
            bool status = false; string body = null;
            string Subject = "Bound Hound Reset Password";

            try
            {
                string URL = userInfo.UI_PageURL + "/#/renewpass/renew/" + EncryptEmail;
                body += "Below is the link for reset the password" + "\n" + "\n";
                body += URL;
                //  body += "http://localhost:4200/#/renewpass";
                //  body += "Total Connection: " + Totalconnection + "\n";
                //body += "Estimated Time: " + userDTO.UserPassword + "\n";
                //body += "Subscription Type: " + subscriptionDTO.SubscriptionType + " For " + subscriptionDTO.SubscriptionDays + " days" + "\n";

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(userInfo.FromUserEmail, userInfo.FromPassword);
                    smtp.Timeout = 20000;
                }
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(userInfo.userEmail);
                mailMessage.From = new MailAddress(userInfo.FromUserEmail);
                mailMessage.Subject = Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                smtp.Send(mailMessage);
                status = true;

            }
            catch (Exception ex)
            {
                status = false;
                throw;
            }

            return status;
        }



        public ResultReponse MailSend(SMTPSettings sMTPSettings, string ToAddress, string Subject, string Body)
        {
            bool status = false; string body = null;

            ResultReponse MyResultReponse = new ResultReponse();
            string FromAddress = "";

            try
            {
                FromAddress = string.Format("{0} <{1}>", sMTPSettings.DisplayName, sMTPSettings.UserName);
                body = Body;


                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = sMTPSettings.HostName;
                    smtp.Port = sMTPSettings.Port;

                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(sMTPSettings.UserName, sMTPSettings.UserPassword);
                    smtp.Timeout = 20000;
                }
                MailMessage mailMessage = new MailMessage(FromAddress, ToAddress);

                //mailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = Subject;
                mailMessage.Body = body;
                smtp.Send(mailMessage);

                //System.Net.NetworkCredential aCred = new System.Net.NetworkCredential(sMTPSettings.UserName, sMTPSettings.UserPassword);
                //SmtpClient smtp1 = new SmtpClient(sMTPSettings.HostName, 465);
                //smtp1.EnableSsl = true;
                //smtp1.UseDefaultCredentials = false;
                //smtp1.Send(mailMessage);

                //smtp.Send(FromAddress, ToAddress, Subject, body);
                status = true;
                MyResultReponse.Status = "SUCCESS";
                MyResultReponse.StatusCode = "SENDMAIL";
                MyResultReponse.Data = "Email sent successfully";
            }
            catch (Exception ex)
            {
                MyResultReponse.Status = "ERROR";
                MyResultReponse.StatusCode = "EMAILERROR";
                MyResultReponse.Data = ex.ToString();
            }
            return MyResultReponse;
        }
    }
}
