using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Addresses
    {
        public Addresses()
        {
            Orders = new HashSet<Orders>();
            Organizations = new HashSet<Organizations>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AddressId { get; set; }
        public int CityId { get; set; }
        public string StreetName { get; set; }
        public string BuildingInt { get; set; }

        public virtual Cities City { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Organizations> Organizations { get; set; }
    }
}
