
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Balance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }
        public decimal? CurrentBalance { get; set; }

        public virtual Organizations Organization { get; set; }
    }
}
