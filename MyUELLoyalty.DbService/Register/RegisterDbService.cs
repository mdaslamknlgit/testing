using Dapper;
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
using MyUELLoyalty.Core.Services;

namespace MyUELLoyalty.DbService.Register
{
    public class RegisterDbService : IRegisterDbService
    {
        private readonly IDbConnection _db;
        string LinkedinConnectionString = ConfigurationManager.ConnectionStrings["LinkedInConnection"].ToString();
        public RegisterDbService()
        {
            
            _db = new SqlConnection(LinkedinConnectionString);
            

            //this.TenantConnectionStr = ConfigurationManager.ConnectionStrings["TenantConnection"].ConnectionString.ToString();
            //TenantConnectionStr = string.Format(TenantConnectionStr, "test1");
        }
        public ResultReponse RegisterForm(UserDTO RegisterInfo)
        {
            //    user userinfo = new user();
            ResultReponse MyResultReponse = new ResultReponse();
            //    try
            //    {
            //        using (var context = new boundhoundEntities())
            //        {
            //            var UserExists = context.users.Where(x => x.userEmail == RegisterInfo.Email).FirstOrDefault();
            //            if (UserExists != null)
            //            {

            //                MyResultReponse.StatusCode = "EMAILEXISTS";
            //                MyResultReponse.Status = "ERROR";
            //                MyResultReponse.Message = string.Format("User already exists with this email {0}", RegisterInfo.Email);



            //            }
            //            else
            //            {
            //                userinfo.userId = 0;
            //                userinfo.userName = RegisterInfo.UserName;
            //                userinfo.userEmail = RegisterInfo.Email;
            //                userinfo.userPassword = RegisterInfo.Password;
            //                userinfo.tenantId = RegisterInfo.TenantId;
            //                userinfo.isFirstTime = 1;

            //                context.users.Add(userinfo);

            //                MyResultReponse.StatusCode = "SUCCESS";
            //                MyResultReponse.Status = "SUCCESS";
            //                MyResultReponse.Message = string.Format("User created successfully with this email {0}", RegisterInfo.Email);

            //            }
            //            context.SaveChanges();
            //        }
            return MyResultReponse;
            //    }
            //    catch(Exception exp)
            //    {
            //        throw;
            //    }

        }

        public SubscriptionDTO GetSubscriptionId(int subscriptionid)
        {
            SubscriptionDTO subscriptionDTO = null;
            try
            {
                subscriptionDTO = this._db.Query<SubscriptionDTO>("SELECT subscriptionId,subscriptionName,subscriptionDays,subscriptionType,amount,noOfUser,creditLimit FROM subscription WHERE subscriptionId = @subscriptionId", new { subscriptionId = subscriptionid }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO
                var MessageError = ex.ToString();
            }

            return subscriptionDTO;
        }

        public TenantDTO GetCreatedLimit(UserInfo MyUserInfo)
        {
            TenantDTO TenantsInfo = null;
            try
            {
                TenantsInfo = this._db.Query<TenantDTO>("SELECT * from tenants WHERE Id = @Id", new { Id = MyUserInfo.TenantId }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO
                var MessageError = ex.ToString();
            }

            return TenantsInfo;
        }

        public int CreditLimitDecrement(UserInfo MyUserInfo)
        {
            int ReturnValue = 0;
            try
            {
                string updateQuery = @"UPDATE tenants SET creditLimit=creditLimit-1 WHERE Id =@Id";
                ReturnValue = this._db.Execute(updateQuery, new
                {
                    Id = MyUserInfo.TenantId
                });


            }
            catch (Exception ex)
            {
                //TODO
                var MessageError = ex.ToString();
            }
            return ReturnValue;
        }



        public UserDTO creatingUsers(string Firstname, string lastname, string useremail, string password, string tenantname,
            int subscriptionid, SubscriptionDTO MySubscriptionInfo)
        {
            UserDTO userDTO = null;
            TenantDTO tenantDTO = null;
            DateTime SubscriptionEnd = DateTime.Now;
            string TenantsQuery = "";

            DateTime FirstDay = DateTime.Now;
            DateTime LastDay = DateTime.Now;
            try
            {
                int year = DateTime.Now.Year;
                FirstDay = new DateTime(year, 1, 1);
                LastDay = new DateTime(year, 12, 31);


                if (MySubscriptionInfo.SubscriptionDays > 0)
                {
                    SubscriptionEnd = DateTime.Now.AddDays(MySubscriptionInfo.SubscriptionDays);
                }

                TenantsQuery = @"INSERT tenants(tenantName,subscriptionId,subscriptionEnd,noOfUsers,creditLimit)
                                                values(@tenantName,@subscriptionId,@subscriptionEnd,@noOfUsers,@creditLimit)";

                int rowsAffected = this._db.Execute(TenantsQuery,
                   new
                   {
                       tenantName = tenantname,
                       subscriptionId = subscriptionid,
                       subscriptionEnd = SubscriptionEnd,
                       noOfUsers = MySubscriptionInfo.noOfUser,
                       creditLimit = MySubscriptionInfo.creditLimit
                   }
                   );
                if (rowsAffected != 0)
                {
                    tenantDTO = this._db.Query<TenantDTO>("SELECT * FROM tenants order by Id desc limit 1").FirstOrDefault();

                    int rowsAffected1 = _db.Execute(@"INSERT users(userTypeId,firstName,lastName,userName,userEmail,userPassword,tenantId,roleId,isFirstTime,isCustom,fromDate,toDate) 
                                        values (@userTypeId,@firstName,@lastName,@userName,@userEmail,@userPassword,@tenantId,@roleId,@isFirstTime,@isCustom,@fromDate,@toDate)",
                        new
                        {
                            userTypeId = 1,
                            firstName = Firstname,
                            lastname = lastname,
                            userName = Firstname + " " + lastname,
                            userEmail = useremail,
                            userPassword = password,
                            tenantId = tenantDTO.ID,
                            roleId = 1,
                            isFirstTime = 1,
                            isCustom = 1,
                            fromDate = FirstDay,
                            toDate = LastDay
                        });
                    if (rowsAffected1 != 0)
                    {
                        userDTO = this._db.Query<UserDTO>("SELECT * FROM  users order by Id desc limit 1").FirstOrDefault();
                    }
                }
            }
            catch (Exception exp)
            {
                var ErrorMessage = exp.ToString();
            }
            return userDTO;

        }


        public UserDTO CheckEmailExists(string Email)
        {
            ResultReponse MyResultReponse = new ResultReponse();
            UserDTO UserInfo = new UserDTO();
            try
            {

                const string sql = @"Select * from users Where userEmail=@email;";

                return this._db.Query<UserDTO>(sql, new Dictionary<string, object> { { "email", Email } }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                //TODO
                var MessageError = ex.ToString();

            }

            return null;
        }

        public ResultMessagesReponse SendMail_ForgetPassword(UserInfo userInfo)
        {

            SendEmailCore m_sendEmailCore = new SendEmailCore();
            ResultMessagesReponse MyResultMessagesReponse = new ResultMessagesReponse();
            try
            {
                const string sql = @"Select * from users Where userEmail=@email;";
                UserInfo MyUserInfo = this._db.Query<UserInfo>(sql, new Dictionary<string, object> { { "email", userInfo.userEmail } }).FirstOrDefault();
                string FromUserEmail = ConfigurationManager.AppSettings["FromUserEmail"];
                string FromUserPassword = ConfigurationManager.AppSettings["FromPassword"];

                if (MyUserInfo != null)
                {
                    string EncryEmail = Convert.ToBase64String(Encoding.UTF8.GetBytes(MyUserInfo.userEmail));
                    string _EncryEmail = EncryEmail.Replace("=", "*");

                    MyUserInfo.FromUserEmail = FromUserEmail;
                    MyUserInfo.FromPassword = FromUserPassword;
                    MyUserInfo.UI_PageURL = userInfo.UI_PageURL;
                    string EncryptEmail = _EncryEmail;
                    bool CheckStatus = m_sendEmailCore.SendEmail_forgetPassword(MyUserInfo, EncryptEmail);
                    if (CheckStatus == true)
                    {
                        //MyResultMessagesReponse.Status = "0";
                        MyResultMessagesReponse.Status = "SUCCESS";
                    }
                }
                else
                {
                    MyResultMessagesReponse.Status = "FAIL";
                }
            }
            catch (Exception ex)
            {
                //TODO
                var MessageError = ex.ToString();
                MyResultMessagesReponse.Status = "FAIL";
                throw;
            }


            return MyResultMessagesReponse;
        }


        public ResultMessagesReponse Renewpassword(UserInfo userInfo)
        {
            ResultMessagesReponse MyResultMessagesReponse = new ResultMessagesReponse();

            try
            {
                string _EncryEmail = userInfo.Email.Replace("*", "=");
                string DecryptEmail = Encoding.UTF8.GetString(Convert.FromBase64String(_EncryEmail));

                int result = 0;
                string updateQuery = @"UPDATE users SET userPassword=@Password WHERE userEmail =@userEmail";
                result = this._db.Execute(updateQuery, new
                {
                    password = userInfo.Password,
                    userEmail = DecryptEmail
                });
                if (result > 0)
                {
                    MyResultMessagesReponse.Status = "SUCCESS";
                    MyResultMessagesReponse.StatusCode = "SUCCESS";
                    MyResultMessagesReponse.Message = "Password Updated Successfully ";

                }
            }
            catch (Exception ex)
            {
                var MessageError = ex.ToString();
                MyResultMessagesReponse.Status = "FAIL";
                throw;
            }
            return MyResultMessagesReponse;

        }

    }
}
