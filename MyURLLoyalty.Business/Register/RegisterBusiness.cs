using MyUELLoyalty.DbService.Register;
using MyUELLoyalty.Model;
using MyURLLoyalty.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyURLLoyalty.Business.Register
{
    public class RegisterBusiness : IRegisterBusiness
    {
        public ResultReponse RegisterForm(UserDTO RegisterInfo)
        {

            try
            {

                RegisterDbService m_RegisterDbService = new RegisterDbService();

                return m_RegisterDbService.RegisterForm(RegisterInfo);
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        public SubscriptionDTO GetSubscriptionId(int subscriptionid)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.GetSubscriptionId(subscriptionid);
        }

        public UserDTO creatingUsers(string Firstname, string lastname, string useremail, string password, string tenantname, int subscriptionid, SubscriptionDTO MySubscriptioninfo)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.creatingUsers(Firstname, lastname, useremail, password, tenantname, subscriptionid, MySubscriptioninfo);

        }

        public UserDTO CheckEmailExists(string Email)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.CheckEmailExists(Email);
        }

        public ResultMessagesReponse SendMail_ForgetPassword(UserInfo UserInfo)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.SendMail_ForgetPassword(UserInfo);
        }

        public ResultMessagesReponse Renewpassword(UserInfo UserInfo)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.Renewpassword(UserInfo);
        }
        public TenantDTO GetCreatedLimit(UserInfo UserInfo)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.GetCreatedLimit(UserInfo);
        }

        public int CreditLimitDecrement(UserInfo UserInfo)
        {
            RegisterDbService m_RegisterDbService = new RegisterDbService();
            return m_RegisterDbService.CreditLimitDecrement(UserInfo);
        }
    }
}
