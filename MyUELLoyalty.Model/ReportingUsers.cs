using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class ReportingUsers
    {
        public Data data { get; set; }
        public List<Child> children { get; set; }
    }
    public class Child
    {
        public Data data { get; set; }
        public List<Child2> children { get; set; }
    }


    public class Data
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ManagerId { get; set; }
    }


    public class Data3
    {
        public string UserName { get; set; }
        public string ManagerId { get; set; }
    }

    public class Child2
    {
        public Data3 data { get; set; }
    }


}
