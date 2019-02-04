using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace MyUELLoyalty.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services

            //// Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            // https://stackoverflow.com/questions/37629422/angularjs-with-asp-net-web-api-http-post-returning-xmlhttprequest-cannot-load
            var enableCorsAttribute = new EnableCorsAttribute("*",
                "Origin, Content-Type, Accept, Authorization, MyUELLoyaltyAuthToken, MyUELLoyaltyServerToken",
                "GET, PUT, POST, DELETE, OPTIONS");
            config.EnableCors(enableCorsAttribute);

            // configure batching, default should be non sequential
            config.Routes.MapHttpBatchRoute("WebApiBatch", "api/$batch",
                new DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer)
                {
                    ExecutionOrder = BatchExecutionOrder.NonSequential
                });
            config.Routes.MapHttpBatchRoute("WebApiSequentialBatch", "api/seq/$batch",
                new DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer)
                {
                    ExecutionOrder = BatchExecutionOrder.Sequential
                });

            config.MapHttpAttributeRoutes();
            config.Filters.Add(new WebApiExceptionFilterAttribute());
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.Add(new BrowserJsonFormatter());

            //Register Swagger
            //SwaggerConfig.Register();
        }
    }

    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // If we don't have an http context, we can fallback to looking at the config section for system.web/compilation
            if ((HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled) || IsDebuggingEnabled())
            {
                SerializerSettings.Formatting = Formatting.Indented;
            }
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        private static bool IsDebuggingEnabled()
        {
            var compilationSection = (CompilationSection)ConfigurationManager.GetSection(@"system.web/compilation");

            // Check the DEBUG attribute on the <compilation> element. If no section exists, return false.
            return compilationSection != null && compilationSection.Debug;
        }
    }

    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// We want to log all exceptions except UserException. If the Exception is a HttpResponseException,
        /// we just throw it, otherwise we create a new HttpResponseException with the exception details and throw that.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = new HttpError
            {
                {"StatusCode", ((int) statusCode).ToString(CultureInfo.InvariantCulture)},
                {"Status", statusCode.ToString()},
                {"ExceptionMessage", exception.Message},
                {"ExceptionType", exception.GetType().FullName}
            };

            //var dependencyScope = context.Request.GetDependencyScope();

            //if (exception.GetType() == typeof(UserException))
            //    throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, errorMessage));

            //if (exception.GetType() == typeof(ValidationException))
            //    throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, errorMessage));

            //if (exception.GetType() == typeof(DuplicateException))
            //    throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, errorMessage));

            //if (exception.GetType() == typeof(DuplicateNameException))
            //    throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, errorMessage));

            ////TODO: Please make sure to add a config setting that will allows us to see the exceptions on UI
            //var errorService = (IErrorService)dependencyScope.GetService(typeof(IErrorService));

            //errorService.LogWebApiError(null, HttpContext.Current.Request, null, exception);

            //throw new HttpResponseException(context.Request.CreateErrorResponse(statusCode, errorMessage));
        }

    }
}
