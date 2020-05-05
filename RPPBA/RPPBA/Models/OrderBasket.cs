
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class OrderBasket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TransactionId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalSale { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
        public virtual Orders Transaction { get; set; }
    }
}
