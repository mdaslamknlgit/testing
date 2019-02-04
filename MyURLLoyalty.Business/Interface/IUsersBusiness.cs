using MyUELLoyalty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyURLLoyalty.Business.Interface
{
    public interface IUsersBusiness
    {
        UserInfo Authenticate(string UserName, string Password);

        UserInfo TokenValidation(string Email, int UserId, int TenantId);

        int UpdateIsFirstTime(int tenantId, int userId);

        //////
        IEnumerable<UserDTO> GetUserList(UserInfo MyUserInfo);

        UserDTO GetUserById(int Id, UserInfo MyUserInfo);

        IEnumerable<UserDTO> GetUserByIds(string Id, UserInfo MyUserInfo);

        SMTPSettings GetSMTPSettings(int Id, int TenantId, UserInfo MyUserInfo);

        ResultReponse UpdateUser(UserDTO MyUser, UserInfo MyUserInfo);

        ResultReponse CreateUser(UserDTO MyUser, UserInfo MyUserInfo);

        IEnumerable<AppSettingsDTO> GetAppSettings(UserInfo MyUserInfo);

        ResultReponse UpdateSMTPSettings(SMTPSettings MySMTPSettings, UserInfo MyUserInfo);

        IEnumerable<UserDTO> GetReportingUsers(UserInfo MyUserInfo);

        int GetCreditUsage(UserInfo MyUserInfo);
    }
}
