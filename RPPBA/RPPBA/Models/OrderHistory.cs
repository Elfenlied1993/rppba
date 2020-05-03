using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class OrderHistory
    {
        public int OrderHistoryId { get; set; }
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? StatusUpdateDate { get; set; }
        public int? AmountTransfer { get; set; }
        public string Comment { get; set; }

        public virtual Orders Order { get; set; }
    }
}
