using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class User
    {
        public User()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(255, ErrorMessage = "User name must be less than {1} characters.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string UserPassword { get; set; }
        public int UserRoleId { get; set; }
        public string SignupWith { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(255, ErrorMessage = "First name must be less than {1} characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(255, ErrorMessage = "Last name must be less than {1} characters.")]
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public string Address { get; set; }
        [Phone(ErrorMessage = "The Phone Address is not valid.")]
        public string PhoneNumber { get; set; } 
        public string Device { get; set; }
        public bool? Ismember { get; set; }
        public int? MemberId { get; set; }
        public bool? Disabled { get; set; }
        public DateTime? LoginExpiryDate { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public DateTime? FirstLogin { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? UserCreate { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }
        public bool? Active { get; set; }
        [EmailAddress(ErrorMessage = "The Email Address is not valid.")]
        public string Email { get; set; }
        public string passcode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual Member Member { get; set; }
        public virtual Role UserRole { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
