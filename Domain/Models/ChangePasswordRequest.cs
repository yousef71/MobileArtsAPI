using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Domain.Models
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        [Required(ErrorMessage = "NewPassword is required.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmNewPassword is required.")]
        public string ConfirmNewPassword { get; set; }


    }
}
