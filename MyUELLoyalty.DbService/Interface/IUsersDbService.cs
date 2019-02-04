using MyUELLoyalty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.DbService.Interface
{
    public interface IUsersDbService
    {
        UserDTO IsUserCredentialsValid(string userName);
        bool IsUserExist(string userName);
        //bool ResetPassword(BoundHoundContext context, int id);
        //bool ChangePassword(BoundHoundContext context, LoginDTO dto);
        UserInfo GetUserInfo(int id);
        UserInfo Authenticate(string UserName, string Password);
        UserInfo TokenValidation(string Email, int UserId, int TenantId);

        int UpdateIsFirstTime(int tenantId, int userId);

        IEnumerable<UserDTO> GetUserList(UserInfo MyUserInfo);

        UserDTO GetUserById(int Id);
        IEnumerable<UserDTO> GetUserByIds(string Id);

        ResultReponse UpdateUser(UserDTO MyUser, UserInfo MyUserInfo);

        ResultReponse CreateUser(UserDTO MyUser, UserInfo MyUserInfo);

        IEnumerable<AppSettingsDTO> GetAppSettings(UserInfo MyUserInfo);

        ResultReponse UpdateSMTPSettings(SMTPSettings MyUser, UserInfo MyUserInfo);

        SMTPSettings GetSMTPSettings(int Id, int TenantId, UserInfo MyUserInfo);

        IEnumerable<UserDTO> GetReportingUsers(UserInfo MyUserInfo);

        int GetCreditUsage(UserInfo MyUserInfo);

    }
}
