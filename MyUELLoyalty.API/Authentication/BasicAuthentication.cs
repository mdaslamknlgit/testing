using MyUELLoyalty.API.Helpers;
using MyUELLoyalty.Model;
using MyURLLoyalty.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyUELLoyalty.API.Authentication
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IUsersBusiness)) as IUsersBusiness;

            //var CurrencyProvider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ICurrencyBusiness)) as ICurrencyBusiness;
            //var EmailSetupProvider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ICheckItemsBusiness)) as ICheckItemsBusiness;

            int CreditUsage = 0;
            bool EmailSetupFinish = false;
            UserInfo MyUserInfo = null;
            string MyUELLoyaltyAuthToken = "";
            var tokenEntities = new AuthUserDTO { };
            IEnumerable<AppSettingsDTO> MyAppSettings = null;
            IEnumerable<CurrencyDTO> BaseCurrencyList = null;
            CheckItem MyCheckItem = null;
            CurrencyDTO BaseCurrency = null;
            //Checking token in header if exists will go for token validation else go for username password validation
            HttpRequestHeaders headers = actionContext.Request.Headers;
            if (headers.Contains("MyUELLoyaltyAuthToken"))
            {
                MyUELLoyaltyAuthToken = headers.GetValues("MyUELLoyaltyAuthToken").First();
                //validating token with data base
                MyUserInfo =MyUELHelpers.ValidateToken(MyUELLoyaltyAuthToken, actionContext);
                if (MyUserInfo != null)
                {
                    //MyAppSettings = provider.GetAppSettings(MyUserInfo);
                    tokenEntities = new AuthUserDTO
                    {
                        UserName = MyUserInfo.UserName,
                        FirstName = MyUserInfo.FirstName,
                        LastName = MyUserInfo.LastName,
                        UserEmail = MyUserInfo.userEmail,
                        Email = MyUserInfo.Email,
                        TenatId = MyUserInfo.TenantId,
                        UserId = MyUserInfo.UserId,
                        UsersIds = MyUserInfo.UsersIds,
                        TenantName = MyUserInfo.TenantName,
                        LinkedInEmail = MyUserInfo.LinkedInEmail,
                        LinkedInPassword = MyUserInfo.LinkedInPassword,
                        LinkedinTypeId = MyUserInfo.LinkedinTypeId,
                        SubscriptionId = MyUserInfo.SubscriptionId,
                        SubscriptionName = MyUserInfo.SubscriptionName,
                        SubscriptionType = MyUserInfo.SubscriptionType,
                        SubscriptionInDays = MyUserInfo.SubscriptionInDays,
                        access_token = MyUELLoyaltyAuthToken,
                        ExpiredDate = DateTime.Now.AddHours(1).ToString(),
                        GenerateDate = DateTime.Now.ToString(),
                        IsFirstTime = MyUserInfo.IsFirstTime,
                        DatabaseName = MyUserInfo.databasename,
                        Modules = "",
                        // MyAppSettings = MyAppSettings
                    };

                    //BasicAuthenticationDTO _basicAuthIdentity = new BasicAuthenticationDTO(tokenEntities);
                    //var genericPrincipal = new GenericPrincipal(_basicAuthIdentity, null);
                    //Thread.CurrentPrincipal = genericPrincipal;
                }
                else { actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "INVALIDTOKEN"); }
            }
            else
            {

                //SyncStatusDbService MySyncStatusDbService = null;
                JavaScriptSerializer js = new JavaScriptSerializer();
                var request = actionContext.Request.Content.ReadAsStringAsync().Result;
                UserInfo UsersInfo = js.Deserialize<UserInfo>(request);
                bool SyncStatus = false;
                if (UsersInfo != null)
                {
                    //validating username and password with data base
                    MyUserInfo = provider.Authenticate(UsersInfo.UserName, UsersInfo.Password);

                    if (MyUserInfo != null)
                    {
                        //Check Background Process
                        //MySyncStatusDbService = new SyncStatusDbService(MyUserInfo);
                        //SyncStatus = MySyncStatusDbService.CheckSyncRecords();
                        //MyAppSettings = provider.GetAppSettings(MyUserInfo);
                        //Set Base Currency
                        //BaseCurrencyList = CurrencyProvider.GetDefaultCurrency(MyUserInfo);
                        //if (BaseCurrencyList.Count() > 0)
                        //{
                        //    BaseCurrency = BaseCurrencyList.FirstOrDefault();
                        //}

                        //Check Email Setup
                        //MyCheckItem = EmailSetupProvider.CheckEmailSetup(MyUserInfo);
                        //if (MyCheckItem.SNO > 0)
                        //{
                        //    EmailSetupFinish = MyCheckItem.Status;
                        //}


                        //Get Credit Usage
                        //CreditUsage = provider.GetCreditUsage(MyUserInfo);
                    }
                }

                if (MyUserInfo != null)
                {
                    UserDTO mUserDTO = new UserDTO();
                    mUserDTO.TenantId = MyUserInfo.TenantId;
                    mUserDTO.Email = MyUserInfo.Email;
                    mUserDTO.UserEmail = MyUserInfo.userEmail;
                    mUserDTO.Id = MyUserInfo.UserId;
                    mUserDTO.UserName = MyUserInfo.UserName;
                    mUserDTO.FirstName = MyUserInfo.FirstName;
                    mUserDTO.LastName = MyUserInfo.LastName;
                    mUserDTO.DatabaseName = MyUserInfo.databasename;
                    mUserDTO.UsersIds = MyUserInfo.UsersIds;
                    mUserDTO.SubscriptionId = MyUserInfo.SubscriptionId;
                    mUserDTO.SubscriptionName = MyUserInfo.SubscriptionName;
                    mUserDTO.SubscriptionEnd = MyUserInfo.SubscriptionEnd;
                    mUserDTO.CreditLimit = MyUserInfo.CreditLimit;
                    mUserDTO.RemainingCredits = MyUserInfo.RemainingCredits;
                    mUserDTO.CreditUsage = CreditUsage;


                    //Generating token Based on above parameters
                    string Token =MyUELHelpers.GenerateToken(mUserDTO);
                    //AppSettingsDTO MyAppSettings
                    tokenEntities = new AuthUserDTO
                    {
                        FirstName = MyUserInfo.FirstName,
                        LastName = MyUserInfo.LastName,
                        UserName = MyUserInfo.UserName,
                        UserEmail = MyUserInfo.userEmail,
                        Email = MyUserInfo.Email,
                        TenatId = MyUserInfo.TenantId,
                        UserId = MyUserInfo.UserId,
                        UsersIds = MyUserInfo.UsersIds,
                        TenantName = MyUserInfo.TenantName,
                        LinkedInEmail = MyUserInfo.LinkedInEmail,
                        LinkedInPassword = MyUserInfo.LinkedInPassword,
                        LinkedinTypeId = MyUserInfo.LinkedinTypeId,
                        SubscriptionInDays = MyUserInfo.SubscriptionId,
                        access_token = Token,
                        ExpiredDate = DateTime.Now.AddHours(1).ToString(),
                        GenerateDate = DateTime.Now.ToString(),
                        IsFirstTime = MyUserInfo.IsFirstTime,
                        DatabaseName = MyUserInfo.databasename,
                        Modules = "",
                        IsBackgroundProcess = SyncStatus,
                        SubscriptionId = MyUserInfo.SubscriptionId,
                        SubscriptionName = MyUserInfo.SubscriptionName,
                        SubscriptionType = MyUserInfo.SubscriptionType,
                        SubscriptionEnd = MyUserInfo.SubscriptionEnd,
                        MyAppSettings = MyAppSettings,
                        BaseCurrency = BaseCurrency,
                        IsEmailSetupFinish = EmailSetupFinish,
                        CreditLimit = MyUserInfo.CreditLimit,
                        RemainingCredits = MyUserInfo.RemainingCredits,
                        CreditUsage = CreditUsage
                    };

                    //BasicAuthenticationDTO _basicAuthIdentity = new BasicAuthenticationDTO(tokenEntities);
                    //var genericPrincipal = new GenericPrincipal(_basicAuthIdentity, null);
                    //Thread.CurrentPrincipal = genericPrincipal;

                    //_basicAuthIdentity.UserName = MyUserInfo.UserName;
                    //_basicAuthIdentity.UserEmail = UsersInfo.Email;
                    //_basicAuthIdentity.TenatId = MyUserInfo.TenantId;
                    //_basicAuthIdentity.UserId = MyUserInfo.UserId;
                    //_basicAuthIdentity.SubscriptionInDays = MyUserInfo.SubscriptionId;
                    //_basicAuthIdentity.BoundHoundAuthToken = Token;
                    //_basicAuthIdentity.ExpiredDate = DateTime.Now.AddHours(1).ToString();
                    //_basicAuthIdentity.GenerateDate = DateTime.Now.ToString();
                    //_basicAuthIdentity.Modules = "";
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "INVALIDUSER");
                }

            }


            BasicAuthenticationDTO _basicAuthIdentity = new BasicAuthenticationDTO(tokenEntities);
            var genericPrincipal = new GenericPrincipal(_basicAuthIdentity, null);
            Thread.CurrentPrincipal = genericPrincipal;


            //  base.OnAuthorization(actionContext);
        }

    }
}