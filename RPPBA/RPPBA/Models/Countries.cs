using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }

        public virtual Regions Region { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
