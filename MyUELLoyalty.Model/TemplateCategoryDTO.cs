using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class TemplateCategoryDTO : EntityBase
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean PlanEditor { set; get; }
        public Boolean IsDefault { get; set; }
        public Boolean IsSystem { get; set; }

        public List<TemplateDTO> TemaplateList { get; set; }
    }
}
