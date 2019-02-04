using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class TemplateDTO :EntityBase
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string TemplateBody { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDefault { get; set; }
        public Boolean IsSystem { get; set; }

        public TemplateCategoryDTO TemplateCategory { get; set; }

        public string Status { set; get; }
        public Boolean PlanEditor { set; get; }
        public string CategoryName { set; get; }
    }

    public class Template
    {
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public Boolean IsActive { get; set; }
    }
    public class TemplateListDTO
    {
        public IEnumerable<Template> TemplateList { set; get; }
    }
}
