using MyUELLoyalty.API.App_Start;
using MyUELLoyalty.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyUELLoyalty.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public string Log4NetFileLocation = "";
        public string LogFullPath = "";
        public string MasterConnectionString = "server=localhost;user id=root; password=sparsh;persist security info=True;database=boundhoundmaster";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Register All Interfaces
            System.Web.Http.GlobalConfiguration.Configure(IOCConfig.RegisterManagers);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //*************************************************************************************************************************************************************
            var log4NetPath = Server.MapPath("~/log4net.config");
            //For Log
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4NetPath));
            LogFullPath = Server.MapPath("~/logs/");
            Log4NetFileLocation = string.Format("{0}BoundHound-", LogFullPath);
            MyUELHelpers.ChangeFileLocation(Log4NetFileLocation);

            //Start Quartz Scheduler
            //JobScheduler.Start();


            //**************************************************************************************************************************************************************************************************************************************************
            //Add jobs here
            //**************************************************************************************************************************************************************************************************************************************************



            //**************************************************************************************************************************************************************************************************************************************************

            //**************************************************************************************************************************************************************************************************************************************************
            //Hangfire starts here
            //**************************************************************************************************************************************************************************************************************************************************
            //var storage = new SqlServerStorage(System.Configuration.ConfigurationManager.ConnectionStrings["db_HangFire"].ConnectionString); // db_HangFire is the connection string for Sql Server DB used as Job Storage for HangFire for processing 

            //var options = new BackgroundJobServerOptions();

            //var _backgroundJobServer = new BackgroundJobServer(options, storage);
            //_backgroundJobServer.Start(); // start BackgroundJobServer process
            //JobStorage.Current = storage; // assign the storage to Current
            //**************************************************************************************************************************************************************************************************************************************************

            //Other way to start hangfire
            //Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("YOUR_CONNECTION_STRING");
            //Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("YOUR_CONNECTION_STRING");

            //MySQL
            //Hangfire.GlobalConfiguration.Configuration.UseStorage(new MySqlStorage("LinkedInConnection"));

            //Hangfire.GlobalConfiguration.Configuration.UseStorage(
            //        new MySqlStorage(
            //            MasterConnectionString,
            //            new MySqlStorageOptions
            //            {
            //                TransactionIsolationLevel = IsolationLevel.ReadCommitted,
            //                QueuePollInterval = TimeSpan.FromSeconds(15),
            //                JobExpirationCheckInterval = TimeSpan.FromHours(1),
            //                CountersAggregateInterval = TimeSpan.FromMinutes(5),
            //                PrepareSchemaIfNecessary = true,
            //                DashboardJobListLimit = 50000,
            //                TransactionTimeout = TimeSpan.FromMinutes(1),
            //                //TablesPrefix = "Hangfire"
            //            }));

            //_server = new BackgroundJobServer();
            //**************************************************************************************************************************************************************************************************************************************************

            //Hang Fire schedule starts
            // RecurringJob.AddOrUpdate(() => MyJobScheduler.RunCampaigns(), "1 0 * 1/1 *");


            //MyJobScheduler.RunCampaigns();
            //*************************************************************************************************************************************************************

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //_backgroundJobServer.Dispose(); // Dispose the BackgroundJobServer Object

            //_server.Dispose();
        }

        protected void Session_Start()
        {
            LogFullPath = Server.MapPath("~/logs/");
            Log4NetFileLocation = string.Format("{0}MyUELLoyalty.API-", LogFullPath);
            Session["LogFilePath"] = Log4NetFileLocation;
        }
    }
}
