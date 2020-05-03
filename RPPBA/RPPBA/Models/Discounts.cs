using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class Discounts
    {
        public int OrganizationId { get; set; }
        public int? Discount { get; set; }

        public virtual Organizations Organization { get; set; }
    }
}
