using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class Balance
    {
        public int OrganizationId { get; set; }
        public int? CurrentBalance { get; set; }

        public virtual Organizations Organization { get; set; }
    }
}
