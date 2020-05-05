using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Countries = new HashSet<Countries>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Countries> Countries { get; set; }
    }
}
