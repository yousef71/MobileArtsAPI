using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public int PermissionId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Contents is required.")]
        public string Contents { get; set; }
        [Required(ErrorMessage = "Url is required.")]
        public string Url { get; set; }
        public bool? Grants { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? UserCreate { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
