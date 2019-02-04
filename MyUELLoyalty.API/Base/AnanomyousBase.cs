using MyUELLoyalty.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace MyUELLoyalty.API.Base
{
    public class AnanomyousBase : ApiController
    {


        private readonly UserInfo _MyUserInfo = new UserInfo();

        public UserInfo SetUserInfo()
        {
            _MyUserInfo.Email = ConfigurationManager.AppSettings["LinkedEmail"].ToString();
            _MyUserInfo.Password = ConfigurationManager.AppSettings["LinkedPassword"].ToString();
            _MyUserInfo.FromUserEmail = ConfigurationManager.AppSettings["FromUserEmail"].ToString();
            _MyUserInfo.FromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

            return _MyUserInfo;
        }


    }
}