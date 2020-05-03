using System;
using System.Collections.Generic;

namespace RPPBA.Models
{
    public partial class System
    {
        public int SystemId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }
    }
}
