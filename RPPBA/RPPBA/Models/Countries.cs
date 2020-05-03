using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }

        public virtual Regions Region { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
