using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPBA.Models
{
    public partial class System
    {
        public int? SystemId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }
    }
}
