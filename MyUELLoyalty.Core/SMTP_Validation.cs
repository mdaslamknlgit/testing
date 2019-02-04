using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using MyUELLoyalty.Core.Services;
using MyUELLoyalty.Model;

//using GemBox.Email;
//using GemBox.Email.Security;
//using GemBox.Email.Smtp;

namespace MyUELLoyalty.Core
{
    public class SMTP_Validation
    {

        #region SMTP Validations
        public ResultReponse SMTPValidate(string server, int port, string login, string password, string security, string authentication, string Subject, string Message)
        {

            ResultReponse MyResultReponse = new ResultReponse();
            ResultReponse TempResultReponse = new ResultReponse();

            SendEmailCore MySendEmailCore = new SendEmailCore();

            string ReturnStr = "";
            int ResponseCode = 0;
            string MessageOk = "";
            try
            {
                TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
                string CRLF = "\r\n";
                byte[] dataBuffer;
                string ResponseString;
                NetworkStream netStream = tClient.GetStream();
                StreamReader reader = new StreamReader(netStream);
                ResponseString = reader.ReadLine();
                /* Perform HELO to SMTP Server and get Response */
                dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                dataBuffer = BytesFromString("MAIL FROM:<mdaslamknl@gmail.com>" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* Read Response of the RCPT TO Message to know from google if it exist or not */
                dataBuffer = BytesFromString("RCPT TO:<" + login + ">" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                ResponseCode = GetResponseCode(ResponseString);
                if (ResponseCode == 550)
                {
                    ReturnStr = "Mai Address Does not Exist !\n\n";
                    //ReturnStr += string.Format("<B><font color='red'>Original Error from Smtp Server :{0}</font></b>", ResponseString);

                    MyResultReponse.Status = "NOTEXISTS";
                    MyResultReponse.StatusCode = "NOTEXISTS";

                    MyResultReponse.Data = ReturnStr;
                    MyResultReponse.Message = ReturnStr;
                }
                else if (ResponseCode == 250)
                {
                    MessageOk = "OK";
                }
                /* QUITE CONNECTION */
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();
                MyResultReponse.Status = "SUCCESS";
                if (MyResultReponse.Status == "SUCCESS")
                {
                    TempResultReponse = MySendEmailCore.SendTestEmail(server, port, login, login, password, Subject, Message);
                    if (TempResultReponse.Status == "SUCCESS")
                    {
                        MyResultReponse.Status = "SUCCESS";
                        MyResultReponse.StatusCode = "SUCCESS";

                        MyResultReponse.Message = "Message Sent";
                    }
                    else
                    {
                        MyResultReponse.Status = "ERROR";
                        MyResultReponse.StatusCode = "ERROR";
                        MyResultReponse.Data = TempResultReponse.Data;
                        //MyResultReponse.Message = "Email Validated password musth be wrong";

                        MyResultReponse.Message = "Email Account Password Not Validated";
                    }
                }
                //*************************************************************************************************************************************
                //Send Email
                //*************************************************************************************************************************************
                // If using Professional version, put your serial key below.
                //ComponentInfo.SetLicense("FREE-LIMITED-KEY");

                //using (GemBox.Email.Smtp.SmtpClient smtp = new GemBox.Email.Smtp.SmtpClient(server))
                //{
                //    // Connect to mail server
                //    smtp.Connect();
                //    Console.WriteLine("Connected.");

                //    // Authenticate with specified username and password
                //    // (SmtpClient will use strongest possible authentication mechanism)
                //    smtp.Authenticate(login, password);
                //    Console.WriteLine("Authenticated.");

                //    // Create new message
                //    GemBox.Email.MailMessage message = new GemBox.Email.MailMessage(login,login);
                //    message.Subject = Subject;
                //    //message.BodyText = Message;
                //    message.BodyHtml = Message;

                //    Console.WriteLine("Sending message...");

                //    // Send message
                //    smtp.SendMessage(message);

                //    MyResultReponse.Status = "SUCCESS";
                //    MyResultReponse.StatusCode = "SUCCESS";

                //    MyResultReponse.Message = "Message Sent";


                //}
                //*************************************************************************************************************************************

                //using (var client = new SmtpClient())
                //{
                //    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                //    client.Authenticate("username", "password");

                //    foreach (var message in messages)
                //    {
                //        client.Send(message);
                //    }

                //    client.Disconnect(true);
                //}

                ReturnStr = "SUCCESS";
                return MyResultReponse;
            }
            catch (Exception exp)
            {
                ReturnStr = "ERROR";
                MyResultReponse.Status = "ERROR";
                MyResultReponse.StatusCode = "NOTEXISTS";

                MyResultReponse.Data = ReturnStr;
                MyResultReponse.Message = exp.ToString();

            }
            return MyResultReponse;
        }
        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        #endregion
        //public string ValidateSMTP(string server, int port, string login, string password, string security, string authentication, string Subject, string Message)
        //{
        //    try
        //    {
        //        // Add this line to your application to set your trial key 
        //        // before working with Rebex classes.
        //        Rebex.Licensing.Key = "==Ahhg+h0fr/eGfMIoA6TNBeTXcaA65mprrGwV2FS5M1Lg==";

        //        Rebex.Mail.MailMessage mail = new Rebex.Mail.MailMessage();
        //        mail.From = login;
        //        mail.To = login;
        //        //mail.Subject = "Setup Commander Service Portal e-mail configuration test";
        //        //mail.BodyHtml = "<br /><br />\nThis is an e-mail sent by the Setup Commander Service Portal to test your SMTP configuration.<br /><br /><br />";
        //        //mail.BodyHtml += "Regards,<br /><br /><br /><br />\n";
        //        //mail.BodyHtml += "Setup Commander";

        //        mail.Subject = Subject;
        //        mail.BodyHtml = Message;

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
        //            return "SUCCESS";
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.Message.Contains("(535"))
        //            {
        //                //Helpers.ErrorLog(typeof(SMTP_Validation).ToString(), "ValidateSMTP()", "invalid SMTP credentials have been used");
        //                return "invalid SMTP credentials have been used";
        //            }
        //            else
        //            {
        //                //Helpers.ErrorLog(typeof(SMTP_Validation).ToString(), "ValidateSMTP()", ex.ToString());
        //                //contact administrator
        //                return authentication + ":" + security + ": " + ex.Message + "<br/>please contact administrator.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Helpers.ErrorLog(typeof(SMTP_Validation).ToString(), "ValidateSMTP()", ex.ToString());
        //        return authentication + ":" + security + ": " + ex.Message + "<br/>please contact administrator.";
        //    }
        //}
    }
}