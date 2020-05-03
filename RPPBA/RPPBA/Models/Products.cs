using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderBasket = new HashSet<OrderBasket>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductPrice { get; set; }
        public int? ProductAvailableQuantity { get; set; }
        public int? ProductReservedQuantity { get; set; }

        public virtual ICollection<OrderBasket> OrderBasket { get; set; }
    }
}
