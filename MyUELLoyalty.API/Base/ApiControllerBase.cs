using MyUELLoyalty.API.Authentication;
using MyUELLoyalty.Model;
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
    [BasicAuthentication]
    public class ApiControllerBase : ApiController
    {
        //private readonly Lazy<SchoolContext> m_context;
        //private readonly IHttpContextService m_httpContextService;
        //public ITokenAuthenticationService Service { get; }
        //private static readonly DateTime AlreadyExpired = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //protected SchoolContext Context => m_context.Value;

        //protected const string AuthenticationCookie = "patasalaAuth";
        //public ApiControllerBase(IHttpContextService httpContextService, ITokenAuthenticationService tokenAuthenticationService)
        //{
        //    m_context = new Lazy<SchoolContext>(new PatasalaCommon(httpContextService, tokenAuthenticationService)
        //        .GetSchoolContext);
        //    m_httpContextService = httpContextService;
        //    Service = tokenAuthenticationService;
        //}

        //public HttpResponseMessage CreateAuthenticationResponse(UserDTO userInfo)
        //{
        //    var cookieInfo =
        //        new AuthenticatedPatasalaUser(m_httpContextService).PrepareAuthenticationCookieString(
        //            Convert.ToUInt32(userInfo.Id), userInfo.Email, userInfo.UserName);
        //    var expiration = DateTime.Now.Add(new TimeSpan(0, Convert.ToInt32(FormsAuthentication.Timeout.TotalMinutes), 0));

        //    // must use this form of the FormsAuthenticationTicket constructor in order for sliding expiration to work
        //    var ticket = new FormsAuthenticationTicket(1,
        //        cookieInfo,
        //        DateTime.Now,
        //        expiration,
        //        true,
        //        "",
        //        FormsAuthentication.FormsCookiePath);

        //    // Encrypt the ticket.
        //    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
        //    var cookie = new HttpCookie(AuthenticationCookie, encryptedTicket)
        //    {
        //        Expires = expiration,
        //        Secure = FormsAuthentication.RequireSSL
        //    };

        //    var response = Request.CreateResponse(HttpStatusCode.OK, userInfo);

        //    response.Headers.AddCookies(new List<CookieHeaderValue>
        //    {
        //        new CookieHeaderValue(AuthenticationCookie, cookie.Value)
        //        {
        //            HttpOnly = true,
        //            Path = "/",
        //            Expires = expiration
        //        }
        //    });
        //    return response;
        //}

        //public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        //{
        //    // We don't want to continue if somehow the task request has been closed or canceled. this change will handle both task and operation cancelled errors. 
        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        return Task.FromResult(response);
        //    }

        //    var task = base.ExecuteAsync(controllerContext, cancellationToken);
        //    return task.IsFaulted
        //        ? task
        //        : task.ContinueWith(t =>
        //        {
        //            t.Result.Headers.CacheControl = new CacheControlHeaderValue
        //            {
        //                NoStore = true,
        //                NoCache = true,
        //                MaxAge = new TimeSpan(0),
        //                MustRevalidate = true
        //            };
        //            t.Result.Headers.Pragma.Add(new NameValueHeaderValue("no-cache"));
        //            if (t.Result.Content != null)
        //            {
        //                t.Result.Content.Headers.Expires = AlreadyExpired;
        //            }

        //            return t.Result;
        //        }, cancellationToken);
        //}

        private readonly UserInfo _MyUserInfo = new UserInfo();

        public UserInfo SetUserInfo()
        {
            //_MyUserInfo.Email = ConfigurationManager.AppSettings["LinkedEmail"].ToString();
            //_MyUserInfo.Password = ConfigurationManager.AppSettings["LinkedPassword"].ToString();
            //_MyUserInfo.FromUserEmail = ConfigurationManager.AppSettings["FromUserEmail"].ToString();
            //_MyUserInfo.FromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();


            //BasicAuthenticationDTO _basicAuthIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationDTO;

            //_MyUserInfo.TenantId = _basicAuthIdentity._AuthUserDTO.TenatId;
            //_MyUserInfo.UserId = _basicAuthIdentity._AuthUserDTO.UserId;
            //_MyUserInfo.LinkedInEmail = _basicAuthIdentity._AuthUserDTO.LinkedInEmail;
            //_MyUserInfo.LinkedInPassword = _basicAuthIdentity._AuthUserDTO.LinkedInPassword;
            //_MyUserInfo.databasename = _basicAuthIdentity._AuthUserDTO.DatabaseName;
            //MyUserInfo.UsersIds = _basicAuthIdentity._AuthUserDTO.UsersIds;

            return _MyUserInfo;
        }


    }
}