using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderBasketOrder = new HashSet<OrderBasket>();
            OrderHistory = new HashSet<OrderHistory>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TransactionId { get; set; }
        public int OrderId { get; set; }
        public int OrganizationId { get; set; }
        public int AddressId { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal? TotalSale { get; set; }
        public int? Discount { get; set; }
        public int? ExtraDiscount { get; set; }
        public string Comment { get; set; }

        public virtual Addresses Address { get; set; }
        public virtual Organizations Organization { get; set; }
        public virtual OrderBasket OrderBasketTransaction { get; set; }
        public virtual ICollection<OrderBasket> OrderBasketOrder { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
