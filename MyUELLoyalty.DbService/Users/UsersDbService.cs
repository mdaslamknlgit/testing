using MyUELLoyalty.DbService.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MyUELLoyalty.Model;
using Dapper;

namespace MyUELLoyalty.DbService.Users
{
    public class UsersDbService : IUsersDbService
    {
        private readonly IDbConnection _db;
        private readonly IDbConnection _dbMaster;
        private IDbConnection _dbCon;
        string connection = ConfigurationManager.ConnectionStrings["MyUELLoyaltyProgrammConnection"].ConnectionString;

        public UsersDbService()
        {
            //_dbMaster = new MySqlConnection(ConfigurationManager.ConnectionStrings["LinkedInConnection"].ConnectionString);
            //_db = new MySqlConnection(ConfigurationManager.ConnectionStrings["LinkedInConnection"].ConnectionString);

            //_dbMaster = new MySqlConnection(ConfigurationManager.ConnectionStrings["LinkedInConnection"].ConnectionString);
            //_db = new MySqlConnection(ConfigurationManager.ConnectionStrings["LinkedInConnection"].ConnectionString);

            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyUELLoyaltyProgrammConnection"].ConnectionString);
        }

        public UserInfo TokenValidation(string Email, int UserId, int TenantId)
        {
            UserInfo MyUserInfo = new UserInfo();
            try
            {
                const string sql =
                     @"Select usr.Id as UserId,usr.userTypeId,usr.firstName,usr.LastName,usr.UserName,usr.userEmail,usr.userPassword,usr.TenantId,usr.roleId,
                    tn.Id,tn.tenantName,tn.linkedinEmail,tn.linkedinPassword,tn.subscriptionId,tn.databasename,usr.IsFirstTime IsFirstTime,
                    sb.subscriptionName,sb.subscriptionType,sb.subscriptionDays SubscriptionInDays,
                    (select userName from users where Id=usr.managerId) ManagerName
                    --(case when (select concat(group_concat(u.Id),',',u.managerId)  end from users u where u.managerId=usr.id) is null then usr.id else (select  concat(group_concat(u.Id),',',u.managerId) end from users u where u.managerId=usr.id) end)  UsersIds
					(case when usr.managerid>0 then cast( usr.id as nvarchar(100)) else 
					 (select
                       distinct  
                        stuff((
                            select ',' +convert(Nvarchar(100), cast(u.id as varchar(100)))
                            from users u
                             where u.id in (select id from users u1 where  ( u.managerid=usr.id  or u1.id=usr.id))
                            order by u.username
                            for xml path('')
                        ),1,1,'') as userlist
                     ) end) UsersIds,
                    From Users usr
                    Inner Join tenants tn on tn.Id=usr.tenantId
                    inner join subscription sb on sb.Id=tn.subscriptionId
                    where usr.userEmail=@email and usr.TenantId=@TenantId and usr.Id=@userId;";

                MyUserInfo = this._db.Query<UserInfo>(sql, new Dictionary<string, object> { { "email", Email }, { "TenantId", TenantId }, { "userId", UserId } }).FirstOrDefault();
            }
            catch(Exception exp)
            {
                var MessageError = exp.ToString();
            }
            return MyUserInfo;

        }

        public UserInfo Authenticate(string UserName, string Password)
        {
            UserInfo MyUserInfo = new UserInfo();

            //    const string sql =
            //         @"Select usr.Id as UserId,usr.userTypeId,usr.firstName,usr.LastName,usr.UserName,usr.userEmail,usr.userEmail Email,usr.userPassword,usr.TenantId,usr.roleId,
            //            usr.annualQuota,tn.Id,tn.tenantName,tn.linkedinEmail,tn.linkedinPassword,tn.linkedinTypeId,tn.subscriptionId,usr.isFirstTime,
            //            tn.databasename,tn.subscriptionId,sb.subscriptionName,sb.subscriptionType,tn.subscriptionEnd,usr.IsFirstTime IsFirstTime,
            //(select userName from users where Id=usr.managerId) ManagerName,
            //      (case when (select GROUP_CONCAT(u.Id) end from users u where u.managerId=usr.id) is null then usr.id else (select GROUP_CONCAT(u.Id) end from users u where u.managerId=usr.id) end)  UsersIds
            //            From Users usr
            //            Inner Join tenants tn on tn.Id=usr.tenantId
            //            Inner Join subscription sb on sb.subscriptionId=tn.subscriptionId
            //            where usr.userEmail=@email and usr.userPassword=@password;";

            try
            {
                const string sql =
                        @"Select usr.Id as UserId,usr.userTypeId,usr.firstName,usr.LastName,usr.UserName,usr.userEmail,usr.userEmail Email,usr.userPassword,usr.TenantId,usr.roleId,
                    usr.annualQuota,tn.Id,tn.tenantName,tn.linkedinEmail,tn.linkedinPassword,tn.linkedinTypeId,tn.subscriptionId,usr.isFirstTime,
                    tn.databasename,tn.subscriptionId,sb.subscriptionName,sb.subscriptionType,tn.subscriptionEnd,usr.IsFirstTime IsFirstTime,
   					(select userName from users where Id=usr.managerId) ManagerName,
		            --(case when (select concat(group_concat(u.Id),',',u.managerId)  end from users u where u.managerId=usr.id) is null then usr.id else (select  concat(group_concat(u.Id),',',u.managerId) end from users u where u.managerId=usr.id) end)  UsersIds,
					(case when usr.managerid>0 then cast( usr.id as nvarchar(100)) else 
					 (select
                       distinct  
                        stuff((
                            select ',' +convert(Nvarchar(100), cast(u.id as varchar(100)))
                            from users u
                             where u.id in (select id from users u1 where  ( u.managerid=usr.id  or u1.id=usr.id))
                            order by u.username
                            for xml path('')
                        ),1,1,'') as userlist
                     ) end) UsersIds,
                    tn.creditLimit
                    From Users usr
                    Inner Join tenants tn on tn.Id=usr.tenantId
                    Inner Join subscription sb on sb.Id=tn.subscriptionId
                    where usr.userEmail=@email and usr.userPassword=@password;";

                MyUserInfo = this._db.Query<UserInfo>(sql, new Dictionary<string, object> { { "email", UserName }, { "password", Password } }).FirstOrDefault();
            }

            catch(Exception exp)
            {
                var MessageError = exp.ToString();
            }
            return MyUserInfo;

        }



        public int GetCreditUsage(UserInfo MyUserInfo)
        {
            int CreditUsage = 0;

            try
            {
                string TenantConnectionStr = ConfigurationManager.ConnectionStrings["TenantConnection"].ConnectionString;
                TenantConnectionStr = string.Format(TenantConnectionStr, MyUserInfo.databasename);
                //_dbCon = new MySqlConnection(TenantConnectionStr);


                const string sql =
                    @" Select sum(noOfCredits) From creditUsage cr
                    where usagedate between (select startdate from fiscalyear) and (select enddate from fiscalyear);";

                CreditUsage = this._dbCon.Query<int>(sql).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var Message = ex.ToString();
                //throw;
            }
            return CreditUsage;

        }


        public IEnumerable<AppSettingsDTO> GetAppSettings(UserInfo MyUserInfo)
        {
            IEnumerable<AppSettingsDTO> MyAppSettings = null;

            try
            {
                string TenantConnectionStr = ConfigurationManager.ConnectionStrings["TenantConnection"].ConnectionString;
                TenantConnectionStr = string.Format(TenantConnectionStr, MyUserInfo.databasename);
                //_dbCon = new MySqlConnection(TenantConnectionStr);
                _dbCon = new SqlConnection(TenantConnectionStr);


                const string sql =
                    @" select * from appsettings;";

                MyAppSettings = this._dbCon.Query<AppSettingsDTO>(sql).ToList();
            }
            catch (Exception ex)
            {
                var Message = ex.ToString();
                throw;
            }
            return MyAppSettings;

        }

       

        public UserInfo GetUserInfo(int id)
        {
            UserInfo MyUserInfo = new UserInfo();

            const string sql =
                 @"Select usr.Id as UserId,usr.userTypeId,usr.firstName,usr.LastName,usr.UserName,usr.userEmail,usr.userEmail Email,usr.userPassword,usr.TenantId,usr.roleId,
                    usr.annualQuota,tn.Id,tn.tenantName,tn.linkedinEmail,tn.linkedinPassword,tn.linkedinTypeId,tn.subscriptionId,usr.isFirstTime
                    From Users usr
                    Inner Join tenants tn on tn.Id=usr.tenantId
                    where usr.Id=@Id";

            MyUserInfo = this._db.Query<UserInfo>(sql, new Dictionary<string, object> { { "Id", id } }).FirstOrDefault();

            return MyUserInfo;
        }

        public UserDTO IsUserCredentialsValid(string userName)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExist(string userName)
        {
            throw new NotImplementedException();
        }

       

        public int UpdateEmail(int tenantId, int userId)
        {
            throw new NotImplementedException();
        }

        public int UpdateIsFirstTime(int tenantId, int userId)
        {
            UserInfo MyUserInfo = new UserInfo();

            string updateQuery = @"UPDATE Users SET isFirstTime=0 WHERE tenantId =@tenantId And Id=@userId";

            int result = this._db.Execute(updateQuery, new
            {
                tenantId,
                userId
            });
            return result;
        }


        public IEnumerable<UserDTO> GetUserList(UserInfo MyUserInfo)
        {
            IEnumerable<UserDTO> MyUserList = null;
            //  UserDTO MyUserList = new UserDTO();
            try
            {

                string QUERY = @" Select usr.*,tn.tenantName,tn.noOfUsers,(select userName from users where Id=usr.managerId) ManagerName,
                    (case when (select GROUP_CONCAT(u.Id) end from users u where u.managerId=usr.id) is null then usr.id else (select GROUP_CONCAT(u.Id) end from users u where u.managerId=usr.id) end)  UsersIds
                    From Users usr
                    Inner Join tenants tn on tn.Id=usr.tenantId WHERE usr.tenantId =@tenantId";
                MyUserList = this._db.Query<UserDTO>(QUERY, new Dictionary<string, object> { { "tenantId", MyUserInfo.TenantId } }).ToList();
            }
            catch (Exception ex)
            {
                var Message = ex.ToString();
            }
            return MyUserList;
        }

        public UserDTO GetUserById(int Id)
        {
            UserDTO MyUserList = new UserDTO();
            try
            {

                string QUERY = @" Select usr.*,tn.tenantName,(select userName from users where Id=usr.managerId) ManagerName,
                                    case when usr.managerId>0 then (select GROUP_CONCAT(u.Id) end from users u where u.managerId=usr.id) else usr.Id end UsersIds
                                    From Users usr
                                    Inner Join tenants tn on tn.Id=usr.tenantId WHERE usr.Id=@Id";

                MyUserList = this._db.Query<UserDTO>(QUERY, new Dictionary<string, object> { { "Id", Id } }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var Message = ex.ToString();
            }
            return MyUserList;
        }

        public IEnumerable<UserDTO> GetUserByIds(string Id)
        {
            IEnumerable<UserDTO> MyUserList = null;
            try
            {
                int[] Ids = Id.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                string QUERY = @" Select usr.Id, usr.userName ,tn.tenantName,(select userName from users where Id=usr.managerId) ManagerName
                                    From Users usr
                                    Inner Join tenants tn on tn.Id=usr.tenantId WHERE usr.Id in @userIds";

                MyUserList = this._db.Query<UserDTO>(QUERY, new Dictionary<string, object> { { "userIds", Ids } }).AsEnumerable();
            }
            catch (Exception ex)
            {
                var Message = ex.ToString();
            }
            return MyUserList;
        }
        public ResultReponse UpdateUser(UserDTO MyUser, UserInfo MyUserInfo)
        {

            ResultReponse MyResultReponse = new ResultReponse();
            int result = 0;
            try
            {
                const string sql = @"Select UserName from Users  Where UserName=@UserName and Id<>@Id and tenantId=@tenantId;";
                string UserName = this._db.Query<string>(sql, new Dictionary<string, object> { { "UserName", MyUser.UserName }, { "Id", MyUser.UserId }, { "tenantId", MyUserInfo.TenantId } }).FirstOrDefault();

                //const string sql2 = @"Select UserEmail from Users  Where UserEmail=@UserEmail and Id<>@Id and tenantId=@tenantId;";
                const string sql2 = @"Select UserEmail from Users  Where UserEmail=@UserEmail and Id<>@Id;";
                string emailId = this._db.Query<string>(sql2, new Dictionary<string, object> { { "UserEmail", MyUser.Email }, { "Id", MyUser.UserId }, { "tenantId", MyUserInfo.TenantId } }).FirstOrDefault();



                if (UserName != null)
                {
                    MyResultReponse.Status = "ERROR";
                    MyResultReponse.StatusCode = "ERROR";
                    MyResultReponse.Message = "User Name Already Exists...!!!";
                    MyResultReponse.Data = MyUser.ToString();
                }

                else if (emailId != null)
                {
                    MyResultReponse.Status = "ERROR";
                    MyResultReponse.StatusCode = "ERROR";
                    MyResultReponse.Message = "Email Already Exists...!!!";
                    MyResultReponse.Data = MyUser.ToString();
                }

                else
                {
                    //string updateQuery = @"UPDATE Users,Tenants 
                    //                        SET Users.UserName = @UserName,
                    //                        Users.FirstName=@FirstName,
                    //                        Users.LastName=@LastName,
                    //                        Users.UserEmail=@UserEmail,
                    //                        Users.UserPassword=@UserPassword,
                    //                        Users.FromDate=@FromDate,
                    //                        Users.ToDate=@ToDate,
                    //                        Users.IsCustom=@IsCustom,
                    //                        Users.AnnualQuota=@AnnualQuota,
                    //                        Users.Commission=@Commission,
                    //                        Users.ManagerId=@managerId,
                    //                        Tenants.TenantName=@TenantName
                    //                        WHERE Tenants.Id=Users.tenantId and Users.Id =@Id";
                    string updateQuery = @"UPDATE Users,Tenants 
                                            SET Users.UserName = @UserName,
                                            Users.FirstName=@FirstName,
                                            Users.LastName=@LastName,
                                            Users.UserEmail=@UserEmail,
                                            Users.UserPassword=@UserPassword,
                                            Users.FromDate=@FromDate,
                                            Users.ToDate=@ToDate,
                                            Users.IsCustom=@IsCustom,
                                            Users.AnnualQuota=@AnnualQuota,
                                            Users.Commission=@Commission,
                                            Users.ManagerId=@managerId
                                            WHERE Tenants.Id=Users.tenantId and Users.Id =@Id";
                    if (MyUser.AnnualQuota != null)
                    {
                        MyUser.AnnualQuota = MyUser.AnnualQuota.Replace(",", "");
                    }
                    result = this._db.Execute(updateQuery, new
                    {
                        UserName = MyUser.UserName,
                        FirstName = MyUser.FirstName,
                        LastName = MyUser.LastName,
                        UserEmail = MyUser.Email,
                        UserPassword = MyUser.UserPassword,
                        FromDate = MyUser.FromDate,
                        ToDate = MyUser.ToDate,
                        IsCustom = MyUser.IsCustom,
                        AnnualQuota = MyUser.AnnualQuota,
                        Commission = MyUser.Commission,
                        TenantName = MyUser.TenantName,
                        managerId = MyUser.ManagerId,
                        Id = MyUser.UserId


                    });
                    MyResultReponse.Status = "SUCCESS";
                    MyResultReponse.StatusCode = "SUCCESS";
                    MyResultReponse.Message = "User Updated Successfully ";
                    MyResultReponse.Data = result.ToString();
                }


            }
            catch (Exception ex)
            {
                MyResultReponse.Status = "FAIL";
                MyResultReponse.StatusCode = "FAIL";
                MyResultReponse.Message = "Error Occured while Updating contact administrator";
                MyResultReponse.Data = ex.ToString();

                var Message = ex.ToString();
                throw;
            }

            return MyResultReponse;

        }

        public ResultReponse CreateUser(UserDTO MyUser, UserInfo MyUserInfo)
        {
            ResultReponse MyResultReponse = new ResultReponse();

            DateTime FirstDay = DateTime.Now;
            DateTime LastDay = DateTime.Now;


            try
            {

                int year = DateTime.Now.Year;
                FirstDay = new DateTime(year, 1, 1);
                LastDay = new DateTime(year, 12, 31);


                int NewUserId = 0;
                const string sql = @"Select UserName from Users  Where UserName=@UserName and Id<>@Id and tenantId=@tenantId;";
                string UserName = this._db.Query<string>(sql, new Dictionary<string, object> { { "UserName", MyUser.UserName }, { "Id", MyUser.UserId }, { "tenantId", MyUserInfo.TenantId } }).FirstOrDefault();

                //const string sql2 = @"Select UserEmail from Users  Where UserEmail=@UserEmail and Id<>@Id and tenantId=@tenantId;";
                const string sql2 = @"Select UserEmail from Users  Where UserEmail=@UserEmail and Id<>@Id;";
                string emailId = this._db.Query<string>(sql2, new Dictionary<string, object> { { "UserEmail", MyUser.Email }, { "Id", MyUser.UserId }, { "tenantId", MyUserInfo.TenantId } }).FirstOrDefault();

                const string sql3 = @"Select noOfUsers from Tenants  Where Id=@TenantId;";
                string TenantCount = this._db.Query<string>(sql3, new Dictionary<string, object> { { "TenantId", MyUserInfo.TenantId } }).FirstOrDefault();

                const string sql4 = @"SELECT count(*) FROM users where tenantid=@TenantId;";
                string UsersCount = this._db.Query<string>(sql4, new Dictionary<string, object> { { "TenantId", MyUserInfo.TenantId } }).FirstOrDefault();

                if (UserName != null)
                {
                    MyResultReponse.Status = "ERROR";
                    MyResultReponse.StatusCode = "ERROR";
                    MyResultReponse.Message = "User Name Already Exists...!!!";
                    MyResultReponse.Data = MyUser.ToString();
                }

                else if (emailId != null)
                {
                    MyResultReponse.Status = "ERROR";
                    MyResultReponse.StatusCode = "ERROR";
                    MyResultReponse.Message = "Email Already Exists...!!!";
                    MyResultReponse.Data = MyUser.ToString();
                }
                else if (int.Parse(UsersCount) >= int.Parse(TenantCount) && int.Parse(TenantCount) != 0)
                {
                    MyResultReponse.Status = "ERROR";
                    MyResultReponse.StatusCode = "ERROR";
                    MyResultReponse.Message = "Maximum number of users reached....!!!";
                    MyResultReponse.Data = MyUser.ToString();
                }
                else
                {
                    string insertUserSql = @"INSERT Users(UserName,
                                             FirstName,
                                             LastName,
                                             UserEmail,
                                             UserPassword,
                                             tenantId,
                                             usertypeId,
                                             roleId,
                                             createdBy,createdDate,managerId,isCustom,fromDate,toDate)
                                            values(@UserName,@FirstName,@LastName,@UserEmail,@UserPassword,@tenantId,@usertypeId,@roleId,@createdBy,
                                                    @createdDate,@managerId,@isCustom,@fromDate,@toDate);
                                    SELECT LAST_INSERT_ID();";
                    NewUserId = this._db.QuerySingle<int>(insertUserSql,
                                                    new
                                                    {
                                                        UserName = MyUser.UserName,
                                                        FirstName = MyUser.FirstName,
                                                        LastName = MyUser.LastName,
                                                        UserEmail = MyUser.Email,
                                                        UserPassword = MyUser.UserPassword,
                                                        tenantId = MyUserInfo.TenantId,
                                                        roleId = 1,
                                                        usertypeId = 1,
                                                        Id = MyUser.UserId,
                                                        createdBy = MyUserInfo.UserId,
                                                        createdDate = DateTime.Now,
                                                        managerId = MyUser.ManagerId,
                                                        isCustom = 1,
                                                        fromDate = FirstDay,
                                                        toDate = LastDay
                                                    });

                    if (NewUserId > 0)
                    {
                        string insertSql = @"INSERT tenants(TenantName)values(@TenantName);
                                    SELECT LAST_INSERT_ID();";
                        int TenantId = this._db.QuerySingle<int>(insertSql,
                                                          new
                                                          {
                                                              TenantName = MyUser.TenantName,

                                                          });
                    }

                    MyResultReponse.Status = "SUCCESS";
                    MyResultReponse.StatusCode = "SUCCESS";
                    MyResultReponse.Message = "User Created Successfully ";
                    MyResultReponse.Data = NewUserId.ToString();

                }

            }
            catch (Exception ex)
            {
                MyResultReponse.Status = "ERROR";
                MyResultReponse.StatusCode = "ERROR";
                MyResultReponse.Message = "Error Occured while inserting contact administrator";
                MyResultReponse.Data = ex.ToString();

                var Message = ex.ToString();
                throw;
            }
            return MyResultReponse;
        }

        public ResultReponse UpdateSMTPSettings(SMTPSettings MySMTPSettings, UserInfo MyUserInfo)
        {
            ResultReponse MyResultReponse = new ResultReponse();
            SMTPSettings ExistSMTPSettings = new SMTPSettings();
            int result = 0;
            int NewUserId = 0;
            try
            {

                const string sql = @"Select * From smtpsettings Where tenantId=@tenantId;";
                ExistSMTPSettings = this._db.Query<SMTPSettings>(sql, new Dictionary<string, object> { { "tenantId", MyUserInfo.TenantId } }).FirstOrDefault();

                if (ExistSMTPSettings != null)
                {
                    string updateQuery = @"UPDATE SMTPSettings
                                            SET hostName = @hostName,
                                            displayName=@displayName,
                                            port=@port,
                                            security=@security,
                                            authentication=@authentication,
                                            userName=@userName,
                                            userPassword=@userPassword,
                                            tenantId=@tenantId
                                            WHERE Id=@Id";

                    result = this._db.Execute(updateQuery, new
                    {
                        hostName = MySMTPSettings.HostName,
                        displayName = MySMTPSettings.DisplayName,
                        port = MySMTPSettings.Port,
                        security = MySMTPSettings.Security,
                        authentication = MySMTPSettings.Authentication,
                        userName = MySMTPSettings.UserName,
                        userPassword = MySMTPSettings.UserPassword,
                        tenantId = MySMTPSettings.TenantId,
                        Id = MySMTPSettings.Id

                    });
                }
                else
                {
                    string insertSql = @"INSERT smtpsettings(hostName,
                                             port,
                                             security,
                                             authentication,
                                             userName,
                                             userPassword,
                                             tenantId,
                                             lastUpdate)values(@hostName,@port,@security,@authentication,@userName,@userPassword,@tenantId,@lastUpdate);
                                    SELECT LAST_INSERT_ID();";

                    NewUserId = this._db.QuerySingle<int>(insertSql,
                                                  new
                                                  {
                                                      hostName = MySMTPSettings.HostName,
                                                      port = MySMTPSettings.Port,
                                                      security = MySMTPSettings.Security,
                                                      authentication = MySMTPSettings.Authentication,
                                                      userName = MySMTPSettings.UserName,
                                                      userPassword = MySMTPSettings.UserPassword,
                                                      tenantId = MySMTPSettings.TenantId,
                                                      lastUpdate = DateTime.Now
                                                  });
                }

                MyResultReponse.Status = "SUCCESS";
                MyResultReponse.StatusCode = "SUCCESS";
                MyResultReponse.Message = "SMTP settings updated successfully ";
                MyResultReponse.Data = result.ToString();


            }
            catch (Exception ex)
            {
                MyResultReponse.Status = "FAIL";
                MyResultReponse.StatusCode = "FAIL";
                MyResultReponse.Message = "Error Occured while Updating contact administrator";
                MyResultReponse.Data = ex.ToString();

                var Message = ex.ToString();
                throw;
            }

            return MyResultReponse;

        }

        public SMTPSettings GetSMTPSettings(int Id, int TenantId, UserInfo MyUserInfo)
        {
            SMTPSettings MySMTPSettings = new SMTPSettings();

            SMTPSettings RetSMTPSettings = new SMTPSettings();

            const string sql =
                 @"Select s.Id,s.hostName,s.displayName,s.port,s.security,s.authentication,s.userName,s.userPassword,s.tenantId from boundhoundmaster.smtpsettings s
                    where s.tenantId=@tenantId";

            //MySMTPSettings = this._db.Query<SMTPSettings>(sql, new Dictionary<string, object> { { "Id", Id }, { "tenantId", TenantId } }).FirstOrDefault();
            MySMTPSettings = this._db.Query<SMTPSettings>(sql, new Dictionary<string, object> { { "tenantId", TenantId } }).FirstOrDefault();

            if (MySMTPSettings == null)
            {
                RetSMTPSettings.HostName = "smtp.gmail.com";
                RetSMTPSettings.Port = 587;
                RetSMTPSettings.Security = "Secure";
                RetSMTPSettings.Authentication = "Auto";
                RetSMTPSettings.UserName = "username@gmail.com";
                RetSMTPSettings.UserPassword = "password";
                RetSMTPSettings.TenantId = MyUserInfo.TenantId;
            }
            else
            {
                RetSMTPSettings = MySMTPSettings;
            }

            return RetSMTPSettings;
        }

        public IEnumerable<UserDTO> GetReportingUsers(UserInfo MyUserInfo)
        {
            IEnumerable<UserDTO> MyUserList = null;
            //  UserDTO MyUserList = new UserDTO();
            int ManagerId = 0;
            int UserId = 0;
            List<ReportingUsers> MyReportingUsersLst = new List<ReportingUsers>();

            ReportingUsers MyReportingUsers = new ReportingUsers();

            List<Child> MyChildLst = new List<Child>();
            try
            {

                string QUERY = @" Select usr.Id,usr.firstName,usr.LastName,usr.userName,COALESCE(managerId,0) ManagerId From users usr WHERE usr.tenantId =@tenantId";
                MyUserList = this._dbMaster.Query<UserDTO>(QUERY, new Dictionary<string, object> { { "tenantId", MyUserInfo.TenantId } }).ToList();


                //foreach(UserDTO usr in MyUserList.Where(x=>x.ManagerId==0).ToList())
                //{
                //    UserId = usr.Id;
                //    ManagerId = usr.ManagerId;

                //    Data DataInfo = new Data();
                //    DataInfo.UserName = usr.UserName;
                //    DataInfo.ManagerId = usr.ManagerId.ToString();

                //    MyReportingUsers.data = DataInfo;

                //    //Get Children
                //    if (ManagerId == 0)
                //    {
                //        var childrenuser = MyUserList.Where(x => x.ManagerId == UserId).ToList();

                //        foreach (var m in childrenuser)
                //        {
                //            Data MyData = new Data();
                //            Child MyChild = new Child();

                //            MyData.UserName = m.UserName;
                //            MyData.ManagerId = m.ManagerId.ToString();


                //            MyChild.data = MyData;
                //            MyChild.children = null;

                //            MyChildLst.Add(MyChild);
                //        }

                //        MyReportingUsers.children = MyChildLst;
                //    }
                //    else
                //    {
                //        MyReportingUsers.children = MyChildLst;
                //    }


                //    MyReportingUsersLst.Add(MyReportingUsers);
                //}

            }
            catch (Exception ex)
            {
                var Message = ex.ToString();
            }
            return MyUserList;
        }
    }
}
