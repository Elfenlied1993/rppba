using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class DiscountProgram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? DiscountId { get; set; }
        public decimal? StartSum { get; set; }
        public decimal? EndSum { get; set; }
        public int? Discount { get; set; }
    }
}
