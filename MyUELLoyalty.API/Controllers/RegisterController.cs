using MyUELLoyalty.API.Base;
using MyUELLoyalty.Model;
using MyURLLoyalty.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyUELLoyalty.API.Helpers;
using MyUELLoyalty.TemplateEngine;
using System.IO;
using System.Web;

namespace MyUELLoyalty.API.Controllers
{
    public class RegisterController : AnanomyousBase
    {
    //Added on 04-02-2018
    //private readonly IEmailsBusiness m_IEmailsBusiness;
    //private readonly IRegisterBusiness m_RegisterBusiness;
    //private readonly ICardDetailsBusiness m_ICardDetailsBusiness;
    //private readonly IPaymentDetailsBusiness m_paymentDetailsBusiness;
    //private readonly ISetupProcessBusiness m_ISetupProcessBusiness;
    //private readonly IPurchaseUsersBusiness m_IPurchaseUsersBusiness;
    private readonly UserInfo MyUserInfo = new UserInfo();

        private readonly IRegisterBusiness m_IRegisterBusiness;

        //private readonly IDashBoardManager _dashboardManager;

        public RegisterController(IRegisterBusiness mIregisterBusiness, 
            IRegisterBusiness mRegisterBusiness
            //ICardDetailsBusiness mICardDetailsBusiness, IPaymentDetailsBusiness mpaymentDetailsBusiness,
            //IEmailsBusiness memailsBusiness, ISetupProcessBusiness mSetupProcessBusiness,
            //IPurchaseUsersBusiness mpurchaseUsersBusiness
            )
        {
            //MyUserInfo = SetUserInfo();
            m_IRegisterBusiness = mIregisterBusiness;
            //m_RegisterBusiness = mRegisterBusiness;
            //m_ICardDetailsBusiness = mICardDetailsBusiness;
            //m_paymentDetailsBusiness = mpaymentDetailsBusiness;
            //m_IEmailsBusiness = memailsBusiness;
            //m_ISetupProcessBusiness = mSetupProcessBusiness;
            //m_IPurchaseUsersBusiness = mpurchaseUsersBusiness;
        }

        [HttpPost, Route("api/RegisterForm")]
        public IHttpActionResult RegisterForm(RegisterFormDTO registerForm)
        {
            PaymentItems MyPaymentItems = null;
            int NoOfUsers = 0;
            int PurchaseItemsId = 0;
            ResultReponse MyResultResponse = new ResultReponse();
            UserDTO UserDTOInfo = new UserDTO();
            SetupProcessDTO SetupProcessDTOInfo = new SetupProcessDTO();

            CardPaymentDTO CardPaymentDTOInfo = null;
            SubscriptionDTO m_subscriptionDTO;
            //string APIKey = "sk_test_MCGUn4fT1YzFQzGcRa9HiC3v";

            string APIKey = "sk_test_ffjSM0I2xFEg7I18xCdxb9EJ";

            RegisterFormResults MyRegisterFormResults = new RegisterFormResults();
            string MessageBody = "Welcome to MyUEL..";
            string TemplateBody = "";
            string Subject = "Message";
            string SubscriptionName = "";

            string ExpMonth = "";
            string ExpYear = "";
            ResultReponse MyResultReponse = new ResultReponse();
            try
            {

                //Get Stripe API Key
                //APIKey = "sk_test_ffjSM0I2xFEg7I18xCdxb9EJ";
                //m_subscriptionDTO = m_IRegisterBusiness.GetSubscriptionId(registerForm.Subscriptionid);

                //if (m_subscriptionDTO != null)
                //{
                //    NoOfUsers = m_subscriptionDTO.noOfUser;



                //    MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveUserDetails", "Started " + DateTime.Now.ToString());
                //    UserDTOInfo = m_IRegisterBusiness.creatingUsers(registerForm.FirstName, registerForm.LastName, registerForm.UserEmail,
                //        registerForm.Password, registerForm.Company, registerForm.Subscriptionid, m_subscriptionDTO);
                //    MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveUserDetails", "Done " + DateTime.Now.ToString());

                //    SubscriptionName = string.Format("{0}  {1}   {2} ", m_subscriptionDTO.SubscriptionName, m_subscriptionDTO.SubscriptionDays, m_subscriptionDTO.SubscriptionType);

                //    if (UserDTOInfo != null)
                //    {
                //        MyUserInfo.TenantId = UserDTOInfo.TenantId;
                //        MyUserInfo.UserId = UserDTOInfo.Id;
                //        MyUserInfo.UserName = UserDTOInfo.UserName;
                //        MyUserInfo.SubscriptionId = registerForm.Subscriptionid;
                //        MyUserInfo.SubscriptionName = m_subscriptionDTO.SubscriptionName;
                //        MyUserInfo.SubscriptionType = m_subscriptionDTO.SubscriptionType;
                //        MyRegisterFormResults.StatusCode = "USERREGISTER";
                //        MyRegisterFormResults.Status = "User Registration Done";
                //        MyRegisterFormResults.Message = "User Registration Done";
                //        MyUELHelpers.Info(typeof(RegisterController).ToString(), "Creating Table", "Started " + DateTime.Now.ToString());
                //        SetupProcessDTOInfo = m_ISetupProcessBusiness.CreateTable("boundhound" + MyUserInfo.TenantId, MyUserInfo);
                //        if (SetupProcessDTOInfo.Status == "SUCCESS")
                //        {
                //            MyUELHelpers.Info(typeof(RegisterController).ToString(), "Table Created Successfully", "End " + DateTime.Now.ToString());
                //            MyUserInfo.databasename = "MyUELLoyalty" + MyUserInfo.TenantId;
                //        }
                //        else
                //        {
                //            MyUELHelpers.Info(typeof(RegisterController).ToString(), "Creating Table Error", string.Format("Error {0}\n ", SetupProcessDTOInfo.Message) + DateTime.Now.ToString());
                //        }

                //        if (m_subscriptionDTO.SubscriptionType == "Paid")
                //        {


                //            registerForm.TotalAmount = m_subscriptionDTO.Amount;
                //            //create a card
                //            ExpMonth = registerForm.ExpMonth.Split('/')[0];
                //            ExpYear = registerForm.ExpMonth.Split('/')[1];

                //            MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveCardDetails", "Started " + DateTime.Now.ToString());
                //            CardDetailsDTO cardDetailsDTOInfo = null;
                //            cardDetailsDTOInfo = m_ICardDetailsBusiness.savecardDetails(registerForm.ccNo, registerForm.SecurityCode, ExpMonth, ExpYear, registerForm.UserEmail, APIKey, MyUserInfo);
                //            MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveCardDetails", "Ended " + DateTime.Now.ToString());
                //            if (cardDetailsDTOInfo != null)
                //            {
                //                //payment
                //                MyUELHelpers.Info(typeof(RegisterController).ToString(), "SavePaymentDetails", "Started " + DateTime.Now.ToString());
                //                CardPaymentDTOInfo = m_paymentDetailsBusiness.PaymentDetails(Convert.ToString(cardDetailsDTOInfo.Id), registerForm.TotalAmount, APIKey, MyUserInfo);
                //                MyUELHelpers.Info(typeof(RegisterController).ToString(), "SavePaymentDetails", "Done " + DateTime.Now.ToString());
                //                MyResultResponse.Status = CardPaymentDTOInfo.transactionstatus;
                //                MyResultResponse.Message = "Paid Subscription";

                //                MyRegisterFormResults.StatusCode = "PAYMENTDONESUCCESS";
                //                MyRegisterFormResults.Status = "Payment done subscription created";
                //                MyRegisterFormResults.Message = "Payment done subscription created";

                //                MyRegisterFormResults.PaymentId = CardPaymentDTOInfo.PaymentId;
                //                MyRegisterFormResults.TransactionId = CardPaymentDTOInfo.transactionId;
                //                //****************************************************************************************************************************************************************
                //                // Save In Payment Items
                //                //****************************************************************************************************************************************************************
                //                MyPaymentItems = new PaymentItems();

                //                MyPaymentItems.PaymentId = CardPaymentDTOInfo.id;
                //                MyPaymentItems.ItemName = "Subscription Charges";
                //                MyPaymentItems.PurchaseAmount = registerForm.TotalAmount;
                //                MyPaymentItems.NoOfUsers = NoOfUsers;
                //                PurchaseItemsId = m_IPurchaseUsersBusiness.CreatePaymentItems(MyPaymentItems, MyUserInfo);

                //                //****************************************************************************************************************************************************************

                //                //Send this Details to send the mail
                //                //MyRegisterFormResults.Subscription = m_subscriptionDTO;
                //                //MyRegisterFormResults.CardPayment = CardPaymentDTOInfo;
                //                //MyRegisterFormResults.User = UserDTOInfo;

                //                //TODO Send the email to user

                //            }
                //        }

                //        //Send Email After Register
                //        Subject = "User Registration Confirmation...";

                //        MyUserInfo.UserName = UserDTOInfo.UserName;
                //        MyUserInfo.Email = UserDTOInfo.UserEmail;
                //        MyUserInfo.Password = UserDTOInfo.UserPassword;

                //        MyUserInfo.SubscriptionName = SubscriptionName;

                //        //Template Parse
                //        TemplateParsers MyTemplateParser = new TemplateParsers(MyUserInfo);

                //        //Get After register html template from the Templates/AfterRegister.html
                //        var filePath = HttpContext.Current.Server.MapPath("~/Templates/AfterRegister.html");

                //        string html = File.ReadAllText(filePath);

                //        TemplateBody = MyTemplateParser.ParseAfterRegister(html, MyUserInfo);

                //        MessageBody = TemplateBody;

                //        //GET SMTP Settings
                //        SMTPSettings MyUELLoyaltySMTPSettings = new SMTPSettings();

                //        UtilityBusiness MyUtilityBusiness = new UtilityBusiness();
                //        BoundHoundSMTPSettings = MyUtilityBusiness.GetBoundHoundSMPTSettings(MyUserInfo);

                //        if (BoundHoundSMTPSettings != null)
                //        {
                //            if (BoundHoundSMTPSettings.Id > 0)
                //            {
                //                //string FromAddress = string.Format("{0} <{1}>", BoundHoundSMTPSettings.DisplayName, BoundHoundSMTPSettings.UserName);

                //                MyResultReponse = m_IEmailsBusiness.SendEmailAfterRegister(registerForm.UserEmail, BoundHoundSMTPSettings.HostName, BoundHoundSMTPSettings.Port, BoundHoundSMTPSettings.UserName,
                //                BoundHoundSMTPSettings.UserPassword,
                //                Subject, MessageBody, UserDTOInfo, m_subscriptionDTO, CardPaymentDTOInfo);

                //                //MyResultReponse = m_IEmailsBusiness.SendEmailAfterRegister(registerForm.UserEmail, MyUserInfo.FromUserEmail,
                //                //        MyUserInfo.FromPassword,
                //                //        Subject, MessageBody, UserDTOInfo, m_subscriptionDTO, CardPaymentDTOInfo);

                //                if (MyResultReponse.Status == "SUCCESS")
                //                {
                //                    MyRegisterFormResults.StatusCode = "USERREGISTER";
                //                    MyRegisterFormResults.Status = "User Registration Done";
                //                    MyRegisterFormResults.Message = "User Registration Done";
                //                    MyRegisterFormResults.User = UserDTOInfo;

                //                }
                //                else
                //                {
                //                    MyRegisterFormResults.StatusCode = "PAYMENTDONESUCCESS";
                //                    MyRegisterFormResults.Status = "Payment done subscription created";
                //                    MyRegisterFormResults.Message = "Payment done subscription created - Email error ";
                //                    MyRegisterFormResults.User = UserDTOInfo;
                //                }
                //            }
                //        }



                //        //SetupProcessDTOInfo = m_ISetupProcessBusiness.CreateTable("boundhound" + MyUserInfo.TenantId, MyUserInfo);

                //    }

                //}

                //else
                //{
                //    MyRegisterFormResults.Message = "InValid Subscription Id ";
                //    MyRegisterFormResults.User = UserDTOInfo;
                //}
                return Json(MyRegisterFormResults);
            }
            catch (Exception Ex)
            {
                MyUELHelpers.ErrorLog(typeof(RegisterController).ToString(), "RegisterForm", Ex.ToString());
            }

            return Json(MyRegisterFormResults);
        }
        //[HttpPost, Route("api/RegisterForm")]
        //public IHttpActionResult RegisterFormOld(RegisterFormDTO registerForm)
        //{
        //    PaymentItems MyPaymentItems = null;
        //    int NoOfUsers = 0;
        //    int PurchaseItemsId = 0;
        //    ResultReponse MyResultResponse = new ResultReponse();
        //    UserDTO UserDTOInfo = new UserDTO();
        //    SetupProcessDTO SetupProcessDTOInfo = new SetupProcessDTO();

        //    CardPaymentDTO CardPaymentDTOInfo = null;
        //    SubscriptionDTO m_subscriptionDTO;
        //    //string APIKey = "sk_test_MCGUn4fT1YzFQzGcRa9HiC3v";

        //    string APIKey = "sk_test_ffjSM0I2xFEg7I18xCdxb9EJ";

        //    RegisterFormResults MyRegisterFormResults = new RegisterFormResults();
        //    string MessageBody = "Welcome to BoundHound..";
        //    string TemplateBody = "";
        //    string Subject = "Message";
        //    string SubscriptionName = "";

        //    string ExpMonth = "";
        //    string ExpYear = "";
        //    ResultReponse MyResultReponse = new ResultReponse();
        //    try
        //    {

        //        //Get Stripe API Key
        //        APIKey = "sk_test_ffjSM0I2xFEg7I18xCdxb9EJ";
        //        m_subscriptionDTO = m_IRegisterBusiness.GetSubscriptionId(registerForm.Subscriptionid);

        //        if (m_subscriptionDTO != null)
        //        {
        //            NoOfUsers = m_subscriptionDTO.noOfUser;



        //            MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveUserDetails", "Started " + DateTime.Now.ToString());
        //            UserDTOInfo = m_IRegisterBusiness.creatingUsers(registerForm.FirstName, registerForm.LastName, registerForm.UserEmail,
        //                registerForm.Password, registerForm.Company, registerForm.Subscriptionid, m_subscriptionDTO);
        //            MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveUserDetails", "Done " + DateTime.Now.ToString());

        //            SubscriptionName = string.Format("{0}  {1}   {2} ", m_subscriptionDTO.SubscriptionName, m_subscriptionDTO.SubscriptionDays, m_subscriptionDTO.SubscriptionType);

        //            if (UserDTOInfo != null)
        //            {
        //                MyUserInfo.TenantId = UserDTOInfo.TenantId;
        //                MyUserInfo.UserId = UserDTOInfo.Id;
        //                MyUserInfo.UserName = UserDTOInfo.UserName;
        //                MyUserInfo.SubscriptionId = registerForm.Subscriptionid;
        //                MyUserInfo.SubscriptionName = m_subscriptionDTO.SubscriptionName;
        //                MyUserInfo.SubscriptionType = m_subscriptionDTO.SubscriptionType;
        //                MyRegisterFormResults.StatusCode = "USERREGISTER";
        //                MyRegisterFormResults.Status = "User Registration Done";
        //                MyRegisterFormResults.Message = "User Registration Done";
        //                MyUELHelpers.Info(typeof(RegisterController).ToString(), "Creating Table", "Started " + DateTime.Now.ToString());
        //                SetupProcessDTOInfo = m_ISetupProcessBusiness.CreateTable("boundhound" + MyUserInfo.TenantId, MyUserInfo);
        //                if (SetupProcessDTOInfo.Status == "SUCCESS")
        //                {
        //                    MyUELHelpers.Info(typeof(RegisterController).ToString(), "Table Created Successfully", "End " + DateTime.Now.ToString());
        //                    MyUserInfo.databasename = "boundhound" + MyUserInfo.TenantId;
        //                }
        //                else
        //                {
        //                    MyUELHelpers.Info(typeof(RegisterController).ToString(), "Creating Table Error", string.Format("Error {0}\n ", SetupProcessDTOInfo.Message) + DateTime.Now.ToString());
        //                }

        //                if (m_subscriptionDTO.SubscriptionType == "Paid")
        //                {


        //                    registerForm.TotalAmount = m_subscriptionDTO.Amount;
        //                    //create a card
        //                    ExpMonth = registerForm.ExpMonth.Split('/')[0];
        //                    ExpYear = registerForm.ExpMonth.Split('/')[1];

        //                    MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveCardDetails", "Started " + DateTime.Now.ToString());
        //                    CardDetailsDTO cardDetailsDTOInfo = null;
        //                    cardDetailsDTOInfo = m_ICardDetailsBusiness.savecardDetails(registerForm.ccNo, registerForm.SecurityCode, ExpMonth, ExpYear, registerForm.UserEmail, APIKey, MyUserInfo);
        //                    MyUELHelpers.Info(typeof(RegisterController).ToString(), "SaveCardDetails", "Ended " + DateTime.Now.ToString());
        //                    if (cardDetailsDTOInfo != null)
        //                    {
        //                        //payment
        //                        MyUELHelpers.Info(typeof(RegisterController).ToString(), "SavePaymentDetails", "Started " + DateTime.Now.ToString());
        //                        CardPaymentDTOInfo = m_paymentDetailsBusiness.PaymentDetails(Convert.ToString(cardDetailsDTOInfo.Id), registerForm.TotalAmount, APIKey, MyUserInfo);
        //                        MyUELHelpers.Info(typeof(RegisterController).ToString(), "SavePaymentDetails", "Done " + DateTime.Now.ToString());
        //                        MyResultResponse.Status = CardPaymentDTOInfo.transactionstatus;
        //                        MyResultResponse.Message = "Paid Subscription";

        //                        MyRegisterFormResults.StatusCode = "PAYMENTDONESUCCESS";
        //                        MyRegisterFormResults.Status = "Payment done subscription created";
        //                        MyRegisterFormResults.Message = "Payment done subscription created";

        //                        MyRegisterFormResults.PaymentId = CardPaymentDTOInfo.PaymentId;
        //                        MyRegisterFormResults.TransactionId = CardPaymentDTOInfo.transactionId;
        //                        //****************************************************************************************************************************************************************
        //                        // Save In Payment Items
        //                        //****************************************************************************************************************************************************************
        //                        MyPaymentItems = new PaymentItems();

        //                        MyPaymentItems.PaymentId = CardPaymentDTOInfo.id;
        //                        MyPaymentItems.ItemName = "Subscription Charges";
        //                        MyPaymentItems.PurchaseAmount = registerForm.TotalAmount;
        //                        MyPaymentItems.NoOfUsers = NoOfUsers;
        //                        PurchaseItemsId = m_IPurchaseUsersBusiness.CreatePaymentItems(MyPaymentItems, MyUserInfo);

        //                        //****************************************************************************************************************************************************************

        //                        //Send this Details to send the mail
        //                        //MyRegisterFormResults.Subscription = m_subscriptionDTO;
        //                        //MyRegisterFormResults.CardPayment = CardPaymentDTOInfo;
        //                        //MyRegisterFormResults.User = UserDTOInfo;

        //                        //TODO Send the email to user

        //                    }
        //                }

        //                //Send Email After Register
        //                Subject = "User Registration Confirmation...";

        //                MyUserInfo.UserName = UserDTOInfo.UserName;
        //                MyUserInfo.Email = UserDTOInfo.UserEmail;
        //                MyUserInfo.Password = UserDTOInfo.UserPassword;

        //                MyUserInfo.SubscriptionName = SubscriptionName;

        //                //Template Parse
        //                TemplateParsers MyTemplateParser = new TemplateParsers(MyUserInfo);

        //                //Get After register html template from the Templates/AfterRegister.html
        //                var filePath = HttpContext.Current.Server.MapPath("~/Templates/AfterRegister.html");

        //                string html = File.ReadAllText(filePath);

        //                TemplateBody = MyTemplateParser.ParseAfterRegister(html, MyUserInfo);

        //                MessageBody = TemplateBody;

        //                //GET SMTP Settings
        //                SMTPSettings BoundHoundSMTPSettings = new SMTPSettings();

        //                UtilityBusiness MyUtilityBusiness = new UtilityBusiness();
        //                BoundHoundSMTPSettings = MyUtilityBusiness.GetBoundHoundSMPTSettings(MyUserInfo);

        //                if (BoundHoundSMTPSettings != null)
        //                {
        //                    if (BoundHoundSMTPSettings.Id > 0)
        //                    {
        //                        //string FromAddress = string.Format("{0} <{1}>", BoundHoundSMTPSettings.DisplayName, BoundHoundSMTPSettings.UserName);

        //                        MyResultReponse = m_IEmailsBusiness.SendEmailAfterRegister(registerForm.UserEmail, BoundHoundSMTPSettings.HostName, BoundHoundSMTPSettings.Port, BoundHoundSMTPSettings.UserName,
        //                        BoundHoundSMTPSettings.UserPassword,
        //                        Subject, MessageBody, UserDTOInfo, m_subscriptionDTO, CardPaymentDTOInfo);

        //                        //MyResultReponse = m_IEmailsBusiness.SendEmailAfterRegister(registerForm.UserEmail, MyUserInfo.FromUserEmail,
        //                        //        MyUserInfo.FromPassword,
        //                        //        Subject, MessageBody, UserDTOInfo, m_subscriptionDTO, CardPaymentDTOInfo);

        //                        if (MyResultReponse.Status == "SUCCESS")
        //                        {
        //                            MyRegisterFormResults.StatusCode = "USERREGISTER";
        //                            MyRegisterFormResults.Status = "User Registration Done";
        //                            MyRegisterFormResults.Message = "User Registration Done";
        //                            MyRegisterFormResults.User = UserDTOInfo;

        //                        }
        //                        else
        //                        {
        //                            MyRegisterFormResults.StatusCode = "PAYMENTDONESUCCESS";
        //                            MyRegisterFormResults.Status = "Payment done subscription created";
        //                            MyRegisterFormResults.Message = "Payment done subscription created - Email error ";
        //                            MyRegisterFormResults.User = UserDTOInfo;
        //                        }
        //                    }
        //                }



        //                //SetupProcessDTOInfo = m_ISetupProcessBusiness.CreateTable("boundhound" + MyUserInfo.TenantId, MyUserInfo);

        //            }

        //        }

        //        else
        //        {
        //            MyRegisterFormResults.Message = "InValid Subscription Id ";
        //            MyRegisterFormResults.User = UserDTOInfo;
        //        }
        //        return Json(MyRegisterFormResults);
        //    }
        //    catch (Exception Ex)
        //    {
        //        MyUELHelpers.ErrorLog(typeof(RegisterController).ToString(), "RegisterForm", Ex.ToString());
        //    }

        //    return Json(MyRegisterFormResults);
        //}





        [Route("api/CheckEmailExists")]
        [HttpPost]
        public IHttpActionResult CheckEmailExists(EmailCheckDTO Email)
        {
            ResultReponse MyResultReponse = new ResultReponse();
            UserDTO UserInfo = new UserDTO();
            try
            {

                MyUELHelpers.Info(typeof(RegisterController).ToString(), "CheckEmailExists", "Started " + DateTime.Now.ToString());

                UserInfo = m_IRegisterBusiness.CheckEmailExists(Email.Email);
                if (UserInfo != null)
                {
                    if (UserInfo.Id > 0)
                    {
                        MyResultReponse.Status = "EMAILEXISTS";
                        MyResultReponse.StatusCode = "EMAILEXISTS";
                        MyResultReponse.Message = "Email already exists";
                    }
                    else
                    {
                        MyResultReponse.Status = "EMAILNOTEXISTS";
                        MyResultReponse.StatusCode = "EMAILNOTEXISTS";
                        MyResultReponse.Message = "Email not exists";
                    }
                }
                else
                {
                    MyResultReponse.Status = "EMAILNOTEXISTS";
                    MyResultReponse.StatusCode = "EMAILNOTEXISTS";
                    MyResultReponse.Message = "Email not exists";

                }
                MyUELHelpers.Info(typeof(RegisterController).ToString(), "CheckEmailExists", "Done " + DateTime.Now.ToString());

                return Json(MyResultReponse);
            }
            catch (Exception Ex)
            {
                MyUELHelpers.ErrorLog(typeof(RegisterController).ToString(), "CheckEmailExists", Ex.ToString());
            }
            return null;
        }


        [Route("api/ForgetPassword")]
        [HttpPost]
        public ResultMessagesReponse ForgetPassword(UserInfo userinfo)
        {
            ResultMessagesReponse MyResultMessagesReponse = new ResultMessagesReponse();
            try
            {

                MyResultMessagesReponse = m_IRegisterBusiness.SendMail_ForgetPassword(userinfo);
            }
            catch (Exception Ex)
            {
                MyUELHelpers.ErrorLog(typeof(RegisterController).ToString(), "ForgetPassword", Ex.ToString());
            }
            return MyResultMessagesReponse;
        }

        [Route("api/Renewpassword")]
        [HttpPost]
        public ResultMessagesReponse Renewpassword(UserInfo userinfo)
        {
            ResultMessagesReponse MyResultMessagesReponse = new ResultMessagesReponse();
            try
            {
                MyResultMessagesReponse = m_IRegisterBusiness.Renewpassword(userinfo);
            }
            catch (Exception Ex)
            {
                MyUELHelpers.ErrorLog(typeof(RegisterController).ToString(), "Renewpassword", Ex.ToString());
            }
            return MyResultMessagesReponse;
        }
        [HttpPost, Route("api/CreditLimitDecrement")]
        public int CreditLimitDecrement(UserInfo userinfo)
        {
            try
            {
                int Credits = m_IRegisterBusiness.CreditLimitDecrement(userinfo);
                return Credits;
            }
            catch (Exception Ex)
            {
                MyUELHelpers.ErrorLog(typeof(RegisterController).ToString(), "CreditLimitDecrement", Ex.ToString());
            }
            return 0;
        }


    }
}
