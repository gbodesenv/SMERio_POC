using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rio.SME.Web.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Senha")]
        public string password { get; set; }

    }
}