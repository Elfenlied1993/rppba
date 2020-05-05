using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Addresses = new HashSet<Addresses>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
    }
}
