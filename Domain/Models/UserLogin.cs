using System;
using System.Collections.Generic;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class UserLogin
    {
        public int LoginId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pasw { get; set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? LoginExpiryDate { get; set; }

        public virtual User User { get; set; }
    }
}
