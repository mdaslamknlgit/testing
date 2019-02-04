//using BoundHound.DbService.EmailList;
//using BoundHound.DbService.Template;

using Mustache;
using MyUELLoyalty.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.TemplateEngine
{
    public class TemplateParsers
    {
        //private readonly EmailListDbService m_EmailListDbService;
        //private readonly TemplateDbService m_TemplateDbService;
        public TemplateParsers(UserInfo MyUserInfo)
        {
            //Initialize goes here
            //m_EmailListDbService = new EmailListDbService(MyUserInfo);
            //m_TemplateDbService = new TemplateDbService(MyUserInfo);
        }

        public string ParseTemplate(int TemplateID, EmailInfo emailInfo, UserInfo MyUserInfo)
        {
            string ReturnValue = "";
            string TemplateBody = "";
            string ParseValue = "";

            try
            {
                //Get Template Body from TemplateID
                //TemplateBody = m_TemplateDbService.GetTemplateBodyByTemplateID(TemplateID, MyUserInfo);


                //Parse Here 
                FormatCompiler compiler = new FormatCompiler();

                Generator generator = compiler.Compile(TemplateBody);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(emailInfo);
                //var reportData = String.Format("{{ feeTypes: {0} }}", JSONString);
                var reportData = JSONString;
                JObject jsonData = JObject.Parse(reportData);
                ParseValue = generator.Render(jsonData);

                ReturnValue = ParseValue;

                return ReturnValue;
            }

            catch (Exception exp)
            {
                throw;
            }
        }
        public string ParseAfterRegister(string templateBody, UserInfo MyUserInfo)
        {
            string ReturnValue = "";
            string TemplateBody = "";
            string ParseValue = "";
            TemplateDTO MyTemplateDTO = new TemplateDTO();
            try
            {
                //Get Template Body from TemplateID
                //MyTemplateDTO = m_TemplateDbService.GetTemplateByTemplateCode(TemplateCode, MyUserInfo);
                try
                {
                    MyUserInfo.UserName = MyUserInfo.UserName.Split(' ')[0];
                }
                catch (Exception exp)
                {
                    var messge = exp.ToString();
                }


                TemplateBody = templateBody;
                //Parse Here 
                FormatCompiler compiler = new FormatCompiler();

                Generator generator = compiler.Compile(TemplateBody);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(MyUserInfo);
                //var reportData = String.Format("{{ feeTypes: {0} }}", JSONString);
                var reportData = JSONString;
                JObject jsonData = JObject.Parse(reportData);
                ParseValue = generator.Render(jsonData);

                ReturnValue = ParseValue;

                return ReturnValue;
            }

            catch (Exception exp)
            {
                throw;
            }
        }


        public string PrintInvoice(string templateBody, InvoiceDTO invoiceDTO)
        {
            string ReturnValue = "";
            string TemplateBody = "";
            string ParseValue = "";
            TemplateDTO MyTemplateDTO = new TemplateDTO();
            try
            {
                //Get Template Body from TemplateID
                //MyTemplateDTO = m_TemplateDbService.GetTemplateByTemplateCode(TemplateCode, MyUserInfo);
                try
                {
                    //MyUserInfo.UserName = MyUserInfo.UserName.Split(' ')[0];
                }
                catch (Exception exp)
                {
                    var messge = exp.ToString();
                }


                TemplateBody = templateBody;
                //Parse Here 
                FormatCompiler compiler = new FormatCompiler();

                Generator generator = compiler.Compile(TemplateBody);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(invoiceDTO);
                //var reportData = String.Format("{{ feeTypes: {0} }}", JSONString);
                var reportData = JSONString;
                JObject jsonData = JObject.Parse(reportData);
                ParseValue = generator.Render(jsonData);

                ReturnValue = ParseValue;

                return ReturnValue;
            }

            catch (Exception exp)
            {
                throw;
            }
        }
        public string GetAfterRegister(string TemplateCode, UserInfo MyUserInfo)
        {
            string ReturnValue = "";
            string TemplateBody = "";
            string ParseValue = "";
            TemplateDTO MyTemplateDTO = new TemplateDTO();
            try
            {
                //Get Template Body from TemplateID
                //MyTemplateDTO = m_TemplateDbService.GetTemplateByTemplateCode(TemplateCode, MyUserInfo);

                TemplateBody = MyTemplateDTO.TemplateBody;
                //Parse Here 
                FormatCompiler compiler = new FormatCompiler();

                Generator generator = compiler.Compile(TemplateBody);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(MyUserInfo);
                //var reportData = String.Format("{{ feeTypes: {0} }}", JSONString);
                var reportData = JSONString;
                JObject jsonData = JObject.Parse(reportData);
                ParseValue = generator.Render(jsonData);

                ReturnValue = ParseValue;

                return ReturnValue;
            }

            catch (Exception exp)
            {
                throw;
            }
        }
        public string ParseTemplateBody(string templateBody, EmailInfo emailInfo, UserInfo MyUserInfo)
        {
            string ReturnValue = "";
            string TemplateBody = "";
            string ParseValue = "";

            try
            {
                //Get Template Body from TemplateID
                TemplateBody = templateBody;


                //Parse Here 
                FormatCompiler compiler = new FormatCompiler();

                Generator generator = compiler.Compile(TemplateBody);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(emailInfo);
                //var reportData = String.Format("{{ feeTypes: {0} }}", JSONString);
                var reportData = JSONString;
                JObject jsonData = JObject.Parse(reportData);
                ParseValue = generator.Render(jsonData);

                ReturnValue = ParseValue;

                return ReturnValue;
            }

            catch (Exception exp)
            {
                throw;
            }
        }

        public List<TemplateParseModel> ParseTemplate(int TemplateID, List<EmailInfo> emailInfoList, string templateBody, UserInfo MyUserInfo)
        {
            string ReturnValue = "";
            string TemplateBody = "";
            string ParseValue = "";
            List<TemplateParseModel> MyTemplateParseModel = new List<TemplateParseModel>();
            try
            {
                //Get Template Body from TemplateID
                //TemplateBody = m_TemplateDbService.GetTemplateBodyByTemplateID(TemplateID, MyUserInfo);
                TemplateBody = templateBody;

                //Parse Here 
                FormatCompiler compiler = new FormatCompiler();

                foreach (EmailInfo emf in emailInfoList)
                {
                    TemplateParseModel TemplateParseModelInfo = new TemplateParseModel();
                    try
                    {

                        Generator generator = compiler.Compile(TemplateBody);

                        string JSONString = string.Empty;
                        JSONString = JsonConvert.SerializeObject(emf);
                        //var reportData = String.Format("{{ feeTypes: {0} }}", JSONString);
                        var reportData = JSONString;
                        JObject jsonData = JObject.Parse(reportData);
                        ParseValue = generator.Render(jsonData);

                        TemplateParseModelInfo.ProfileId = emf.Profileid;
                        TemplateParseModelInfo.Email = emf.Email;
                        TemplateParseModelInfo.ParseValue = ParseValue;
                        TemplateParseModelInfo.ProfileURL = emf.Url;
                        TemplateParseModelInfo.Status = "SUCCESS";

                        MyTemplateParseModel.Add(TemplateParseModelInfo);
                    }
                    catch (Exception exp)
                    {
                        TemplateParseModelInfo.ProfileId = emf.Profileid;
                        TemplateParseModelInfo.Email = emf.Email;
                        TemplateParseModelInfo.ParseValue = ParseValue;
                        TemplateParseModelInfo.ProfileURL = emf.Url;
                        TemplateParseModelInfo.Status = "ERROR";
                        continue;
                    }

                }
                return MyTemplateParseModel;
            }

            catch (Exception exp)
            {
                throw;
            }
        }
    }
}
