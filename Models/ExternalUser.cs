using System;
using System.Collections.Generic;

#nullable disable

namespace KhareedLo.Models
{
    public partial class ExternalUser
    {
        public int ExternalUserId { get; set; }
        public string ExternalUserName { get; set; }
        public string ExternalUserEmail { get; set; }
        public string ExternalUserPassword { get; set; }
    }
}
