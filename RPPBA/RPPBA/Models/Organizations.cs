using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class Organizations
    {
        public Organizations()
        {
            Orders = new HashSet<Orders>();
        }

        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDirectorFullname { get; set; }
        public string OrganizationPaymentAccount { get; set; }
        public string OrganizationPhoneInt { get; set; }
        public int OrganizationAddressId { get; set; }

        public virtual Addresses OrganizationAddress { get; set; }
        public virtual Balance Balance { get; set; }
        public virtual Discounts Discounts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
