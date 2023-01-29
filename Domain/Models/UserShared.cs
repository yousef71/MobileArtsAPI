using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class UserShared
    {
        [Required(ErrorMessage = "Member Id is required.")]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "=Product Id is required.")]
        public int ProductId { get; set; }
        public string SharedUsing { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? Active { get; set; }

        public virtual Member Member { get; set; }
    }
}
