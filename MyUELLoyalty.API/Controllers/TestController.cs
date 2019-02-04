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
    public class TestController : ApiControllerBase
    {
        private readonly IUsersBusiness m_IUsersBusiness;
        private readonly UserInfo MyUserInfo = new UserInfo();

        public TestController(IUsersBusiness musersBusiness)
        {
            MyUserInfo = SetUserInfo();
            m_IUsersBusiness = musersBusiness;
        }

        [BasicAuthentication]
        [HttpPost, Route("api/testauthenticate")]
        public IHttpActionResult Authenticate()
        {

            try
            {
                BasicAuthenticationDTO _basicAuthIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationDTO;
                //  return Json(tokenEntities);
                return Content(HttpStatusCode.OK, _basicAuthIdentity._AuthUserDTO);
            }
            catch (Exception Ex)
            {
                MyUELHelpers.ErrorLog(typeof(AccountsController).ToString(), "Authenticate", Ex.ToString());
            }
            return null;
        }
    }
}
