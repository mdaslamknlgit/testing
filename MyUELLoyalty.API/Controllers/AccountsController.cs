using MyUELLoyalty.API.Authentication;
using MyUELLoyalty.API.Base;
using MyUELLoyalty.API.Helpers;
using MyUELLoyalty.Model;
using MyURLLoyalty.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace MyUELLoyalty.API.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        //private readonly ILinkedinBusiness m_ILinkedinBusiness;
        //private readonly IEmailListBusiness m_IEmailListBusiness;

        private readonly IUsersBusiness m_IUsersBusiness;
        private readonly UserInfo MyUserInfo = new UserInfo();

        public AccountsController(IUsersBusiness musersBusiness)
        {
            MyUserInfo = SetUserInfo();
            m_IUsersBusiness = musersBusiness;
        }

        [BasicAuthentication]
        [HttpPost, Route("api/authenticate")]
        public IHttpActionResult Authenticate()
        {

            try
            {
                BasicAuthenticationDTO _basicAuthIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationDTO;
                //  return Json(tokenEntities);
                return Content(HttpStatusCode.OK, _basicAuthIdentity._AuthUserDTO);
            }
            catch (Exception Ex)
            {MyUELHelpers.ErrorLog(typeof(AccountsController).ToString(), "Authenticate", Ex.ToString()); }
            return null;
        }






        //[HttpPost, Route("api/authenticate")]
        //public IHttpActionResult Authenticate(UserInfo userInfo)
        //{
        //    UserInfo MyUserInfo = null;
        //    try
        //    {

        //        MyUserInfo = m_IUserBusiness.Authenticate(userInfo.Email, userInfo.Password);
        //        //if (userInfo != null)
        //        //{
        //        //    ProfileURL = mEmailDTO.FirstOrDefault().Url;
        //        //    ReturnValue = m_ILinkedinBusiness.SendMessage(ProfileURL, SendMessage, MyUserInfo);
        //        //}

        //        return Json(userInfo);
        //    }
        //    catch (Exception Ex)
        //    { Helpers.ErrorLog(typeof(EmailController).ToString(), "UpdateEmail", Ex.ToString()); }
        //    return null;
        //}

    }
}
