using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using MyUELLoyalty.Model;
//using Rebex.Net;

namespace MyUELLoyalty.Core
{
    public class SMTPServices
    {
        //public SMTPResponseDTO ValidateSMTP(string server, int port, string login, string password, string security, string authentication)
        //{
        //    SMTPResponseDTO MySMTPResponseDTO = new SMTPResponseDTO();
        //    try
        //    {
        //        Rebex.Mail.MailMessage mail = new Rebex.Mail.MailMessage();
        //        mail.From = login;
        //        mail.To = login;
        //        mail.Subject = "Setup Commander Service Portal e-mail configuration test";
        //        mail.BodyHtml = "<br /><br />\nThis is an e-mail sent by the Setup Commander Service Portal to test your SMTP configuration.<br /><br /><br />";
        //        //mail.BodyHtml += "Regards,<br /><br /><br /><br />\n";
        //        //mail.BodyHtml += "Setup Commander";

        //        Rebex.Net.SmtpConfiguration smtpconfig = new Rebex.Net.SmtpConfiguration();
                

        //        if (port == 465)
        //        {
        //            smtpconfig.Security = Rebex.Net.SmtpSecurity.Implicit;

        //        }
        //        else
        //        {
        //            if (security == "Secure")
        //            {
        //                smtpconfig.Security = Rebex.Net.SmtpSecurity.Secure;
        //            }
        //            else if (security == "Explicit")
        //            {
        //                smtpconfig.Security = Rebex.Net.SmtpSecurity.Explicit;
        //            }
        //            else if (security == "Implicit")
        //            {
        //                smtpconfig.Security = Rebex.Net.SmtpSecurity.Implicit;
        //            }
        //            else if (security == "Unsecure")
        //            {
        //                smtpconfig.Security = Rebex.Net.SmtpSecurity.Unsecure;
        //            }
        //        }

        //        // smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.Auto;
        //        if (authentication == Rebex.Net.SmtpAuthentication.Auto.ToString())
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.Auto;
        //        }
        //        else if (authentication == Rebex.Net.SmtpAuthentication.DigestMD5.ToString())
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.DigestMD5;
        //        }
        //        else if (authentication == Rebex.Net.SmtpAuthentication.Login.ToString())
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.Login;
        //        }
        //        else if (authentication == Rebex.Net.SmtpAuthentication.CramMD5.ToString())
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.CramMD5;
        //        }
        //        else if (authentication == Rebex.Net.SmtpAuthentication.GssApi.ToString())
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.GssApi;
        //        }
        //        else if (authentication == Rebex.Net.SmtpAuthentication.Plain.ToString())
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.Plain;
        //        }
        //        else
        //        {
        //            smtpconfig.AuthenticationMethod = Rebex.Net.SmtpAuthentication.Ntlm;
        //        }

        //        smtpconfig.DeliveryMethod = Rebex.Net.SmtpDeliveryMethod.Smtp;
        //        smtpconfig.UseDefaultCredentials = false;
        //        smtpconfig.ServerName = server;
        //        smtpconfig.ServerPort = port;

        //        if (security != "Unsecure")
        //        {
        //            smtpconfig.UserName = login;
        //            smtpconfig.Password = password;
        //        }

        //        smtpconfig.From = login;

        //        try
        //        {
        //            Rebex.Net.Smtp.Send(mail, smtpconfig);
        //            MySMTPResponseDTO.Status = "SUCCESS";
        //            MySMTPResponseDTO.Message = "SUCCESS";

        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.Message.Contains("(535"))
        //            {

        //                //Helpers.ErrorLog(typeof(SMTP_Validation).ToString(), "ValidateSMTP()", "invalid SMTP credentials have been used");
        //                //return "invalid SMTP credentials have been used";
        //                MySMTPResponseDTO.Status = "ERROR";
        //                MySMTPResponseDTO.Message = "Invalid SMTP credentials have been used";
        //            }
        //            else
        //            {
        //                //Helpers.ErrorLog(typeof(SMTP_Validation).ToString(), "ValidateSMTP()", ex.ToString());
        //                //return authentication + ":" + security + ": " + ex.Message + "<br/>please contact administrator.";
        //                MySMTPResponseDTO.Status = "ERROR";
        //                MySMTPResponseDTO.Message = authentication + ":" + security + ": " + ex.Message + "<br/>please contact administrator.";

        //            }
        //        }
        //        return MySMTPResponseDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Helpers.ErrorLog(typeof(SMTP_Validation).ToString(), "ValidateSMTP()", ex.ToString());
        //        //return authentication + ":" + security + ": " + ex.Message + "<br/>please contact administrator.";

        //        MySMTPResponseDTO.Status = "ERROR";
        //        MySMTPResponseDTO.Message = authentication + ":" + security + ": " + ex.Message + "<br/>please contact administrator.";
        //    }
        //    return MySMTPResponseDTO;
        //}
    }
}
