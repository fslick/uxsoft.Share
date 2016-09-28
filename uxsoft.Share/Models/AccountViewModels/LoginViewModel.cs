using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace uxsoft.Share.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression("^[A-z0-9_\\.-]{2,16}$")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
