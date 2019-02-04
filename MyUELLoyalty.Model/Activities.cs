using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUELLoyalty.Model
{
    public class Activities
    {
        public int ActivitiesID_PK { get; set; }

        public string ActivitiesName { get; set; }

        public string ActivitiesAlias { get; set; }

        public bool? ActivitiesActive { get; set; }

        public Guid? ActivitiesRowID { get; set; }

        public DateTime ActivitiesTimeStamp { get; set; }

    }
}
