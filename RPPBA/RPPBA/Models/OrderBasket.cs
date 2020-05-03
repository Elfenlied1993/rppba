using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class OrderBasket
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? TotalSale { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
        public virtual Orders Transaction { get; set; }
    }
}
