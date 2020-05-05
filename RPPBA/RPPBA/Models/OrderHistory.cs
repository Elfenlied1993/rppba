using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class OrderHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? OrderHistoryId { get; set; }
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? StatusUpdateDate { get; set; }
        public decimal? AmountTransfer { get; set; }
        public string Comment { get; set; }

        public virtual Orders Order { get; set; }
    }
}
