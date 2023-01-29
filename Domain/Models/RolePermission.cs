using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class RolePermission
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Permission is required.")]
        public int PermissionId { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? UserCreate { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }
        public bool? Active { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
