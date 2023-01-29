using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Domain.Models
{
    public class LogInRequest
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        public string UserPassword { get; set; }




        
    }
}
