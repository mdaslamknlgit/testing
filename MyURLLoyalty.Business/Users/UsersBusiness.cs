using MyUELLoyalty.DbService.Users;
using MyUELLoyalty.Model;
using MyURLLoyalty.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyURLLoyalty.Business.Users
{
    public class UsersBusiness : IUsersBusiness
    {
        public UserInfo Authenticate(string UserName, string Password)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.Authenticate(UserName, Password);
        }

        public UserInfo TokenValidation(string Email, int UserId, int TenantId)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.TokenValidation(Email, UserId, TenantId);
        }


        public int UpdateIsFirstTime(int tenantId, int userId)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.UpdateIsFirstTime(tenantId, userId);
        }

        public IEnumerable<UserDTO> GetUserList(UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetUserList(MyUserInfo);
        }

        public UserDTO GetUserById(int Id, UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetUserById(Id);
        }
        public IEnumerable<UserDTO> GetUserByIds(string Id, UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetUserByIds(Id);
        }
        public ResultReponse UpdateUser(UserDTO MyUser, UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.UpdateUser(MyUser, MyUserInfo);
        }
        public ResultReponse CreateUser(UserDTO MyUser, UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.CreateUser(MyUser, MyUserInfo);
        }

        public IEnumerable<AppSettingsDTO> GetAppSettings(UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetAppSettings(MyUserInfo);
        }

        public ResultReponse UpdateSMTPSettings(SMTPSettings SMTPSettings, UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.UpdateSMTPSettings(SMTPSettings, MyUserInfo);
        }

        public SMTPSettings GetSMTPSettings(int Id, int TenantId, UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetSMTPSettings(Id, TenantId, MyUserInfo);
        }

        public IEnumerable<UserDTO> GetReportingUsers(UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetReportingUsers(MyUserInfo);
        }

        public int GetCreditUsage(UserInfo MyUserInfo)
        {
            UsersDbService m_UsersDbService = new UsersDbService();
            return m_UsersDbService.GetCreditUsage(MyUserInfo);
        }
    }
}
