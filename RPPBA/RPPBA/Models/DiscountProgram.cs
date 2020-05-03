using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class DiscountProgram
    {
        public int DiscountId { get; set; }
        public int? StartSum { get; set; }
        public int? EndSum { get; set; }
        public int? Discount { get; set; }
    }
}
