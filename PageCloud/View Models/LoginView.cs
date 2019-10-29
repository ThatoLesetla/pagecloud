using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageCloud.View_Models
{
    public class LoginView
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}