
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Discounts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }
        public int? Discount { get; set; }

        public virtual Organizations Organization { get; set; }
    }
}
