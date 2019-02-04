using MyUELLoyalty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyURLLoyalty.Business.Interface
{
    public interface IRegisterBusiness
    {
        ResultReponse RegisterForm(UserDTO RegisterInfo);
        SubscriptionDTO GetSubscriptionId(int subscriptionid);
        UserDTO creatingUsers(string Firstname, string lastname, string useremail, string password, string tenantname, int subscriptionid, SubscriptionDTO MySubscriptionInfo);

        UserDTO CheckEmailExists(string Email);
        ResultMessagesReponse SendMail_ForgetPassword(UserInfo UserInfo);
        ResultMessagesReponse Renewpassword(UserInfo UserInfo);
        TenantDTO GetCreatedLimit(UserInfo MyUserInfo);
        int CreditLimitDecrement(UserInfo MyUserInfo);
    }
}
