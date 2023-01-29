using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class Member
    {
        public Member()
        {
          
            UserFavorites = new HashSet<UserFavorite>();
            UserShareds = new HashSet<UserShared>();
            Users = new HashSet<User>();
        }

        public int MemberId { get; set; }
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(255, ErrorMessage = "User name must be less than {1} characters.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")] 
        public string UserPassword { get; set; }
        public string SignupWith { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(255, ErrorMessage = "First name must be less than {1} characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(255, ErrorMessage = "Last name must be less than {1} characters.")]
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public string ProfilePic { get; set; }
        public string TradeName { get; set; }
        public string BrandLogo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "The Phone Address is not valid.")]
        public string PhoneNumber { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string CountryZipcode { get; set; }
        public string Device { get; set; }
        public bool? Isguest { get; set; }
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

 
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserShared> UserShareds { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
